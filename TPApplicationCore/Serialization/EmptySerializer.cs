using DataTransferGraph.Api;
using DataTransferGraph.DTGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPApplicationCore.Serialization
{
    class EmptySerializer : ISerializer
    {
        public AssemblyDTG Deserialize()
        {
            return new AssemblyDTG("");
        }

        public void Serialize(AssemblyDTG obj)
        {
           // Do nothing.
        }
    }
}
