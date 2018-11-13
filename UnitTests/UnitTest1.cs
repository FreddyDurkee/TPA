using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPApplicationCore.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Should_ReturnFalse_When_DLLFileIsUnproperlyLoaded()
        {

            string path = System.IO.Path.Combine(Environment.CurrentDirectory,
                                     @"..\..\library.dll");
            MetadataModel test = new MetadataModel(path);
            Assert.IsNotNull(test);
            Assert.IsTrue(test.getTypes().Count > 0);
        }

        [TestMethod]
        public void Should_Pass_When_LoadedFileIsDLL()
        {

            string path = System.IO.Path.Combine(Environment.CurrentDirectory,
                                     @"..\..\library.dll");
            MetadataModel test = new MetadataModel(path);
            Assert.AreEqual(".dll", path.Substring(path.Length - 4));
        }

        [TestMethod]
        public void Should_5_BeEqualNumberOfTypes()
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory,
                                    "..\..\library.dll");
            MetadataModel test = new MetadataModel(path);
            Assert.AreEqual(5, test.typesList.Count);
        }

        [TestMethod]
        public void Should_1_BeEqualNumberOfPropertiesForTypeServiceA()
        {
            string path = System.IO.Path.Combine(Environment.CurrentDirectory,
                                    @"..\..\library.dll");
            MetadataModel test = new MetadataModel(path);
            Assert.AreEqual(1, test.getProperties(test.typesList["ServiceA"]).Count);
        }
    }
}
