using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbSerialize.Model

{
    public class TypeDbModel
    {
        public TypeDbModel() { }
        public TypeDbModel(string name, string namespaceDef, bool isDefined)
        {
            Name = name;
            NamespaceDef = namespaceDef;
            this.isDefined = isDefined;
            Methods = new HashSet<MethodDbModel>();
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isDefined { get; set; }
        public ICollection<MethodDbModel> Methods { get; set; }
        public string NamespaceDef { get; internal set; }
        #endregion
    }
}
