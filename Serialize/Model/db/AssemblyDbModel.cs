using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Serialize.Model.Db
{
    public class AssemblyDbModel
    {
        public AssemblyDbModel()
        {

        }

        public AssemblyDbModel(string name)
        {
            Name = name;
            typeList = new List<TypeDbModel>();
            fieldList = new List<FieldDbModel>();
            methodList = new List<MethodDbModel>();
            propertyList = new List<PropertyDbModel>();
        }

        [Key]
        public int Id { get; set; }
        public List<TypeDbModel> typeList { get; set; }
        public List<FieldDbModel> fieldList { get; set; }
        public List<MethodDbModel> methodList { get; set; }
        public List<PropertyDbModel> propertyList { get; set; }
        public string Name { get; set; }
    }
}

