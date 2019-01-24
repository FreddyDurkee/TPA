using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPApplicationCore.Model;

namespace UnitTests
{
    [TestClass]
    [DeploymentItem(@"Instrumentation\TPA.ApplicationArchitecture.dll", @"Instrumentation")]
    public class CoreUnitTests
    {
        [TestMethod]
        public void Should_ReturnFalse_When_DLLFileIsUnproperlyLoaded()
        {
            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);

            AssemblyMetadata test = new AssemblyMetadata(_DllPath);
            Assert.IsNotNull(test);
            Assert.IsTrue(test.getTypes().Count > 0);
        }

        [TestMethod]
        public void Should_Pass_When_LoadedFileIsDLL()
        {

            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);
            AssemblyMetadata test = new AssemblyMetadata(_DllPath);
            Assert.AreEqual(".dll", _DllPath.Substring(_DllPath.Length - 4));
        }

        [TestMethod]
        public void Should_20_BeEqualNumberOfTypes()
        {
            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);
            AssemblyMetadata test = new AssemblyMetadata(_DllPath);
            Assert.AreEqual(20, test.typesList.Count);
        }

        [TestMethod]
        public void Should_1_BeEqualNumberOfPropertiesForTypeServiceA()
        {
            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);
            AssemblyMetadata test = new AssemblyMetadata(_DllPath);
            Assert.AreEqual(1, test.getProperties(test.typesList["ServiceA"]).Count);
        }
    }
}
