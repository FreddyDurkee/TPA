using Logging;
using System.ComponentModel.Composition;

namespace TPApplicationCore
{
    public class LogContext
    {
        [Import]
        public ILogSaver LogSaver { get; private set; }

        public LogContext()
        {
            LogSaver = new EmptyLogger();
        }
    }
}
