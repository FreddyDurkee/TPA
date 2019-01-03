using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferGraph.DTGModel
{
    public class FieldDTG
    {
        #region private
        public string Name { get; set; }
        public TypeDTG Type { get; set; }
        #endregion
        
        public FieldDTG(string name, TypeDTG type)
        {
            this.Name = name;
            this.Type = type;
        }

    }
}
