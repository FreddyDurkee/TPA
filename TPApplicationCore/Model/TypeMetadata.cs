using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace TPApplicationCore.Model
{
    public class TypeMetadata
    {
        #region private
        private List<MethodMetadata> methods;
        private List<PropertyMetadata> properties;
        private List<FieldMetadata> fields;
        private string name;
        private string namespaceDef;
        #endregion


        public TypeMetadata(string name,string namespaceDef)
        {
            methods = new List<MethodMetadata>();
            properties = new List<PropertyMetadata>();
            fields = new List<FieldMetadata>();
            this.name = name;
            this.namespaceDef = namespaceDef;
        }

        public string getName()
        {
            return name;
        }

        public string getNamespaceDef()
        {
            return namespaceDef;
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
