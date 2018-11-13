using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;
using TPApplicationCore.ViewModelAPI;

namespace TPApplicationCore.ViewModel
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
                createExtension();
                m_WasBuilt = true;
            }
        }
        protected MetadataModel model;

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        virtual protected void createExtension()
        {
            foreach (TypeMetadata c in model.getTypes())
            {
                Children.Add(new TreeViewType(model, c));
            }
        }

    }
}
