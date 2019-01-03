using System.ComponentModel.Composition;
using DataTransferGraph.Api;
namespace DbSerialize
{
    class SerializerFactory
    {
        [Export()]
        public ISerializer DbSerializer { get { return new DBSerializer(); } }
    }
}
