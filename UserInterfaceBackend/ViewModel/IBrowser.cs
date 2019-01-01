using System;
using System.Collections.Generic;
using System.Text;

namespace UIBackend.ViewModel
{
    public interface IBrowser
    {
        bool Browse();
        string fileName { get; set; }
    }
}
