using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferGraph.DTGModel
{
    public class MethodDTG
    {
        #region private
        public string Name { get; set; }
        public TypeDTG ReturnType { get; set; }
        public List<ParameterDTG> Parameters { get; set; }
        #endregion

        public MethodDTG(string name, TypeDTG type)
        {
            this.Name = name;
            ReturnType = type;
            Parameters = new List<ParameterDTG> ();
        }
    }
}