using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPAapplication.ModelAPI
{
    public interface ITypeMetadata
    {
        string getName();
        bool anyChildren();
    }
}
