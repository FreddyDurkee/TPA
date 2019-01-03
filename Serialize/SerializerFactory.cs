﻿using System.ComponentModel.Composition;
using DataTransferGraph.Api;
namespace Serialize
{
    class SerializerFactory
    {
        [Export()]
        public ISerializer XmlSerializer { get { return new XMLSerializer(); } }
    }
}
