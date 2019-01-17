using DbLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{

    [TestClass]
    [DeploymentItem(@"Instrumentation\logTestDatabase.mdf", @"Instrumentation")]
    public class LogDbSerializationUnitTests
    {
        #region instrumentation
        private static string m_ConnectionString;
        #endregion

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string _logRelativeFilePath = @"Instrumentation\logTestDatabase.mdf";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _logDBPath = Path.Combine(_TestingWorkingFolder, _logRelativeFilePath);
            m_ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_logDBPath};Integrated Security = True; Connect Timeout = 30;";
        }

        [TestMethod]
        public void WhenCreateLogExpectCreateLogInDB()
        {
            int logNum;
            DbLogSaver dbLogSaver = new DbLogSaver(m_ConnectionString);
            dbLogSaver.Info(" FileLogSaver", "INFO log test!");

            using (SqlConnection con = new SqlConnection(m_ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(Id) FROM Log", con))
                {
                    logNum = (int) command.ExecuteScalar();
                }
            }
            Assert.AreEqual<int>(1, logNum);
        }
    }
}
