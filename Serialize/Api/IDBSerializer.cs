﻿using Serialize.Model.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;

namespace Serialize.Api
{
    public interface IDBSerializer
    {
    void serialize(AssemblyXmlModel obj);

    AssemblyXmlModel deserialize(string modelID);
    }
}