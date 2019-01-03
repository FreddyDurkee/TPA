﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferGraph.DTGModel;

namespace DataTransferGraph.Api
{
    public interface ISerializer
    {
        void serialize(AssemblyDTG obj, string filePath);

        AssemblyDTG deserialize(string filePath);
    }
}
