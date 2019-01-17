using FileLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class LogFileSerializationUnitTests
    {

        [TestMethod]
        public void WhenCreateLogExpectCreateLogFile()
        {
            string _logRelativeFilePath = @"Instrumentation\fileTestLog.xml";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _logFilePath = Path.Combine(_TestingWorkingFolder, _logRelativeFilePath);

            FileLogSaver fileLogSaver = new FileLogSaver(_logFilePath);
            fileLogSaver.Info(" FileLogSaver", "INFO log test!");


            Assert.IsTrue(File.Exists(_logFilePath));
        }

        [TestMethod]
        public void WhenCreateAllLogTypesExpectFileLenghtEqualsNumberOfLogs()
        {
            string _logRelativeFilePath = @"Instrumentation\fileTestLog.xml";
            string _TestingWorkingFolder = Environment.CurrentDirectory;
            string _logFilePath = Path.Combine(_TestingWorkingFolder, _logRelativeFilePath);

            FileLogSaver fileLogSaver = new FileLogSaver(_logFilePath);
            fileLogSaver.Info(" FileLogSaver", "INFO log test!");
            fileLogSaver.Debug(" FileLogSaver", "DEBUG log test!");
            fileLogSaver.Warn(" FileLogSaver", "WARN log test!");
            fileLogSaver.Fatal(" FileLogSaver", "FATAL log test!");
            fileLogSaver.Error(" FileLogSaver", "ERROR log test!");

            int lineCounter = 0;
            using (var stream = File.Open(_logFilePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCounter++;
                    }
                    reader.Close();
                }
            }
            //FileInfo fileInfo = new FileInfo(_logFilePath);  
            Assert.AreEqual(5, lineCounter);
        }
    }
}
