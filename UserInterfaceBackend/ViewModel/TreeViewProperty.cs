using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;
using UIBackend.ViewModelAPI;

namespace UIBackend.ViewModel
{
    public class TreeViewProperty: TreeViewItem
    {

        private PropertyMetadata reflectionReference;

        protected override void createExtension()
        {
            foreach(MethodMetadata method in model.getAccessors(reflectionReference))
            {
                Children.Add(new TreeViewMethod(model, method));
            }
        }

        public TreeViewProperty(AssemblyMetadata model, PropertyMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = "pr:  " + reflectionReference.getName() + "  : " + model.getType(reflectionReference).getName();
        }
     
    }
}
