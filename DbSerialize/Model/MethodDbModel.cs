using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbSerialize.Model
{
    public class MethodDbModel
    {
        public MethodDbModel() { }
        public MethodDbModel(string name, TypeDbModel returnType)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = new List<ParameterDbModel>();
            OwnerType = new HashSet<TypeDbModel>();
            OwnerProperty = new HashSet<PropertyDbModel>();
        }
        #region public
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeDbModel ReturnType { get; set; }
        public List<ParameterDbModel> Parameters { get; set; }
        public ICollection<TypeDbModel> OwnerType { get; set; }
        public ICollection<PropertyDbModel> OwnerProperty { get; set; }

        #endregion

    }
}