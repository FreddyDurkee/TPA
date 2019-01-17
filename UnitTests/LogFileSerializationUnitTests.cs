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


            Assert.IsTrue(File.Exists(_logFilePath));
        }
    }
}
