using System.Collections.Generic;
using System.Data.Entity;
using Serialize.Model.Db;
using Serialize.Api;
using Serialize.Converter;
using TPApplicationCore.Model;
using System.Linq;
using System;

namespace Serialize
{
    public class DBSerializer : IDBSerializer
    {
        ModelToDBConverter converter = new ModelToDBConverter();

        public AssemblyMetadata deserialize()
        {
            int modelId = 1;
            using (var ctx = new SerializationContext(false))
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

        public void serialize(AssemblyMetadata obj)
        {
            using (var ctx = new SerializationContext(true))
            {
                var actual = converter.ToDTO(obj);
                actual.Id = 1;
                ctx.Assemblies.Add(actual);
                ctx.SaveChanges();
            }
        }

        public class SerializationContext : DbContext
        {
            public SerializationContext(bool reset):base("name=TPASerialize")
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
