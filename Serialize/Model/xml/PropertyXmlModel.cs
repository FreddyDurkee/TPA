using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialize.Model.Xml
{
    [DataContract]
    public class PropertyXmlModel
    {
        public PropertyXmlModel(string name, TypeXmlModel type)
        {
            this.name = name;
            this.type = type;
            accessorList = new List<MethodXmlModel>();
        }
        #region public
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public TypeXmlModel type { get; set; }
        [DataMember]
        public List<MethodXmlModel> accessorList { get; set; }
        #endregion


    }
}
