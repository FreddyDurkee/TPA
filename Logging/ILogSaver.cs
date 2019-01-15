using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public interface ILogSaver
    {
        void Debug(string loggerName, string message);
        void Info(string loggerName, string message);
        void Warn(string loggerName, string message);
        void Error(string loggerName, string message);
        void Fatal(string loggerName, string message);
    }
}
