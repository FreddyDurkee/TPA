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
        private FileSystemWatcher fileWatcher;

        public ConfigurationManager(string confPath)
        {
            this.confPath = confPath;
          
            fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(confPath));
            fileWatcher.Filter = Path.GetFileName(confPath);
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.EnableRaisingEvents = true;
        }

        public ApplicationConfiguration getApplicationConfiguration()
        {
            using (FileStream fs = new FileStream(confPath, FileMode.Open)) {
                XmlSerializer ser = new XmlSerializer(typeof(ApplicationConfiguration));
                return (ApplicationConfiguration)ser.Deserialize(fs);
            }
        }

        public SerializerConfig getSerializerConfig()
        {
            return getApplicationConfiguration().SerializerConfig;
        }

        public LoggerConfig getLoggerConfig()
        {
            return getApplicationConfiguration().LoggerConfig;
        }

        public void subscribeConfigurationChange(FileSystemEventHandler fileChangeHandler)
        {
            fileWatcher.Changed += fileChangeHandler;
        }
    }
}
