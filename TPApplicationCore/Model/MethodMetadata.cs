using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPApplicationCore.Model
{
    public class MethodMetadata
    {
        #region private
        private string name;
        private TypeMetadata returnType;
        private List<ParameterMetadata> parameters;
        #endregion

        public MethodMetadata(string name, TypeMetadata type)
        {
            this.name = name;
            returnType = type;
            parameters = new List<ParameterMetadata> ();
        }

        public List<ParameterMetadata> getParameterList()
        {
            return parameters;
        }

        public string getName()
        {
            return name;
        }

        public TypeMetadata getType()
        {
            return returnType;
        }

        public bool anyChildren()
        {
            if (parameters.Count() != 0)
                return true;
            else
                return false;
        }
    }
}