using Aplikacja.Model;
using Aplikacja.ModelAPI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Aplikacja
{
    class MyViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<ITreeViewItem> HierarchicalAreas { get; set; }
        private Dictionary<string, MyModel> connectedModels;

        public MyViewModel()
        {
            HierarchicalAreas = new ObservableCollection<ITreeViewItem>();
            connectedModels = new Dictionary<string, MyModel>();      
        }

        private string pathVariable;
       
        public string PathVariable
        {
            get { return pathVariable; }
            set { pathVariable = value; }
        }
 

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName_)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName_));
            }
        }

        private Visibility _visibility = Visibility.Hidden;

        public Visibility ChangeControlVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
            }
        }

        public ICommand Click_Browse
        {
            get { return new DelegateCommand(Browse); }
        }

        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog();
            test.Filter = "Dynamic Library File(*.dll)|*.dll|XML file (*.xml)|*.xml";
            test.ShowDialog();


            if(test.FileName.Length==0)
            {
                MessageBox.Show("No files selected");
            }
            else
            {
                pathVariable =test.FileName;
                _visibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");

            }
        }

        public ICommand Click_Button
        {
            get { return new DelegateCommand(LoadDLL); }
        }
        public ICommand Click_Serialize { get { return new DelegateCommand(SerializeDLL); } }

        private void SerializeDLL()
        {
            MyModel tmp_model;
            if(connectedModels.TryGetValue(pathVariable, out tmp_model))
            {
                FileStream fs = new FileStream(pathVariable.Substring(0,pathVariable.Length-3)+"xml", FileMode.Create);
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
                NetDataContractSerializer ser = new NetDataContractSerializer();
                ser.WriteObject(writer, tmp_model);
                writer.Close();
            }
            else
            {
                MessageBox.Show("No such model loaded. Load first.");
            }
        }

        private void LoadDLL()
        {
            
            try
            {
                    MyModel model = null;
                    if (!connectedModels.TryGetValue(pathVariable, out model))
                    {
                    if (pathVariable.Substring(pathVariable.Length - 4) == ".dll")
                        model = new MyModel(pathVariable);
                    else
                    {
                        FileStream fs = new FileStream(pathVariable, FileMode.Open);
                        Console.WriteLine(pathVariable+fs.CanRead);
                        XmlDictionaryReader reader =XmlDictionaryReader.CreateTextReader(fs, XmlDictionaryReaderQuotas.Max);
                        NetDataContractSerializer ser = new NetDataContractSerializer();
                        model = (MyModel)ser.ReadObject(reader,true);
                        reader.Close();
                    }
                        connectedModels.Add(pathVariable, model);
                    }
                    TreeViewLoaded(model);
               

            }
            catch (BadImageFormatException)
            {
                MessageBox.Show("File cannot be opened");
            }
          
           
        }

        private void TreeViewLoaded(IMyModel model)
        {
            HierarchicalAreas.Add(new TreeViewDll(model, pathVariable.Substring(pathVariable.LastIndexOf('\\') + 1)));
        }
        

    }
}
