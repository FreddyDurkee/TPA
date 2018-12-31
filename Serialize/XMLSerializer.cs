using Serialize.Api;
using Serialize.Model.Xml;
using TPApplicationCore.Model;
using Serialize.Converter;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace Serialize
{
    class XMLSerializer : IFileSerializer
    {
        ModelToXMLConverter converter = new ModelToXMLConverter();
        public AssemblyMetadata deserialize(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max);
            NetDataContractSerializer ser = new NetDataContractSerializer();
            AssemblyXmlModel xmlModel = (AssemblyXmlModel)ser.ReadObject(reader, true);
            reader.Close();
            return converter.FromDTO(xmlModel);
        }

        public void serialize(AssemblyMetadata obj, string filePath)
        {
            AssemblyXmlModel xmlModel = converter.ToDTO(obj);
            FileStream fs = new FileStream(filePath.Substring(0, filePath.Length - 3) + "xml", FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            NetDataContractSerializer ser = new NetDataContractSerializer();
            ser.WriteObject(writer, xmlModel);
            writer.Close();
        }
    }
}
