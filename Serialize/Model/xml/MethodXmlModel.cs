using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialize.Model.Xml
{
    [DataContract]
    public class MethodXmlModel
    {
        public MethodXmlModel(string name, TypeXmlModel returnType)
        {
            this.name = name;
            this.returnType = returnType;
            parameters = new List<ParameterXmlModel>();
        }
        #region public
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public TypeXmlModel returnType { get; set; }
        [DataMember]
        public List<ParameterXmlModel> parameters { get; set; }
        #endregion

    }
}