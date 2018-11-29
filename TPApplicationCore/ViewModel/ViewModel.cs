using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using TPApplicationCore.Model;

namespace TPApplicationCore.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region DataContext
        public System.Collections.ObjectModel.ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private Dictionary<string, MetadataModel> connectedModels;
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
            Click_ShowTreeView = new DelegateCommand(LoadDLL);
            Click_Browse = new DelegateCommand(Browse);
            Click_Serialize = new DelegateCommand(Serialize);
        }

        public ViewModel()
        {
            Browser = new SimpleBrowser();
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
                TreeViewLoaded();
        }

        public void Serialize()
        {
            MetadataModel tmp_model;
            if (connectedModels.TryGetValue(PathVariable, out tmp_model))
            {
                FileStream fs = new FileStream(PathVariable.Substring(0, PathVariable.Length - 3) + "xml", FileMode.Create);
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
                NetDataContractSerializer ser = new NetDataContractSerializer();
                ser.WriteObject(writer, tmp_model);
                writer.Close();
            }

        }
        #endregion

        #region private
        private void TreeViewLoaded()
        {
            MetadataModel model = new MetadataModel(PathVariable);
            TreeViewItem rootItem = new TreeViewItem(model,true) { Name = PathVariable.Substring(PathVariable.LastIndexOf('\\') + 1) };
            Logging.Logger.log(System.Diagnostics.TraceEventType.Information, "New model loaded:" + rootItem.Name);
            HierarchicalAreas.Add(rootItem);
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
