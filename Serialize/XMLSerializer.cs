using Serialize.Converter;
using Serialize.Model.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using DataTransferGraph.Api;
using DataTransferGraph.DTGModel;
using System.ComponentModel.Composition;

namespace Serialize
{
    [Export(typeof(ISerializer))]
    public class XMLSerializer : ISerializer
    {
        ModelToXMLConverter converter = new ModelToXMLConverter();
        private readonly string filePath;

        [ImportingConstructor]
        public XMLSerializer([Import("XMLSerializer.FileName")] string fileName)
        {
            this.filePath = fileName;
        }

        public AssemblyDTG Deserialize()
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max);
            NetDataContractSerializer ser = new NetDataContractSerializer();
            AssemblyXmlModel xmlModel = (AssemblyXmlModel)ser.ReadObject(reader, false);
            reader.Close();
            return converter.FromDTO(xmlModel);
        }

        public void Serialize(AssemblyDTG obj)
        {
            AssemblyXmlModel xmlModel = converter.ToDTO(obj);
            FileStream fs = new FileStream(filePath, FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            NetDataContractSerializer ser = new NetDataContractSerializer();
            ser.WriteObject(writer, xmlModel);
            writer.Close();
        }
    }
}