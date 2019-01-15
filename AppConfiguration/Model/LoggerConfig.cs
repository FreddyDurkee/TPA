using AppConfiguration.Model.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfiguration.Model
{
    [Serializable]
    public class LoggerConfig
    {
        public string AssemblyName { get; set; }
        public string AssemblyCatalog { get; set; }
        public DictionaryStruct ConstructorArgs { get; set; }
    }
}
