using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class TPALogManager
    {
        public static TPALogManager LogManager { get; private set; }

        static TPALogManager(){
            LogManager = new TPALogManager();
        }
        
        public TPALogManager()
        {
            LogSaver = new EmptyLogger();
        }

        [Import]
        public ILogSaver LogSaver { get; private set; }

        public static void reloadSingletone(TPALogManager manager)
        {
            LogManager = manager;
        }
    }
}
