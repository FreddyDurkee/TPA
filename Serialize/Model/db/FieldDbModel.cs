using System.ComponentModel.DataAnnotations;

namespace Serialize.Model.Db
{

    public class FieldDbModel
    {
        public FieldDbModel(string name, TypeDbModel type)
        {
            this.Name = name;
            this.Type = type;
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeDbModel Type { get; set; }
        public TypeDbModel Owner { get; set; }
        
        #endregion
    }
}
