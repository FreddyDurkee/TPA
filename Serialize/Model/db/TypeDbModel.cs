using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Serialize.Model.Db

{
    public class TypeDbModel
    {
        public TypeDbModel(string name, bool isDefined)
        {
            Name = name;
            isDefined = isDefined;
            Methods = new HashSet<MethodDbModel>();
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isDefined;
        public ICollection<MethodDbModel> Methods { get; set; }
        #endregion
    }
}
