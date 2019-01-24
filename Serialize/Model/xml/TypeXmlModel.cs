using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialize.Model.Xml

{
    [DataContract]
    public class TypeXmlModel
    {
        public TypeXmlModel(string name, string namespaceDef)
        {
            this.name = name;
            this.NamespaceDef = namespaceDef;
            methods = new List<MethodXmlModel>();
            fields = new List<FieldXmlModel>();
            properties = new List<PropertyXmlModel>();
        }
        #region public
        [DataMember]
        public List<MethodXmlModel> methods { get; set; }
        [DataMember]
        public List<PropertyXmlModel> properties { get; set; }
        [DataMember]
        public List<FieldXmlModel> fields { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string NamespaceDef { get; internal set; }
        #endregion
    }
}
