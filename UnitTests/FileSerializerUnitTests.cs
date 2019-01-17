using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;
using Serialize;
using TPApplicationCore.Serialization;

namespace UnitTests
{
    [TestClass]
    [DeploymentItem(@"Instrumentation\TPA.ApplicationArchitecture.dll", @"Instrumentation")]
    public class FileSerializerUnitTests
    {

        [TestMethod]
        public void WhenSerializedExpectAsAResultRecivedNotNullValue()
        {
            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);

            AssemblyMetadata assembly = new AssemblyMetadata(_DllPath);
            SerializationManager serializationManager = new SerializationManager();
            serializationManager.Serializer = new XMLSerializer("xmlSerialization.xml");
            serializationManager.serialize(assembly);

            Assert.IsNotNull(serializationManager.deserialize());

        }
    }
}
