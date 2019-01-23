using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore;

namespace TUIApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext.Init();
            TextualProcessor textualProcessor = new TextualProcessor();
            textualProcessor.run();
            ApplicationContext.CONTEXT.Dispose();
        }
    }
}
