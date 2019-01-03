using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DbSerialize.Model
{
    public class AssemblyDbModel
    {
        public AssemblyDbModel()
        {

        }

        public AssemblyDbModel(string name)
        {
            Name = name;
            Types = new List<TypeDbModel>();
            Fields = new List<FieldDbModel>();
            Methods = new List<MethodDbModel>();
            Properties = new List<PropertyDbModel>();
        }

        [Key]
        public int Id { get; set; }
        public List<TypeDbModel> Types { get; set; }
        public List<FieldDbModel> Fields { get; set; }
        public List<MethodDbModel> Methods { get; set; }
        public List<PropertyDbModel> Properties { get; set; }
        public string Name { get; set; }
    }
}

