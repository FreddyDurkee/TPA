using DataTransferGraph.DTGModel;
using Serialize.Model.Xml;
using System.Collections.Generic;

namespace Serialize.Converter
{
    class ModelToXMLConverter
    {

        public AssemblyXmlModel ToDTO(AssemblyDTG obj)
        {
            AssemblyXmlModel xmlModel = new AssemblyXmlModel(obj.Name);
            Dictionary<TypeDTG, TypeXmlModel> mapper = new Dictionary<TypeDTG, TypeXmlModel>();
            foreach (TypeDTG type in obj.Types)
            {
              TypeXmlModel xmlType = new TypeXmlModel(type.Name);
              mapper.Add(type, xmlType);
                xmlModel.typeList.Add(xmlType);
            }

            foreach(TypeDTG key in mapper.Keys)
            {
                FillDTOData(key, mapper);
            }

            return xmlModel;
        }

        private void FillDTOData(TypeDTG key, Dictionary<TypeDTG, TypeXmlModel> mapper)
        {
            TypeXmlModel xmlType = mapper[key];
            Dictionary<string, TypeXmlModel> unknownTypes = new Dictionary<string, TypeXmlModel>();
            foreach(FieldDTG field in key.Fields)
            {
                FieldXmlModel xmlField = new FieldXmlModel(field.Name, RetrieveXmlType(mapper,unknownTypes, field.Type));
                xmlType.fields.Add(xmlField);
            }

            foreach (MethodDTG method in key.Methods)
            {
                MethodXmlModel xmlMethod = new MethodXmlModel(method.Name, RetrieveXmlType(mapper, unknownTypes, method.ReturnType));
                foreach(ParameterDTG parameter in method.Parameters)
                {
                    ParameterXmlModel xmlParameter = new ParameterXmlModel(parameter.Name, RetrieveXmlType(mapper, unknownTypes, parameter.Type));
                    xmlMethod.parameters.Add(xmlParameter);
                }
                xmlType.methods.Add(xmlMethod);
            }

            foreach (PropertyDTG property in key.Properties)
            {
                PropertyXmlModel xmlProperty = new PropertyXmlModel(property.Name, RetrieveXmlType(mapper, unknownTypes, property.Type));
                foreach(MethodDTG method in property.AccessorList)
                {
                    MethodXmlModel xmlMethod = new MethodXmlModel(method.Name, RetrieveXmlType(mapper, unknownTypes, method.ReturnType));
                    foreach (ParameterDTG parameter in method.Parameters)
                    {
                        ParameterXmlModel xmlParameter = new ParameterXmlModel(parameter.Name, RetrieveXmlType(mapper, unknownTypes, parameter.Type));
                        xmlMethod.parameters.Add(xmlParameter);
                    }
                    xmlProperty.accessorList.Add(xmlMethod);
                }
                xmlType.properties.Add(xmlProperty);
            }

        }

        private TypeXmlModel RetrieveXmlType(Dictionary<TypeDTG, TypeXmlModel> mapper, Dictionary<string, TypeXmlModel> unknownTypes, TypeDTG type)
        {
            TypeXmlModel model;
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
                model = new TypeXmlModel(type.Name);
                unknownTypes.Add(type.Name, model);
                return model;
            }
        }

        private TypeDTG RetrieveType(Dictionary<TypeXmlModel, TypeDTG> mapper, Dictionary<string, TypeDTG> unknownTypes, TypeXmlModel type)
        {
            TypeDTG model;
            if (mapper.TryGetValue(type, out model))
            {
                return model;
            }
            else if (unknownTypes.TryGetValue(type.name, out model))
            {
                return model;
            }
            else
            {
                model = new TypeDTG(type.name);
                unknownTypes.Add(type.name, model);
                return model;
            }
        }

        public AssemblyDTG FromDTO(AssemblyXmlModel dto)
        {
            AssemblyDTG assembly = new AssemblyDTG(dto.name);
            Dictionary<TypeXmlModel, TypeDTG> mapper = new Dictionary<TypeXmlModel, TypeDTG>();

            foreach (TypeXmlModel xmlType in dto.typeList)
            {
                TypeDTG type = new TypeDTG(xmlType.name);
                mapper.Add(xmlType, type);
                assembly.Types.Add(type);
            }

            foreach (TypeXmlModel key in mapper.Keys)
            {
                FillModelData(key, mapper);
            }

            return assembly;
        }

        private void FillModelData(TypeXmlModel key, Dictionary<TypeXmlModel, TypeDTG> mapper)
        {
            TypeDTG type = mapper[key];
            Dictionary<string, TypeDTG> unknownTypes = new Dictionary<string, TypeDTG>();
            foreach (FieldXmlModel xmlField in key.fields)
            {
                FieldDTG field = new FieldDTG(xmlField.name, RetrieveType(mapper,unknownTypes,xmlField.type));
                type.Fields.Add(field);
            }

            foreach (MethodXmlModel xmlMethod in key.methods)
            {
                MethodDTG method = new MethodDTG(xmlMethod.name, RetrieveType(mapper, unknownTypes, xmlMethod.returnType));
                foreach (ParameterXmlModel xmlParameter in xmlMethod.parameters)
                {
                    ParameterDTG parameter = new ParameterDTG(xmlParameter.name, RetrieveType(mapper, unknownTypes, xmlParameter.type));
                    method.Parameters.Add(parameter);
                }
                type.Methods.Add(method);
            }

            foreach (PropertyXmlModel xmlProperty in key.properties)
            {
                PropertyDTG property = new PropertyDTG(xmlProperty.name, RetrieveType(mapper, unknownTypes, xmlProperty.type));
                foreach (MethodXmlModel xmlMethod in key.methods)
                {
                    MethodDTG method = new MethodDTG(xmlMethod.name, RetrieveType(mapper, unknownTypes, xmlMethod.returnType));
                    foreach (ParameterXmlModel xmlParameter in xmlMethod.parameters)
                    {
                        ParameterDTG parameter = new ParameterDTG(xmlParameter.name, RetrieveType(mapper, unknownTypes, xmlParameter.type));
                        method.Parameters.Add(parameter);
                    }
                    property.AccessorList.Add(method);
                }
                type.Properties.Add(property);
            }
        }
    }
}
