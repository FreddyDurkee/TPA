﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPApplicationCore.ViewModel;

namespace TPAapplication
{
    class Browser : TPApplicationCore.ViewModel.IBrowser
    {
        bool IBrowser.Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return false;
            }
            fileName = test.FileName;
            return true;

        }

        public string fileName { get; set; }

    }
}
    

