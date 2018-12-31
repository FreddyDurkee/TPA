using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialize.Model.Xml
{
    [DataContract]
    public class FieldXmlModel
    {
        public FieldXmlModel(string name, TypeXmlModel type)
        {
            this.name = name;
            this.type = type;
        }
        #region public
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public TypeXmlModel type { get; set; }
        #endregion
    }
}
