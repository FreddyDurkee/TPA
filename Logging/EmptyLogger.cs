using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class EmptyLogger : ILogSaver
    {
        public void Debug(string loggerName, string message)
        {
        }

        public void Error(string loggerName, string message)
        {
        }

        public void Fatal(string loggerName, string message)
        {
        }

        public void Info(string loggerName, string message)
        {
        }

        public void Warn(string loggerName, string message)
        {
        }
    }
}
