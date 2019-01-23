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
        private string ConfPath;
        private FileSystemWatcher FileWatcher;

        public ConfigurationManager(string confPath)
        {
            this.ConfPath = confPath;
          
            FileWatcher = new FileSystemWatcher(Path.GetDirectoryName(confPath));
            FileWatcher.Filter = Path.GetFileName(confPath);
            FileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            FileWatcher.EnableRaisingEvents = true;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            using (FileStream fs = new FileStream(ConfPath, FileMode.Open)) {
                XmlSerializer ser = new XmlSerializer(typeof(ApplicationConfiguration));
                return (ApplicationConfiguration)ser.Deserialize(fs);
            }
        }

        public SerializerConfig GetSerializerConfig()
        {
            return GetApplicationConfiguration().SerializerConfig;
        }

        public LoggerConfig GetLoggerConfig()
        {
            return GetApplicationConfiguration().LoggerConfig;
        }

        public void SubscribeConfigurationChange(FileSystemEventHandler fileChangeHandler)
        {
            FileWatcher.Changed += fileChangeHandler;
        }
    }
}
