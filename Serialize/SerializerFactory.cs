using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serialize.Api;

namespace Serialize
{
    class SerializerFactory
    {
        [Export()]
        public IFileSerializer XmlSerializer { get { return new XMLSerializer(); } }

        [Export()]
        public IDBSerializer DbSerializer { get { return new DBSerializer(); } }
    }
}
