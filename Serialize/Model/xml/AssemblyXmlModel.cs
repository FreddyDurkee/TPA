using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Serialize.Model.Xml
{
    [DataContract]
    public class AssemblyXmlModel
    {
        public AssemblyXmlModel(string name)
        {
            this.name = name;
            typeList = new List<TypeXmlModel>();
        }

        [DataMember]
        public List<TypeXmlModel> typeList { get; set; }

        [DataMember]
        public string name { get; set; }

    }
}

