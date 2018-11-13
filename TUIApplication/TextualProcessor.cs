﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.ViewModel;

namespace TUIApplication
{
    class TextualProcessor
    {
        private ViewModel viewModel = new ViewModel();
        private UserNotifier userNotifier = new UserNotifier();

        public void run()
        {
            userNotifier.showMessage(Properties.Resources.welcomeMsg);
            string path;
            while (getAsseblyPath(out path))
            {
                if (!String.IsNullOrEmpty(path)) {
                    handleModelExploration(path);
                }
            }
            userNotifier.showMessage(Properties.Resources.goodbye);
        }

        private void handleModelExploration(string path)
        {
            viewModel.PathVariable = path;
            viewModel.LoadDLL();
            Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
            
            stack.Push(viewModel.HierarchicalAreas.ElementAt(0) ) ;
            stack.Peek().IsExpanded = true;
            userNotifier.showTreeViewItem(stack.Peek());

            ConsoleKeyInfo key;
            
            while (stack.Count != 0 && (key = Console.ReadKey()).Key != ConsoleKey.Escape)
            {
                string decision = key.KeyChar + Console.ReadLine();
                switch (decision) {
                    case "-":
                        stack.Pop();
                        break;
                    default:
                        int decisionNumber = 0;
                        bool resut = int.TryParse(decision, out decisionNumber);
                        if (decisionNumber < 0 || decisionNumber > stack.Peek().Children.Count || resut == false)
                        {
                            userNotifier.warnUser(Properties.Resources.incorrectInput);
                        }
                        else
                        {
                            stack.Push(stack.Peek().Children.ElementAt(decisionNumber));
                            stack.Peek().IsExpanded = true;
                        }
                        break;
                }
                if(stack.Count != 0) {
                    userNotifier.showTreeViewItem(stack.Peek());
                }
                
            }
        }


        private bool getAsseblyPath(out string path)
        {
            userNotifier.askData(Properties.Resources.askDLLPath);
            ConsoleKeyInfo key = Console.ReadKey();
            if(key.Key != ConsoleKey.Escape)
            {
                path = key.KeyChar + Console.ReadLine();
                return true;
            }
            else
            {
                path = "";
                return false;
            }

        }
    }
}
