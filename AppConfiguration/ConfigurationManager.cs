using AppConfiguration.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AppConfiguration
{
    public class ConfigurationManager
    {
        private string confPath;

        public ConfigurationManager(string confPath)
        {
            this.confPath = confPath;
        }

        public ApplicationConfiguration getApplicationConfiguration()
        {
            using (FileStream fs = new FileStream(confPath, FileMode.Open)) {
                XmlSerializer ser = new XmlSerializer(typeof(ApplicationConfiguration));
                return (ApplicationConfiguration)ser.Deserialize(fs);
            }
        }

        public void overrideFileConfig(ApplicationConfiguration config)
        {
            using (FileStream fs = new FileStream(confPath, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(ApplicationConfiguration));
                ser.Serialize(fs, config);
            }
        }

        public SerializerConfig getSerializerConfig()
        {
            return getApplicationConfiguration().serializerConfig;
        }
    }
}
