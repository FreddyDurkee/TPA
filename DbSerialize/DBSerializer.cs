using DataTransferGraph.Api;
using DataTransferGraph.DTGModel;
using DbSerialize.Converter;
using DbSerialize.Model;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace DbSerialize
{
    [Export(typeof(ISerializer))]
    public class DBSerializer : ISerializer
    {
        ModelToDBConverter converter = new ModelToDBConverter();
        private string connectionString;

        [ImportingConstructor]
        public DBSerializer([Import("DBSerializer.ConnectionString")] string connectionString)
        {
            this.connectionString = connectionString;
        }

        public AssemblyDTG deserialize(string filePath)
        {
            int modelId = 1;
            using (var ctx = new SerializationContext(connectionString, false))
            {
              
                    AssemblyDbModel model = ctx.Assemblies.Where(a => a.Id.Equals(modelId))
                        .Include("Fields")
                        .Include("Methods")
                        .Include("Properties")
                        .Include("Types")
                        .Include("Methods.Parameters")
                        .Include("Methods.OwnerType")
                        .Include("Methods.OwnerProperty")
                        .Include("Properties.Accessors")
                        .Include("Types.Methods")
                        .First();
                    return converter.FromDTO(model);
 
            }
        }

        public void serialize(AssemblyDTG obj, string filePath)
        {
            using (var ctx = new SerializationContext(connectionString, true))
            {
                var actual = converter.ToDTO(obj);
                actual.Id = 1;
                ctx.Assemblies.Add(actual);
                ctx.SaveChanges();
            }
        }

        public class SerializationContext : DbContext
        {
            public SerializationContext(string connectionString,bool reset):base(connectionString)
            {
                if (reset) {
                    Database.Delete();
                }
                
            }

            public DbSet<AssemblyDbModel> Assemblies { get; set; }
 
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {

                modelBuilder.Entity<TypeDbModel>()
                            .HasMany<MethodDbModel>(t => t.Methods)
                            .WithMany(m => m.OwnerType)
                            .Map(tm =>
                            {
                                tm.MapLeftKey("TypeRefId");
                                tm.MapRightKey("MethodRefId");
                                tm.ToTable("Type_Method");
                            });

                modelBuilder.Entity<PropertyDbModel>()
                           .HasMany<MethodDbModel>(p => p.Accessors)
                           .WithMany(m => m.OwnerProperty)
                           .Map(pm =>
                           {
                               pm.MapLeftKey("PropertyRefId");
                               pm.MapRightKey("MethodRefId");
                               pm.ToTable("Property_Method");
                           });

            }
        }
    }

}
