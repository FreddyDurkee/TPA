using AppConfiguration;
using AppConfiguration.Model;
using Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Serialization;

namespace TPApplicationCore
{
    public class ApplicationContext : IDisposable, ILogContextProvider
    {
        public static readonly ApplicationContext CONTEXT;

        static ApplicationContext(){
            try
            {
                CONTEXT = new ApplicationContext();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //Shutdown app
                throw ex;
            }

        }

        public SerializationManager SerializationManager { get; private set; }
        public LogContext LogContext { get; private set; }
        private ConfigurationManager AppConfManager;
        private CompositionContainer container;

        private ApplicationContext()
        {
            AppConfManager = new ConfigurationManager(@"./appconf.xml");
            SerializationManager = new SerializationManager();
            LogContext = new LogContext();
            ReloadContext();
            AppConfManager.SubscribeConfigurationChange(new FileSystemEventHandler(OnConfigChange));
        }

        private void OnConfigChange(object sender, FileSystemEventArgs e)
        {
            container.Dispose();
            ReloadContext();
        }

        private void ReloadContext()
        {
            SerializerConfig serConf = AppConfManager.GetSerializerConfig();
            LoggerConfig logConf = AppConfManager.GetLoggerConfig();
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(serConf.AssemblyCatalog, serConf.AssemblyName));
            catalog.Catalogs.Add(new DirectoryCatalog(logConf.AssemblyCatalog, logConf.AssemblyName));
            container = new CompositionContainer(catalog);
            foreach (String key in serConf.ConstructorArgs.Keys)
            {
                container.ComposeExportedValue(key, serConf.ConstructorArgs[key]);
            }
            foreach (String key in logConf.ConstructorArgs.Keys)
            {
                container.ComposeExportedValue(key, logConf.ConstructorArgs[key]);
            }
            container.ComposeParts(SerializationManager);
            container.ComposeParts(LogContext);
        }


        #region IDisposable
        public void Dispose()
        {
            container.Dispose();
        }
        #endregion

        #region ILogContextProvider
        public ILogSaver GetTPALogSaver()
        {
            return LogContext.LogSaver;
        }
        #endregion

        #region static_methods
        public static void Init()
        {
            //Do nothing. Just force static block.
        }

        public static TPALogger GetLogger(Type loggerClass)
        {
            return new TPALogger(loggerClass, CONTEXT);
        }
        #endregion
    }
}
