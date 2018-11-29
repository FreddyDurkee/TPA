using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPApplicationCore.ViewModel
{
    class SimpleBrowser : IBrowser
    {
        public string fileName { get => ""; set => fileName = ""; }

        public bool Browse()
        {
            return true;
        }
    }
}
