using Serialize.Model.Db;
using System;
using System.Collections.Generic;
using TPApplicationCore.Model;

namespace Serialize.Converter
{
    class ModelToDBConverter
    {

        public AssemblyDbModel ToDTO(AssemblyMetadata obj)
        {
            AssemblyDbModel dbAssamblyModel = new AssemblyDbModel(obj.name);
            Dictionary<TypeMetadata, TypeDbModel> mapper = new Dictionary<TypeMetadata, TypeDbModel>();
            foreach (TypeMetadata type in obj.getTypes())
            {
              TypeDbModel dbType = new TypeDbModel(type.getName(),true);
              mapper.Add(type, dbType);
              dbAssamblyModel.Types.Add(dbType);
            }

            Dictionary<string, TypeDbModel> unknownTypes = new Dictionary<string, TypeDbModel>();
            foreach (TypeMetadata key in mapper.Keys)
            {
                fillDTOData(key, mapper,unknownTypes, dbAssamblyModel);
            }
            dbAssamblyModel.Types.AddRange(unknownTypes.Values);
            return dbAssamblyModel;
        }

        private void fillDTOData(TypeMetadata key, Dictionary<TypeMetadata, TypeDbModel> mapper, Dictionary<string, TypeDbModel> unknownTypes, AssemblyDbModel dbAssamblyModel)
        {
            TypeDbModel dbType = mapper[key];
           
            foreach(FieldMetadata field in key.getFieldsList())
            {
                FieldDbModel dbField = new FieldDbModel(field.getName(), retrieveDbType(mapper,unknownTypes, field.getType()));
                dbField.Owner = dbType;
                dbAssamblyModel.Fields.Add(dbField);
            }

            foreach (MethodMetadata method in key.getMethodsList())
            {
                MethodDbModel dbMethod = new MethodDbModel(method.getName(), retrieveDbType(mapper, unknownTypes, method.getType()));
                dbType.Methods.Add(dbMethod);
                dbMethod.OwnerType.Add(dbType);
                foreach(ParameterMetadata parameter in method.getParameterList())
                {
                    ParameterDbModel dbParameter = new ParameterDbModel(parameter.getName(), retrieveDbType(mapper, unknownTypes, parameter.getType()));
                    dbMethod.Parameters.Add(dbParameter);
                }
                dbAssamblyModel.Methods.Add(dbMethod);
            }

            foreach (PropertyMetadata property in key.getPropertiesList())
            {
                PropertyDbModel dbProperty = new PropertyDbModel(property.getName(), retrieveDbType(mapper, unknownTypes, property.getType()));
                dbProperty.OwnerType = dbType;
                
                foreach (MethodMetadata method in property.getAccessorList())
                {
                    MethodDbModel dbMethod = new MethodDbModel(method.getName(), retrieveDbType(mapper, unknownTypes, method.getType()));
                    dbProperty.Accessors.Add(dbMethod);
                    dbMethod.OwnerProperty.Add(dbProperty);
                    foreach (ParameterMetadata parameter in method.getParameterList())
                    {
                        ParameterDbModel dbParameter = new ParameterDbModel(parameter.getName(), retrieveDbType(mapper, unknownTypes, parameter.getType()));
                        dbMethod.Parameters.Add(dbParameter);
                    }
                    dbAssamblyModel.Methods.Add(dbMethod);
                }
                dbAssamblyModel.Properties.Add(dbProperty);
            }
            
        }

        private TypeDbModel retrieveDbType(Dictionary<TypeMetadata, TypeDbModel> mapper, Dictionary<string, TypeDbModel> unknownTypes, TypeMetadata type)
        {
            TypeDbModel model;
            if(mapper.TryGetValue(type, out model))
            {
                return model;
            }
            else if(unknownTypes.TryGetValue(type.getName(), out model))
            {
                TPApplicationCore.Logging.Logger.log(System.Diagnostics.TraceEventType.Information, "Class found using unknown: " + type.getName());
                return model;
            }
            else
            {
                TPApplicationCore.Logging.Logger.log(System.Diagnostics.TraceEventType.Information, "Class not found in unknownClasses, creating: " + type.getName());
                model = new TypeDbModel(type.getName(),false);
                unknownTypes.Add(type.getName(), model);
                return model;
            }
        }

        private TypeMetadata retrieveType(Dictionary<TypeDbModel, TypeMetadata> mapper, TypeDbModel type)
        {
            return mapper[type];
        }

        public AssemblyMetadata FromDTO(AssemblyDbModel dto)
        {
            Dictionary<string, TypeMetadata> types = new Dictionary<string, TypeMetadata>();
            AssemblyMetadata assembly = new AssemblyMetadata(dto.Name, types);
            Dictionary<TypeDbModel, TypeMetadata> mapper = new Dictionary<TypeDbModel, TypeMetadata>();

            foreach (TypeDbModel dbType in dto.Types)
            {
                TypeMetadata type = new TypeMetadata(dbType.Name);
                mapper.Add(dbType, type);
                if (dbType.isDefined)
                {
                    types.Add(type.getName(), type);
                }
            }

            foreach (TypeDbModel key in mapper.Keys)
            {
               
                fillModelData(key, mapper,dto);
            }

            return assembly;
        }

        private void fillModelData(TypeDbModel key, Dictionary<TypeDbModel, TypeMetadata> mapper, AssemblyDbModel dto)
        {
            
            TypeMetadata type = mapper[key];
            
            foreach (FieldDbModel dbField in dto.Fields)
            {
                if (dbField.Owner.Equals(key))
                {
                    FieldMetadata field = new FieldMetadata(dbField.Name, retrieveType(mapper, dbField.Type));
                    type.getFieldsList().Add(field);
                }
            }

            foreach (MethodDbModel dbMethod in key.Methods)
            {
                MethodMetadata method = new MethodMetadata(dbMethod.Name, retrieveType(mapper,dbMethod.ReturnType));
                foreach (ParameterDbModel dbParameter in dbMethod.Parameters)
                {
                    ParameterMetadata parameter = new ParameterMetadata(dbParameter.Name, retrieveType(mapper, dbParameter.Type));
                    method.getParameterList().Add(parameter);
                }
                type.getMethodsList().Add(method);
            }

            foreach (PropertyDbModel dbProperty in dto.Properties)
            {
                PropertyMetadata property = new PropertyMetadata(dbProperty.Name, retrieveType(mapper, dbProperty.Type));
                foreach (MethodDbModel dbMethod in dbProperty.Accessors)
                {
                    MethodMetadata method = new MethodMetadata(dbMethod.Name, retrieveType(mapper, dbMethod.ReturnType));
                    foreach (ParameterDbModel dbParameter in dbMethod.Parameters)
                    {
                        ParameterMetadata parameter = new ParameterMetadata(dbParameter.Name, retrieveType(mapper, dbParameter.Type));
                        method.getParameterList().Add(parameter);
                    }
                    property.getAccessorList().Add(method);
                }
                type.getPropertiesList().Add(property);
            }
        }
    }
}
