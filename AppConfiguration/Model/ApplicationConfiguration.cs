using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppConfiguration.Model
{
    [Serializable]
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {

        }
        public SerializerConfig SerializerConfig;
    }
}
