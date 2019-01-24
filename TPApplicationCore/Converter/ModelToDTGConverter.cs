using System.Collections.Generic;
using TPApplicationCore.Model;
using AutoMapper;
using DataTransferGraph.DTGModel;

namespace TPApplicationCore.Converter
{
    public class ModelToDTGConverter
    {
        public AssemblyDTG ToDTG(AssemblyMetadata obj)
        {
            AssemblyDTG dtgModel = new AssemblyDTG(obj.name);
            Dictionary<TypeMetadata, TypeDTG> mapper = new Dictionary<TypeMetadata, TypeDTG>();
            foreach (TypeMetadata type in obj.getTypes())
            {
                TypeDTG dtgType = new TypeDTG(type.getName(), type.getNamespaceDef());
                mapper.Add(type, dtgType);
                dtgModel.Types.Add(dtgType);
            }

            foreach (TypeMetadata key in mapper.Keys)
            {
                FillDTOData(key, mapper);
            }

            return dtgModel;
        }

        private void FillDTOData(TypeMetadata key, Dictionary<TypeMetadata, TypeDTG> mapper)
        {
            TypeDTG dtgType = mapper[key];
            Dictionary<string, TypeDTG> unknownTypes = new Dictionary<string, TypeDTG>();
            foreach (FieldMetadata field in key.getFieldsList())
            {
                FieldDTG dtgField = new FieldDTG(field.getName(), RetrievedtgType(mapper, unknownTypes, field.getType()));
                dtgType.Fields.Add(dtgField);
            }

            foreach (MethodMetadata method in key.getMethodsList())
            {
                MethodDTG dtgMethod = new MethodDTG(method.getName(), RetrievedtgType(mapper, unknownTypes, method.getType()));
                foreach (ParameterMetadata parameter in method.getParameterList())
                {
                    ParameterDTG dtgParameter = new ParameterDTG(parameter.getName(), RetrievedtgType(mapper, unknownTypes, parameter.getType()));
                    dtgMethod.Parameters.Add(dtgParameter);
                }
                dtgType.Methods.Add(dtgMethod);
            }

            foreach (PropertyMetadata property in key.getPropertiesList())
            {
                PropertyDTG dtgProperty = new PropertyDTG(property.getName(), RetrievedtgType(mapper, unknownTypes, property.getType()));
                foreach (MethodMetadata method in property.getAccessorList())
                {
                    MethodDTG dtgMethod = new MethodDTG(method.getName(), RetrievedtgType(mapper, unknownTypes, method.getType()));
                    foreach (ParameterMetadata parameter in method.getParameterList())
                    {
                        ParameterDTG dtgParameter = new ParameterDTG(parameter.getName(), RetrievedtgType(mapper, unknownTypes, parameter.getType()));
                        dtgMethod.Parameters.Add(dtgParameter);
                    }
                    dtgProperty.AccessorList.Add(dtgMethod);
                }
                dtgType.Properties.Add(dtgProperty);
            }

        }

        private TypeDTG RetrievedtgType(Dictionary<TypeMetadata, TypeDTG> mapper, Dictionary<string, TypeDTG> unknownTypes, TypeMetadata type)
        {
            TypeDTG model;
            if (mapper.TryGetValue(type, out model))
            {
                return model;
            }
            else if (unknownTypes.TryGetValue(type.getName(), out model))
            {
                return model;
            }
            else
            {
                model = new TypeDTG(type.getName(),type.getNamespaceDef());
                unknownTypes.Add(type.getName(), model);
                return model;
            }
        }

        public AssemblyMetadata FromDTG(AssemblyDTG dtg)
        {
            Dictionary<string, TypeMetadata> types = new Dictionary<string, TypeMetadata>();
            AssemblyMetadata assembly = new AssemblyMetadata(dtg.Name, types);
            Dictionary<TypeDTG, TypeMetadata> mapper = new Dictionary<TypeDTG, TypeMetadata>();

            foreach (TypeDTG dtgType in dtg.Types)
            {
                TypeMetadata type = new TypeMetadata(dtgType.Name,dtgType.NamespaceDef);
                mapper.Add(dtgType, type);
                types.Add(type.getName(), type);
            }

            foreach (TypeDTG key in mapper.Keys)
            {
                FillModelData(key, mapper);
            }

            return assembly;
        }

        private void FillModelData(TypeDTG key, Dictionary<TypeDTG, TypeMetadata> mapper)
        {
            TypeMetadata type = mapper[key];
            Dictionary<string, TypeMetadata> unknownTypes = new Dictionary<string, TypeMetadata>();
            foreach (FieldDTG dtgField in key.Fields)
            {
                FieldMetadata field = new FieldMetadata(dtgField.Name, RetrieveType(mapper, unknownTypes, dtgField.Type));
                type.getFieldsList().Add(field);
            }

            foreach (MethodDTG dtgMethod in key.Methods)
            {
                MethodMetadata method = new MethodMetadata(dtgMethod.Name, RetrieveType(mapper, unknownTypes, dtgMethod.ReturnType));
                foreach (ParameterDTG dtgParameter in dtgMethod.Parameters)
                {
                    ParameterMetadata parameter = new ParameterMetadata(dtgParameter.Name, RetrieveType(mapper, unknownTypes, dtgParameter.Type));
                    method.getParameterList().Add(parameter);
                }
                type.getMethodsList().Add(method);
            }

            foreach (PropertyDTG dtgProperty in key.Properties)
            {
                PropertyMetadata property = new PropertyMetadata(dtgProperty.Name, RetrieveType(mapper, unknownTypes, dtgProperty.Type));
                foreach (MethodDTG dtgMethod in key.Methods)
                {
                    MethodMetadata method = new MethodMetadata(dtgMethod.Name, RetrieveType(mapper, unknownTypes, dtgMethod.ReturnType));
                    foreach (ParameterDTG dtgParameter in dtgMethod.Parameters)
                    {
                        ParameterMetadata parameter = new ParameterMetadata(dtgParameter.Name, RetrieveType(mapper, unknownTypes, dtgParameter.Type));
                        method.getParameterList().Add(parameter);
                    }
                    property.getAccessorList().Add(method);
                }
                type.getPropertiesList().Add(property);
            }
        }

        private TypeMetadata RetrieveType(Dictionary<TypeDTG, TypeMetadata> mapper, Dictionary<string, TypeMetadata> unknownTypes, TypeDTG type)
        {
            TypeMetadata model;
            if (mapper.TryGetValue(type, out model))
            {
                return model;
            }
            else if (unknownTypes.TryGetValue(type.Name, out model))
            {
                return model;
            }
            else
            {
                model = new TypeMetadata(type.Name,type.NamespaceDef);
                unknownTypes.Add(type.Name, model);
                return model;
            }
        }

    }
}
