﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPApplicationCore.ViewModelAPI
{
    public interface ITreeViewItem
    {
        string Name { get; set; }
        bool IsExpanded { get; set; }
    }
}
