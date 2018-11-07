using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.Model

{
    class TypeMetadata
    {
        #region private
        private List<MethodMetadata> methods;
        private List<PropertyMetadata> properties;
        private List<FieldMetadata> fields;
        private string name;
        #endregion

        public TypeMetadata(string name)
        {
            methods = new List<MethodMetadata>();
            properties = new List<PropertyMetadata>();
            fields = new List<FieldMetadata>();
            this.name = name;
        }

        public List<MethodMetadata> getMethodsList()
        {
            return methods;
        }

        public List<PropertyMetadata> getPropertiesList()
        {
            return properties;
        }

        public List<FieldMetadata> getFieldsList()
        {
            return fields;
        }

        public bool anyChildren()
        {
            if (methods.Count() != 0)
                return true;
            if (fields.Count() != 0)
                return true;
            if (properties.Count() != 0)
                return true;
            return false;
        }
    }
}
