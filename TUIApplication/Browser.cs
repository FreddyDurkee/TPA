using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUIApplication
{
    class Browser : TPApplicationCore.ViewModel.IBrowser
    {
        private UserNotifier userNotifier = new UserNotifier();

        public string fileName { get ; set ; }

        public bool Browse()
        {
            try
            {
                userNotifier.askData(Properties.Resources.askDLLPath);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter)
                {
                    fileName = key.KeyChar + Console.ReadLine();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                userNotifier.NewLine();
            }
        }
    }
}
