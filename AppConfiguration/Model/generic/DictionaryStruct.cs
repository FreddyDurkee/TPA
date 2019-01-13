using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AppConfiguration.Model.generic
{
    public class DictionaryStruct : Dictionary<string, string>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(String));

            bool wasEmpty = reader.IsEmptyElement;
            if (wasEmpty)
                return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement();
                String key = reader.Name;
                String value = reader.ReadElementContentAsString();
                this.Add(key, value);
                reader.ReadEndElement();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer valSerializer = new XmlSerializer(typeof(String));
            foreach (String key in this.Keys)
            {
                writer.WriteStartElement(key);
                writer.WriteValue(this[key]);
                writer.WriteEndElement();
            }
        }
    }
}
