using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using TPApplicationCore.Model;
using TPApplicationCore.Logging;
using Serialize.Api;
using Serialize;

namespace UIBackend.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region DataContext
        public System.Collections.ObjectModel.ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private Dictionary<string, AssemblyMetadata> connectedModels;
        private IFileSerializer xmlSerializer = new XMLSerializer();
        private IDBSerializer dBSerializer = new DBSerializer();
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Serialize { get; }
        public ICommand Click_ShowTreeView { get; }
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
        }

        public ViewModel()
        {
            Browser = new SimpleBrowser();
            connectedModels = new Dictionary<string, AssemblyMetadata>();
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_ShowTreeView = new DelegateCommand(LoadDLL);
            Click_Browse = new DelegateCommand(Browse);
            Click_Serialize = new DelegateCommand(Serialize);
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
            else if((PathVariable.Substring(PathVariable.Length - 4) == ".xml"))
            {
                //AssemblyMetadata model = xmlSerializer.deserialize(PathVariable);
                AssemblyMetadata model = dBSerializer.deserialize(1);
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
            if (connectedModels.TryGetValue(PathVariable, out tmp_model))
            {
                //xmlSerializer.serialize(tmp_model, PathVariable);
                dBSerializer.serialize(tmp_model);
            }

        }
        #endregion

        #region private
        private void TreeViewLoaded(AssemblyMetadata model)
        {

            TreeViewItem rootItem = new TreeViewItem(model,true) { Name = model.name };
            Logger.log(System.Diagnostics.TraceEventType.Information, "New model loaded:" + rootItem.Name);
            HierarchicalAreas.Add(rootItem);
            if (!connectedModels.ContainsKey(PathVariable))
            {
                connectedModels.Add(PathVariable, model);
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