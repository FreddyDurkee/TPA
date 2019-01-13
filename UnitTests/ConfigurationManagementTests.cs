using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppConfiguration.Model;
using AppConfiguration;

namespace UnitTests
{
    [TestClass]
    public class ConfigurationManagementTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            ApplicationConfiguration config = new ApplicationConfiguration();
            config.serializerConfig = new SerializerConfig();
            config.serializerConfig.AssemblyCatalog = "C:/";
            config.serializerConfig.AssemblyName = "assembly.dll";
            config.serializerConfig.constructorArgs = new AppConfiguration.Model.generic.DictionaryStruct();
            config.serializerConfig.constructorArgs.Add("fileName", "a.xml");
            ConfigurationManager manager = new ConfigurationManager(@"..\..\appconf.xml");
            manager.overrideFileConfig(config);
        }
    }
}
