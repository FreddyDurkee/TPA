using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace DataTransferGraph.DTGModel
{
    public class AssemblyDTG
    {
        public List<TypeDTG> Types { get; set; }

        public string Name { get; set; }

       
        public AssemblyDTG(string name)
        {
            this.Types = new List<TypeDTG>();
            this.Name = name;
        }

    }
}

