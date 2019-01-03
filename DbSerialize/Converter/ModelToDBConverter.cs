using DataTransferGraph.DTGModel;
using DbSerialize.Model;
using System.Collections.Generic;

namespace DbSerialize.Converter
{
    class ModelToDBConverter
    {

        public AssemblyDbModel ToDTO(AssemblyDTG obj)
        {
            AssemblyDbModel dbAssamblyModel = new AssemblyDbModel(obj.Name);
            Dictionary<TypeDTG, TypeDbModel> mapper = new Dictionary<TypeDTG, TypeDbModel>();
            foreach (TypeDTG type in obj.Types)
            {
              TypeDbModel dbType = new TypeDbModel(type.Name,true);
              mapper.Add(type, dbType);
              dbAssamblyModel.Types.Add(dbType);
            }

            Dictionary<string, TypeDbModel> unknownTypes = new Dictionary<string, TypeDbModel>();
            foreach (TypeDTG key in mapper.Keys)
            {
                fillDTOData(key, mapper,unknownTypes, dbAssamblyModel);
            }
            dbAssamblyModel.Types.AddRange(unknownTypes.Values);
            return dbAssamblyModel;
        }

        private void fillDTOData(TypeDTG key, Dictionary<TypeDTG, TypeDbModel> mapper, Dictionary<string, TypeDbModel> unknownTypes, AssemblyDbModel dbAssamblyModel)
        {
            TypeDbModel dbType = mapper[key];
           
            foreach(FieldDTG field in key.Fields)
            {
                FieldDbModel dbField = new FieldDbModel(field.Name, retrieveDbType(mapper,unknownTypes, field.Type));
                dbField.Owner = dbType;
                dbAssamblyModel.Fields.Add(dbField);
            }

            foreach (MethodDTG method in key.Methods)
            {
                MethodDbModel dbMethod = new MethodDbModel(method.Name, retrieveDbType(mapper, unknownTypes, method.ReturnType));
                dbType.Methods.Add(dbMethod);
                dbMethod.OwnerType.Add(dbType);
                foreach(ParameterDTG parameter in method.Parameters)
                {
                    ParameterDbModel dbParameter = new ParameterDbModel(parameter.Name, retrieveDbType(mapper, unknownTypes, parameter.Type));
                    dbMethod.Parameters.Add(dbParameter);
                }
                dbAssamblyModel.Methods.Add(dbMethod);
            }

            foreach (PropertyDTG property in key.Properties)
            {
                PropertyDbModel dbProperty = new PropertyDbModel(property.Name, retrieveDbType(mapper, unknownTypes, property.Type));
                dbProperty.OwnerType = dbType;
                
                foreach (MethodDTG method in property.AccessorList)
                {
                    MethodDbModel dbMethod = new MethodDbModel(method.Name, retrieveDbType(mapper, unknownTypes, method.ReturnType));
                    dbProperty.Accessors.Add(dbMethod);
                    dbMethod.OwnerProperty.Add(dbProperty);
                    foreach (ParameterDTG parameter in method.Parameters)
                    {
                        ParameterDbModel dbParameter = new ParameterDbModel(parameter.Name, retrieveDbType(mapper, unknownTypes, parameter.Type));
                        dbMethod.Parameters.Add(dbParameter);
                    }
                    dbAssamblyModel.Methods.Add(dbMethod);
                }
                dbAssamblyModel.Properties.Add(dbProperty);
            }
            
        }

        private TypeDbModel retrieveDbType(Dictionary<TypeDTG, TypeDbModel> mapper, Dictionary<string, TypeDbModel> unknownTypes, TypeDTG type)
        {
            TypeDbModel model;
            if(mapper.TryGetValue(type, out model))
            {
                return model;
            }
            else if(unknownTypes.TryGetValue(type.Name, out model))
            {
                return model;
            }
            else
            {
                model = new TypeDbModel(type.Name,false);
                unknownTypes.Add(type.Name, model);
                return model;
            }
        }

        private TypeDTG retrieveType(Dictionary<TypeDbModel, TypeDTG> mapper, TypeDbModel type)
        {
            return mapper[type];
        }

        public AssemblyDTG FromDTO(AssemblyDbModel dto)
        {
            AssemblyDTG assembly = new AssemblyDTG(dto.Name);
            Dictionary<TypeDbModel, TypeDTG> mapper = new Dictionary<TypeDbModel, TypeDTG>();

            foreach (TypeDbModel dbType in dto.Types)
            {
                TypeDTG type = new TypeDTG(dbType.Name);
                mapper.Add(dbType, type);
                if (dbType.isDefined)
                {
                    assembly.Types.Add(type);
                }
            }

            foreach (TypeDbModel key in mapper.Keys)
            {
               
                fillModelData(key, mapper,dto);
            }

            return assembly;
        }

        private void fillModelData(TypeDbModel key, Dictionary<TypeDbModel, TypeDTG> mapper, AssemblyDbModel dto)
        {
            
            TypeDTG type = mapper[key];
            
            foreach (FieldDbModel dbField in dto.Fields)
            {
                if (dbField.Owner.Equals(key))
                {
                    FieldDTG field = new FieldDTG(dbField.Name, retrieveType(mapper, dbField.Type));
                    type.Fields.Add(field);
                }
            }

            foreach (MethodDbModel dbMethod in key.Methods)
            {
                MethodDTG method = new MethodDTG(dbMethod.Name, retrieveType(mapper,dbMethod.ReturnType));
                foreach (ParameterDbModel dbParameter in dbMethod.Parameters)
                {
                    ParameterDTG parameter = new ParameterDTG(dbParameter.Name, retrieveType(mapper, dbParameter.Type));
                    method.Parameters.Add(parameter);
                }
                type.Methods.Add(method);
            }

            foreach (PropertyDbModel dbProperty in dto.Properties)
            {
                PropertyDTG property = new PropertyDTG(dbProperty.Name, retrieveType(mapper, dbProperty.Type));
                foreach (MethodDbModel dbMethod in dbProperty.Accessors)
                {
                    MethodDTG method = new MethodDTG(dbMethod.Name, retrieveType(mapper, dbMethod.ReturnType));
                    foreach (ParameterDbModel dbParameter in dbMethod.Parameters)
                    {
                        ParameterDTG parameter = new ParameterDTG(dbParameter.Name, retrieveType(mapper, dbParameter.Type));
                        method.Parameters.Add(parameter);
                    }
                    property.AccessorList.Add(method);
                }
                type.Properties.Add(property);
            }
        }
    }
}
