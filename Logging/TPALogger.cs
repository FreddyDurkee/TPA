using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class TPALogger
    {
        private readonly string loggerName;

        public TPALogger(Type loggerClass)
        {
            this.loggerName = loggerClass.FullName;
        }

        public void Debug(string message)
        {
            TPALogManager.LogManager.LogSaver.Debug(loggerName, message);
        }

        public void Error(string message)
        {
            TPALogManager.LogManager.LogSaver.Error(loggerName, message);
        }

        public void Fatal(string message)
        {
            TPALogManager.LogManager.LogSaver.Fatal(loggerName, message);
        }

        public void Info(string message)
        {
            TPALogManager.LogManager.LogSaver.Info(loggerName, message);
        }

        public void Warn(string message)
        {
            TPALogManager.LogManager.LogSaver.Warn(loggerName, message);
        }
    }
}
