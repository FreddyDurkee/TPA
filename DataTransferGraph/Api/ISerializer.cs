using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferGraph.DTGModel;

namespace DataTransferGraph.Api
{
    public interface ISerializer
    {
        void Serialize(AssemblyDTG obj);

        AssemblyDTG Deserialize();
    }
}
