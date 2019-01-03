using System.ComponentModel.DataAnnotations;

namespace DbSerialize.Model
{

    public class FieldDbModel
    {
        public FieldDbModel() { }
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
