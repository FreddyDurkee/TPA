﻿using System.ComponentModel.Composition;

namespace Logging
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
