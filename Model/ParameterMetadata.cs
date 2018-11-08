using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.Model
{
    class ParameterMetadata
    {
        #region private
        private string name;
        private TypeMetadata type;
        #endregion

        public ParameterMetadata(string name, TypeMetadata type)
        {
            this.name = name;
            this.type = type;
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
            return type.anyChildren();
        }
    }
}
