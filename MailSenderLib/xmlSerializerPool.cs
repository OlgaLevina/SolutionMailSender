using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;//потокобезопасные коллекции
using System.Xml.Serialization;

namespace MailSenderLib
{
    public static class xmlSerializerPool
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> _SerialisersPool = new ConcurrentDictionary<Type, XmlSerializer>();

        public static XmlSerializer GetSerializer<T>() => GetSerializer(typeof(T));

        public static XmlSerializer GetSerializer(Type objectType) =>
            _SerialisersPool.GetOrAdd(objectType, type => new XmlSerializer(type));

    }
}
