using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using AppConfiguration.Model.generic;

namespace AppConfiguration.Model
{
    [Serializable]
    public  class SerializerConfig
    {
        public string AssemblyName { get; set; }
        public string AssemblyCatalog { get; set; }
        public DictionaryStruct ConstructorArgs { get; set; }
    }
}
