using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Serialize.Model.Db
{

    public class PropertyDbModel
    {
        public PropertyDbModel() { }
        public PropertyDbModel(string name, TypeDbModel type)
        {
            Name = name;
            Type = type;
            Accessors = new HashSet<MethodDbModel>();
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeDbModel Type { get; set; }
        public TypeDbModel OwnerType { get; set; }
        public ICollection<MethodDbModel> Accessors { get; set; }
        #endregion


    }
}
