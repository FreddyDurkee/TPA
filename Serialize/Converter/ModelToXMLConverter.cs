using Serialize.Model.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using TPApplicationCore.Model;

namespace Serialize.Converter
{
    class ModelToXMLConverter
    {

        public AssemblyXmlModel ToDTO(AssemblyMetadata obj)
        {
            AssemblyXmlModel xmlModel = new AssemblyXmlModel(obj.name);
            Dictionary<TypeMetadata, TypeXmlModel> mapper = new Dictionary<TypeMetadata, TypeXmlModel>();
            foreach (TypeMetadata type in obj.getTypes())
            {
              TypeXmlModel xmlType = new TypeXmlModel(type.getName());
              mapper.Add(type, xmlType);
            }

            foreach(TypeMetadata key in mapper.Keys)
            {
                fillDTOData(key, mapper);
            }

            return xmlModel;
        }

        private void fillDTOData(TypeMetadata key, Dictionary<TypeMetadata, TypeXmlModel> mapper)
        {
            TypeXmlModel xmlType = mapper[key];
            foreach(FieldMetadata field in key.getFieldsList())
                {
                    FieldXmlModel xmlField = new FieldXmlModel(field.getName(),mapper[field.getType()]);
                    xmlType.fields.Add(xmlField);
                }

            foreach (MethodMetadata method in key.getMethodsList())
            {
                MethodXmlModel xmlMethod = new MethodXmlModel(method.getName(), mapper[method.getType()]);
                foreach(ParameterMetadata parameter in method.getParameterList())
                {
                    ParameterXmlModel xmlParameter = new ParameterXmlModel(parameter.getName(), mapper[parameter.getType()]);
                    xmlMethod.parameters.Add(xmlParameter);
                }
                xmlType.methods.Add(xmlMethod);
            }

            foreach (PropertyMetadata property in key.getPropertiesList())
            {
                PropertyXmlModel xmlProperty = new PropertyXmlModel(property.getName(),mapper[property.getType()]);
                foreach(MethodMetadata method in property.getAccessorList())
                {
                    MethodXmlModel xmlMethod = new MethodXmlModel(method.getName(), mapper[method.getType()]);
                    foreach (ParameterMetadata parameter in method.getParameterList())
                    {
                        ParameterXmlModel xmlParameter = new ParameterXmlModel(parameter.getName(), mapper[parameter.getType()]);
                        xmlMethod.parameters.Add(xmlParameter);
                    }
                    xmlProperty.accessorList.Add(xmlMethod);
                }
                xmlType.properties.Add(xmlProperty);
            }

        }

        public AssemblyMetadata FromDTO(AssemblyXmlModel dto)
        {
            Dictionary<string, TypeMetadata> types = new Dictionary<string, TypeMetadata>();
            AssemblyMetadata assembly = new AssemblyMetadata(dto.name, types);
            Dictionary<TypeXmlModel, TypeMetadata> mapper = new Dictionary<TypeXmlModel, TypeMetadata>();

            foreach (TypeXmlModel xmlType in dto.typeList)
            {
                TypeMetadata type = new TypeMetadata(xmlType.name);
                mapper.Add(xmlType, type);
            }

            foreach (TypeXmlModel key in mapper.Keys)
            {
                fillModelData(key, mapper);
            }

            return assembly;
        }

        private void fillModelData(TypeXmlModel key, Dictionary<TypeXmlModel, TypeMetadata> mapper)
        {
            TypeMetadata type = mapper[key];
            foreach (FieldXmlModel xmlField in key.fields)
            {
                FieldMetadata field = new FieldMetadata(xmlField.name, mapper[xmlField.type]);
                type.getFieldsList().Add(field);
            }

            foreach (MethodXmlModel xmlMethod in key.methods)
            {
                MethodMetadata method = new MethodMetadata(xmlMethod.name, mapper[xmlMethod.returnType]);
                foreach (ParameterXmlModel xmlParameter in xmlMethod.parameters)
                {
                    ParameterMetadata parameter = new ParameterMetadata(xmlParameter.name, mapper[xmlParameter.type]);
                    method.getParameterList().Add(parameter);
                }
                type.getMethodsList().Add(method);
            }

            foreach (PropertyXmlModel xmlProperty in key.properties)
            {
                PropertyMetadata property = new PropertyMetadata(xmlProperty.name, mapper[xmlProperty.type]);
                foreach (MethodXmlModel xmlMethod in key.methods)
                {
                    MethodMetadata method = new MethodMetadata(xmlMethod.name, mapper[xmlMethod.returnType]);
                    foreach (ParameterXmlModel xmlParameter in xmlMethod.parameters)
                    {
                        ParameterMetadata parameter = new ParameterMetadata(xmlParameter.name, mapper[xmlParameter.type]);
                        method.getParameterList().Add(parameter);
                    }
                    property.getAccessorList().Add(method);
                }
                type.getPropertiesList().Add(property);
            }
        }
    }
}
