﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.Model;
using UIBackend.ViewModelAPI;

namespace UIBackend.ViewModel
{
    public class TreeViewParameter:TreeViewItem
    {

        
        private ParameterMetadata reflectionReference;
        protected override void createExtension()
        {
            TypeMetadata classReference = model.getType(reflectionReference);
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

        public TreeViewParameter(AssemblyMetadata model, ParameterMetadata reflectionReference):base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = "p:  " + reflectionReference.getName() + "  : " + model.getType(reflectionReference).getName()  ;
        }
      
    }
}
