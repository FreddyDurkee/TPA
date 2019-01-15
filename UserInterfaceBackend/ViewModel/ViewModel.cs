using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TPApplicationCore.Model;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using TPApplicationCore.Serialization;
using AppConfiguration;
using AppConfiguration.Model;
using Logging;

namespace UIBackend.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        #region DataContext
        private static TPALogger LOGGER = new TPALogger(typeof(ViewModel));

        public System.Collections.ObjectModel.ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        private Dictionary<string, AssemblyMetadata> connectedModels;
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Serialize { get; }
        public ICommand Click_ShowTreeView { get; }
        public ICommand Click_Deserialize { get; }
        public IBrowser Browser { get; set; }
        private ConfigurationManager appConfManager;
        private SerializationManager serManager;


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
            appConfManager = new ConfigurationManager(@"./appconf.xml");
            serManager = ComposeSerializer();
            TPALogManager.reloadSingletone(ComposeLogManager());
            appConfManager.subscribeConfigurationChange(new FileSystemEventHandler(onConfigChange));
        }

        private void onConfigChange(object sender, FileSystemEventArgs e)
        {
            serManager = ComposeSerializer();
            TPALogManager.reloadSingletone(ComposeLogManager());
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
                ComposeSerializer().serialize(tmp_model);
            }

        }

 
        public void Deserialize()
        {
            TreeViewLoaded(ComposeSerializer().deserialize());
        }

        #endregion

        #region private
        private void TreeViewLoaded(AssemblyMetadata model)
        {
            TreeViewItem rootItem = new TreeViewItem(model,true) { Name = model.name };
            LOGGER.Info( "New model loaded:" + rootItem.Name);
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

        private SerializationManager ComposeSerializer()
        {
                SerializationManager manager = new SerializationManager();
                SerializerConfig serConf = appConfManager.getSerializerConfig();
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(serConf.AssemblyCatalog, serConf.AssemblyName));
                CompositionContainer container = new CompositionContainer(catalog);
                foreach(String key in serConf.ConstructorArgs.Keys)
                {
                    container.ComposeExportedValue(key, serConf.ConstructorArgs[key]);
                }
                container.ComposeParts(manager);
                return manager;
        }

        private TPALogManager ComposeLogManager()
        {
            TPALogManager manager = new TPALogManager();
            LoggerConfig logConf = appConfManager.getLoggerConfig();
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(logConf.AssemblyCatalog, logConf.AssemblyName));
            CompositionContainer container = new CompositionContainer(catalog);
            foreach (String key in logConf.ConstructorArgs.Keys)
            {
                container.ComposeExportedValue(key, logConf.ConstructorArgs[key]);
            }
            container.ComposeParts(manager);
            return manager;
        }
        #endregion
    }
}