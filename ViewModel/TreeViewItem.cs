using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.ViewModel
{
    public class TreeViewItem
    {
        public TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>() { null };
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

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        private void BuildMyself()
        {
            Random random = new Random();
            for (int i = 0; i < random.Next(7); i++)
                this.Children.Add(new TreeViewItem() { Name = "sample" + i });
        }

    }
}
