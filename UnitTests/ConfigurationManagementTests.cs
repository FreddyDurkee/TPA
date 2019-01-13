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
            config.SerializerConfig = new SerializerConfig();
            config.SerializerConfig.AssemblyCatalog = "C:/";
            config.SerializerConfig.AssemblyName = "assembly.dll";
            config.SerializerConfig.ConstructorArgs = new AppConfiguration.Model.generic.DictionaryStruct();
            config.SerializerConfig.ConstructorArgs.Add("fileName", "a.xml");
            ConfigurationManager manager = new ConfigurationManager(@"..\..\appconf.xml");
            manager.overrideFileConfig(config);
        }
    }
}
