using Serialize.Converter;
using Serialize.Model.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataTransferGraph.Api;
using DataTransferGraph.DTGModel;

namespace Serialize
{
    public class XMLSerializer : ISerializer
    {
        ModelToXMLConverter converter = new ModelToXMLConverter();

        public AssemblyDTG deserialize(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max);
            NetDataContractSerializer ser = new NetDataContractSerializer();
            AssemblyXmlModel xmlModel = (AssemblyXmlModel)ser.ReadObject(reader, true);
            reader.Close();
            return converter.FromDTO(xmlModel);
        }

        public void serialize(AssemblyDTG obj, string filePath)
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
