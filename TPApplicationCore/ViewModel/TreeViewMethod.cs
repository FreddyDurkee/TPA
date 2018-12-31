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
    public class TreeViewMethod : TreeViewItem
    {
        private MethodMetadata reflectionReference;
        protected override void createExtension()
        {
            foreach (ParameterMetadata parameter in model.getParameters(reflectionReference))
            {
                Children.Add(new TreeViewParameter(model, parameter));
            }
        }


        public TreeViewMethod(AssemblyMetadata model, MethodMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = "m:  " + reflectionReference.getName() + "  : " + reflectionReference.getType().getName() ;
        }
       
    }
}
