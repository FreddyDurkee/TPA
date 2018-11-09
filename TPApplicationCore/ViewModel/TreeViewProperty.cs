using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TPApplicationCore.Model;
using TPApplicationCore.ViewModelAPI;

namespace TPApplicationCore.ViewModel
{
    public class TreeViewProperty: TreeViewItem
    {

        private PropertyMetadata reflectionReference;

        protected override void BuildMyself()
        {
            foreach(MethodMetadata method in model.getAccessors(reflectionReference))
            {
                Children.Add(new TreeViewMethod(model, method));
            }
        }

        public TreeViewProperty(MetadataModel model, PropertyMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = model.getClass(reflectionReference).getName() + " " + reflectionReference.getName();
        }
     
    }
}
