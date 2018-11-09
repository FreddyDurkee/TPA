using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TPApplicationCore.Logging
{

    class Logger
    {
        private static TraceSource source = new TraceSource("TPApplicationLogger");

        public static void log(TraceEventType eventType, int id, object data) {
            source.TraceData(eventType, id, data);
        }
        public static void log(TraceEventType eventType, String msg)
        {
            source.TraceData(eventType, (int)eventType, msg);
        }
    }
}
