﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPAapplication.Model;
using TPAapplication.ModelAPI;
using TPAapplication.ViewModelAPI;

namespace TPAapplication.ViewModel
{
    public class TreeViewItem : ITreeViewItem
    {
        public TreeViewItem(MetadataModel model, bool hasChildren)
        {
            if (hasChildren) {
                Children = new ObservableCollection<TreeViewItem>() { null };
            }
            else {
                Children = new ObservableCollection<TreeViewItem>();
            }
            this.model = model;
            this.m_WasBuilt = false;
        }
        public string Name { get; set; }
        public ObservableCollection<TreeViewItem> Children { get; set; }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                m_WasBuilt = true;
            }
        }
        protected MetadataModel model;

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        virtual protected void BuildMyself()
        {
            foreach (TypeMetadata c in model.getClasses())
            {
                Children.Add(new TreeViewType(model, c));
            }
        }

    }
}
