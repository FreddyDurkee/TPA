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

        private void createExtension()
        {
            TypeMetadata typeReference = model.getType(reflectionReference);
            if (typeReference == null)
                return;
            foreach (FieldMetadata field in model.getFields(typeReference))
            {
                Children.Add(new TreeViewField(model, field));
            }
            foreach (PropertyMetadata property in model.getProperties(typeReference))
            {
                Children.Add(new TreeViewProperty(model, property));
            }
            foreach (MethodMetadata method in model.getMethods(typeReference))
            {
                Children.Add(new TreeViewMethod(model, method));
            }
        }

        public TreeViewField(MetadataModel model, FieldMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = model.getType(reflectionReference).getName() + " " + reflectionReference.getName();              
        }
     
    }
}
