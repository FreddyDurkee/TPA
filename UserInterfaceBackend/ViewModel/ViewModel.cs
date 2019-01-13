using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TPApplicationCore.Model;
using TPApplicationCore.Logging;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using TPApplicationCore.Serialization;
using AppConfiguration;
using AppConfiguration.Model;

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
        public ICommand Click_SaveToDb { get; }
        public ICommand Click_Deserialize { get; }
        public ICommand Click_SerializeSource { get; }
        public IBrowser Browser { get; set; }
        public SerializationManager SerializationManager { get; set; }
        private bool isXMLSerializer = false;
        private ConfigurationManager appConfManager = new ConfigurationManager(@"./appconf.xml");


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
            Click_SerializeSource = new RelayCommand(SetSerializeSource);
            SerializationManager = new SerializationManager();
        }

        private void SetSerializeSource(object obj)
        {
            string checkBoxName = (string)obj;
            if ("xml".Equals(checkBoxName))
            {
                Compose("Serialize.dll");
                isXMLSerializer = true;
            }
            else
            {
                Compose("DbSerialize.dll");
                isXMLSerializer = false;
            }
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
                SerializationManager.serialize(tmp_model,PathVariable);
            }

        }

 
        public void Deserialize()
        {
            AssemblyMetadata model = null;
            if (isXMLSerializer)
            {
                if (Browser.Browse()) {
                    model = SerializationManager.deserialize(Browser.fileName);
                }
            }
            else
            {
                model = SerializationManager.deserialize("");
            }
            TreeViewLoaded(model);
        }

        #endregion

        #region private
        private void TreeViewLoaded(AssemblyMetadata model)
        {

            TreeViewItem rootItem = new TreeViewItem(model,true) { Name = model.name };
            Logger.log(System.Diagnostics.TraceEventType.Information, "New model loaded:" + rootItem.Name);
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

        private void Compose(string dll)
        {
            SerializerConfig serConf = appConfManager.getSerializerConfig();
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(serConf.AssemblyCatalog, serConf.AssemblyName));
            CompositionContainer container = new CompositionContainer(catalog);
            foreach(String key in serConf.constructorArgs.Keys)
            {
                container.ComposeExportedValue(key, serConf.constructorArgs[key]);
            }
            container.ComposeParts(SerializationManager);
        }
        #endregion
    }
}