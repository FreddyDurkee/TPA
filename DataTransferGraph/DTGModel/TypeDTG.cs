using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace DataTransferGraph.DTGModel

{
    public class TypeDTG
    {
        #region public
        public List<MethodDTG> Methods { get; set; }
        public List<PropertyDTG> Properties { get; set; }
        public List<FieldDTG> Fields { get; set; }
        public string Name { get; set; }
        public string NamespaceDef { get; set; }
        #endregion


        public TypeDTG(string name, string namespaceDef)
        {
            Methods = new List<MethodDTG>();
            Properties = new List<PropertyDTG>();
            Fields = new List<FieldDTG>();
            this.Name = name;
            this.NamespaceDef = namespaceDef;
        }

    }
}
