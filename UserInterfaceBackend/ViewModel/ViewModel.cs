using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TPApplicationCore.Model;

namespace UIBackend.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region DataContext
        public System.Collections.ObjectModel.ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private Dictionary<string, AssemblyMetadata> connectedModels;
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Serialize { get; }
        public ICommand Click_ShowTreeView { get; }
        public ICommand Click_Deserialize { get; }
        public IBrowser Browser { get; set; }


        #endregion

        #region constructor
        public ViewModel(IBrowser browser)
        {
            Browser = browser;
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            connectedModels = new Dictionary<string, AssemblyMetadata>();
            Click_ShowTreeView = new DelegateCommand(LoadDLL);
            Click_Browse = new DelegateCommand(Browse);
            Click_Serialize = new DelegateCommand(Serialize);
            Click_Deserialize = new DelegateCommand(Deserialize);
        }



        public ViewModel(): this(new SimpleBrowser())
        {   
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region public
        public void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
            {
                AssemblyMetadata model = new AssemblyMetadata(PathVariable);
                TreeViewLoaded(model);
            }
            else
            {
                Console.WriteLine("Incorrect path ", PathVariable);
            }
        }


        public void Serialize()
        {
            AssemblyMetadata tmp_model;
            string filename = Path.GetFileName(PathVariable);
            string modelName = filename.Substring(0, filename.Length - 4);
            if (connectedModels.TryGetValue(modelName, out tmp_model))
            {
                tmp_model.Serialize();
            }

        }

 
        public void Deserialize()
        {
            TreeViewLoaded(AssemblyMetadata.Deserialize());
        }

        #endregion

        #region private
        private void TreeViewLoaded(AssemblyMetadata model)
        {
            TreeViewItem rootItem = new TreeViewItem(model,true) { Name = model.name };
            HierarchicalAreas.Add(rootItem);
            if (!connectedModels.ContainsKey(model.name))
            {
                connectedModels.Add(model.name, model);
            }
        }

        private void Browse()
        {
            if (Browser.Browse())            
            {
                PathVariable = Browser.fileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }

        #endregion
    }
}