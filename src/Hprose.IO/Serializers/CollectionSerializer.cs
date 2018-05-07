﻿/**********************************************************\
|                                                          |
|                          hprose                          |
|                                                          |
| Official WebSite: http://www.hprose.com/                 |
|                   http://www.hprose.org/                 |
|                                                          |
\**********************************************************/
/**********************************************************\
 *                                                        *
 * CollectionSerializer.cs                                *
 *                                                        *
 * CollectionSerializer class for C#.                     *
 *                                                        *
 * LastModified: Apr 7, 2018                              *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

using System.Collections.Generic;

using static Hprose.IO.Tags;

namespace Hprose.IO.Serializers {
    class CollectionSerializer<T, V> : ReferenceSerializer<T> where T : ICollection<V> {
        public override void Write(Writer writer, T obj) {
            base.Write(writer, obj);
            var stream = writer.Stream;
            int length = obj.Count;
            stream.WriteByte(TagList);
            if (length > 0) {
                ValueWriter.WriteInt(stream, length);
            }
            stream.WriteByte(TagOpenbrace);
            var serializer = Serializer<V>.Instance;
            foreach (V value in obj) {
                serializer.Serialize(writer, value);
            }
            stream.WriteByte(TagClosebrace);
        }
    }

}
