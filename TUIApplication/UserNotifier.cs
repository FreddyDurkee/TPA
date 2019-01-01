using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIBackend.ViewModel;

namespace TUIApplication
{
    class UserNotifier
    {
        public void showMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void askData(string msg)
        {
            Console.Write(msg);
        }

        public void showTreeViewItem(TreeViewItem item)
        {
            for (int i = 0; i < item.Children.Count; i++)
            {
                Console.WriteLine("[" + i + "] " + item.Children.ElementAt(i).Name);
            }
            Console.WriteLine("[-] Get back.");
        }

        public void warnUser(string msg)
        {
            Console.WriteLine("|Warning| " + msg);
        }

        internal void NewLine()
        {
            Console.WriteLine();
        }
    }
}
