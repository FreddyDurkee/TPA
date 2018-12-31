using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TPApplicationCore.ViewModelAPI;


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

