using DataTransferGraph.Api;
using DataTransferGraph.DTGModel;
using DataTransferGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Converter;
    using TPApplicationCore.Model;

namespace TPApplicationCore.Serialization
{
    
    public class SerializationManager
    {
        [Import]
        public ISerializer Serializer { get; set; }

        private ModelToDTGConverter converter = new ModelToDTGConverter();

        public SerializationManager()
        {
            Serializer = new EmptySerializer();
        }

        public void Serialize(AssemblyMetadata data)
        {
            Serializer.Serialize(converter.ToDTG(data));
        }

        public AssemblyMetadata Deserialize() {
            return converter.FromDTG(Serializer.Deserialize());
        }
    }
}
