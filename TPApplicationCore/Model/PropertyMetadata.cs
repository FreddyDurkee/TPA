﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPApplicationCore.Model
{
    [DataContract]
    public class PropertyMetadata
    {
        #region private
        [DataMember]
        private string name;
        [DataMember]
        private TypeMetadata type;
        [DataMember]
        private List<MethodMetadata> accessorList;
        #endregion

        public PropertyMetadata(string name, TypeMetadata propertyType)
        {
            this.name = name;
            type = propertyType;
            accessorList = new List<MethodMetadata>();
        }

        public TypeMetadata getType()
        {
            return type;
        }

        public string getName()
        {
            return name;
        }

        public List<MethodMetadata> getAccessorList()
        {
            return accessorList;
        }

        public bool anyChildren()
        {
            return accessorList.Count() > 0;
        }
    }
}
