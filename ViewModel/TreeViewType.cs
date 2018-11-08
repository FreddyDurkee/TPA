﻿using System;
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
    public class TreeViewType : TreeViewItem
    {
        private TypeMetadata reflectionReference;

        protected override void BuildMyself()
        {
            foreach (FieldMetadata field in model.getFields(reflectionReference))
            {
                Children.Add(new TreeViewField(model, field));
            }
            foreach (PropertyMetadata property in model.getProperties(reflectionReference))
            {
                Children.Add(new TreeViewProperty(model, property));
            }
            foreach (MethodMetadata method in model.getMethods(reflectionReference))
            {
                Children.Add(new TreeViewMethod(model, method));
            }
        }
        
       
        public TreeViewType(MetadataModel model, TypeMetadata reflectionReference) : base(model, reflectionReference.anyChildren())
        {
            this.reflectionReference = reflectionReference;
            Name = reflectionReference.getName();
        }
      
    }
}
