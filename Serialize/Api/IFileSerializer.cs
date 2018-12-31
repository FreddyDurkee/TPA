using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;

namespace Serialize.Api
{
    public interface IFileSerializer
    {
        void serialize(AssemblyMetadata obj, string filePath);

        AssemblyMetadata deserialize(string filePath);
    }
}
