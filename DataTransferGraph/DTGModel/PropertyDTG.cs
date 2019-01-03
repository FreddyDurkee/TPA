using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferGraph.DTGModel
{
    public class PropertyDTG
    {
        #region public
        public string Name { get; set; }
        public TypeDTG Type { get; set; }
        public List<MethodDTG> AccessorList { get; set; }
        #endregion

        public PropertyDTG(string name, TypeDTG propertyType)
        {
            this.Name = name;
            Type = propertyType;
            AccessorList = new List<MethodDTG>();
        }

    }
}
