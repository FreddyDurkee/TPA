using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.Model
{
    public class FieldMetadata
    {
        #region private
        private string name;
        private TypeMetadata type;
        #endregion

        public FieldMetadata(string name, TypeMetadata type)
        {
            this.name = name;
            this.type = type;
        }

        public string getName()
        {
            return name;
        }
        public TypeMetadata getType()
        {
            return type;
        }

        public bool anyChildren()
        {
            return type.anyChildren();
        }
    }
}
