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
        private readonly ILogContextProvider contextProvider;

        public TPALogger(Type loggerClass, ILogContextProvider contextProvider)
        {
            this.loggerName = loggerClass.FullName;
            this.contextProvider = contextProvider;
        }

        public void Debug(string message)
        {
            contextProvider.GetTPALogSaver().Debug(loggerName, message);
        }

        public void Error(string message)
        {
            contextProvider.GetTPALogSaver().Error(loggerName, message);
        }

        public void Fatal(string message)
        {
            contextProvider.GetTPALogSaver().Fatal(loggerName, message);
        }

        public void Info(string message)
        {
            contextProvider.GetTPALogSaver().Info(loggerName, message);
        }

        public void Warn(string message)
        {
            contextProvider.GetTPALogSaver().Warn(loggerName, message);
        }
    }
}
