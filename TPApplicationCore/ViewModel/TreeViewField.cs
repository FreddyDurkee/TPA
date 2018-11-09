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
    public class TreeViewField : TreeViewItem
{

        private FieldMetadata reflectionReference;

        private void buildMyself()
        {
            TypeMetadata classReference = model.getClass(reflectionReference);
            if (classReference == null)
                return;
            foreach (FieldMetadata field in model.getFields(classReference))
            {
                Children.Add(new TreeViewField(model, field));
            }
            foreach (PropertyMetadata property in model.getProperties(classReference))
            {
                Children.Add(new TreeViewProperty(model, property));
            }
            foreach (MethodMetadata method in model.getMethods(classReference))
            {
                Children.Add(new TreeViewMethod(model, method));
            }
        }

        public TreeViewField(MetadataModel model, FieldMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = model.getClass(reflectionReference).getName() + " " + reflectionReference.getName();              
        }
     
    }
}
