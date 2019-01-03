using System.ComponentModel.DataAnnotations;

namespace DbSerialize.Model
{
    public class ParameterDbModel
    {
        public ParameterDbModel() { }
        public ParameterDbModel(string name, TypeDbModel type)
        {
            Name = name;
            Type = type;
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeDbModel Type { get; set; }
        #endregion

    }
}
