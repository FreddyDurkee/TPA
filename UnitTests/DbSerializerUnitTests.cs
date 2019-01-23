using DataTransferGraph.Api;
using DbSerialize;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Converter;
using TPApplicationCore.Model;
using TPApplicationCore.Serialization;
using  DbSerialize;
using static DbSerialize.DBSerializer;
using DbSerialize.Model;

namespace UnitTests
{
    [TestClass]
    [DeploymentItem(@"Instrumentation\TPA.ApplicationArchitecture.dll", @"Instrumentation")]
    [DeploymentItem(@"Instrumentation\testDatabase.mdf", @"Instrumentation")]
    public class DbSerializerUnitTests
    {
        #region instrumentation
        private static string m_ConnectionString;
        private static SqlProviderServices instance = SqlProviderServices.Instance;
        #endregion

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _DBRelativePath = @"Instrumentation\testDatabase.mdf";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DBPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);
            FileInfo _databaseFile = new FileInfo(_DBPath);
            m_ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestMethod]
        public void serializeDllToDb()
        {
            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);

            AssemblyMetadata assembly = new AssemblyMetadata(_DllPath);
            SerializationManager serializationManager = new SerializationManager();
            serializationManager.Serializer = new DBSerializer(m_ConnectionString);
            serializationManager.Serialize(assembly);

            Assert.IsNotNull(serializationManager.Deserialize());

        }

        [TestMethod]
        public void WhenSeializeExpectFilledDB()
        {
            int typesNumber;

            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);

            AssemblyMetadata assembly = new AssemblyMetadata(_DllPath);
            SerializationManager serializationManager = new SerializationManager();
            serializationManager.Serializer = new DBSerializer(m_ConnectionString);
            serializationManager.Serialize(assembly);

            using (var ctx = new SerializationContext(m_ConnectionString, false))
            {
                typesNumber = ctx.Assemblies.Where(a => a.Id == 1)
                .SelectMany(a => a.Types)
                .Count();

            }

            Assert.IsNotNull(serializationManager.Deserialize());
            Assert.AreEqual<int>(32, typesNumber);
        }

        [TestMethod]
        public void WhenSeializeExpectCorrectFilledAssamblyName()
        {
            AssemblyDbModel assemblyItem;

            string _DBRelativePath = @"Instrumentation\TPA.ApplicationArchitecture.dll";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _DllPath = Path.Combine(_TestingWorkingFolder, _DBRelativePath);

            AssemblyMetadata assembly = new AssemblyMetadata(_DllPath);
            DBSerializer serializer = new DBSerializer(m_ConnectionString);
            ModelToDTGConverter converter = new ModelToDTGConverter();
            serializer.Serialize(converter.ToDTG(assembly));
            using (var ctx = new SerializationContext(m_ConnectionString, false))
            {
                assemblyItem = ctx.Assemblies.Where(a => a.Id == 1)
                    .FirstOrDefault();
            }

            Assert.IsNotNull(serializer.Deserialize());
            Assert.AreEqual<String>("TPA.ApplicationArchitecture", assemblyItem.Name);
        }

    }
}
