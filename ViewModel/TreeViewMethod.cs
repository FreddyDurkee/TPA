using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TPAapplication.Model;
using TPAapplication.ViewModelAPI;

namespace TPAapplication.ViewModel
{
    public class TreeViewMethod : TreeViewItem
    {
        private MethodMetadata reflectionReference;
        protected override void BuildMyself()
        {
            foreach (ParameterMetadata parameter in model.getParameters(reflectionReference))
            {
                Children.Add(new TreeViewParameter(model, parameter));
            }
        }


        public TreeViewMethod(MetadataModel model, MethodMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = reflectionReference.getType().getName() + " " + reflectionReference.getName();
        }
       
    }
}
