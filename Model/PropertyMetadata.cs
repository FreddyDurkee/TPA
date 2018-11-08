using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.Model
{
    class PropertyMetadata
    {
        #region private
        private string name;
        private TypeMetadata type;
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

        public bool anyChildren()
        {
            return accessorList.Count() > 0;
        }
    }
}
