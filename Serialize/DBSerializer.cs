using System.Collections.Generic;
using System.Data.Entity;
using Serialize.Model.Db;
using Serialize.Api;
using Serialize.Converter;
using TPApplicationCore.Model;
using System.Linq;

namespace Serialize
{
    public class DBSerializer: IDBSerializer
    {

        ModelToDBConverter converter = new ModelToDBConverter();

        public AssemblyMetadata deserialize(int modelId)
        {
            using (var ctx = new SerializationContext())
            {
                AssemblyDbModel model = (from a in ctx.Assemblies where a.Id.Equals(modelId)  select a).First();
                if(model != null) {
                    return converter.FromDTO(model);
                }
                else
                {
                    return null;
                }
                
            }
        }

        public void serialize(AssemblyMetadata obj)
        {
            using (var ctx = new SerializationContext())
            {
                var actual = converter.ToDTO(obj);
                ctx.Assemblies.Add(actual);
                ctx.SaveChanges();
            }
        }

        public class SerializationContext : DbContext
        {
            public SerializationContext():base("name=TPASerialize")
            {
                this.Configuration.ProxyCreationEnabled = false;
                Database.SetInitializer<SerializationContext>(new DropCreateDatabaseAlways<SerializationContext>());
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
