using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Tracer
{
    class MyXmlSerializer : ISerializer
    {
        private XmlSerializerNamespaces emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        public string Serialize(object obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(stringWriter, obj, emptyNamespaces);
                    return stringWriter.ToString();
                }
            }
        }
    }
}
