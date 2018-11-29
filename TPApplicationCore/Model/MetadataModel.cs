using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using TPApplicationCore.ViewModelAPI;


namespace TPApplicationCore.Model
{
    [DataContract]
    public class MetadataModel
    {
        [DataMember]
        private Dictionary<string,TypeMetadata> typeList;

        [DataMember]
        public string name;

        public Dictionary<string, TypeMetadata> typesList
        {
            get { return typeList; }
        }

        public TypeMetadata getType(ParameterMetadata parameter)
        {
            return parameter.getType();
        }

        public List<FieldMetadata> getFields(TypeMetadata type)
        {
            return type.getFieldsList().ToList();
        }

        internal List<MethodMetadata> getAccessors(PropertyMetadata property)
        {
            return property.getAccessorList();
        }

        public List<MethodMetadata> getMethods(TypeMetadata type)
        {
            return type.getMethodsList().ToList();
        }

        public List<ParameterMetadata> getParameters(MethodMetadata method)
        {
            return method.getParameterList().ToList();
        }

        public List<PropertyMetadata> getProperties(TypeMetadata type)
        {
            return type.getPropertiesList().ToList();
        }

        public MetadataModel(string assemblyFile)
        {
            typeList = new Dictionary<string,TypeMetadata>();
            Type[] typy = Assembly.LoadFrom(assemblyFile).GetTypes();
            string filename = Path.GetFileName(assemblyFile);
            name = filename.Substring(0, filename.Length - 4);
            foreach (Type t in typy)
            {
                buildTypes(t);
            }
        }


        public void buildTypes(Type type)
        {
           TypeMetadata typeMetadata;
            Logging.Logger.log(System.Diagnostics.TraceEventType.Information, "Loading class: " + type.Name);

            if (typeList.TryGetValue(type.Name, out typeMetadata))
            {
                //DO NOTHING
            }
            else
            {
                typeMetadata = new TypeMetadata(type.Name);
                typeList.Add(type.Name, typeMetadata);
            }
            if (type.IsPrimitive != true)
            {
                buildMethods(typeMetadata, type);
                buildFields(typeMetadata, type);
                buildProperites(typeMetadata, type);
            }

        }
        public void buildMethods(TypeMetadata typeMetadata, Type type)
        {
            MethodInfo[] metody = type.GetMethods();
            MethodMetadata metoda;
            foreach (MethodInfo index in metody)
            {
               TypeMetadata typeObj;
                if (typeList.TryGetValue(index.ReturnType.Name, out typeObj))
                {
                    metoda = new MethodMetadata(index.Name, typeObj);
                }
                else
                {
                    typeObj = new TypeMetadata(index.ReturnType.Name);
                    metoda = new MethodMetadata(index.Name, typeObj);
                }
                typeMetadata.getMethodsList().Add(metoda);
                buildParameters(metoda, index);

            }
        }
        public void buildMethods(PropertyMetadata property, PropertyInfo type)
        {
            MethodInfo[] methods = type.GetAccessors(true);
            MethodMetadata method;
            foreach (MethodInfo index in methods)
            {
               TypeMetadata typeMetadata;
                if (typeList.TryGetValue(index.ReturnType.Name, out typeMetadata))
                {
                    method = new MethodMetadata(index.Name, typeMetadata);
                }
                else
                {
                    typeMetadata = new TypeMetadata(index.ReturnType.Name);
                    method = new MethodMetadata(index.Name, typeMetadata);
                }
                buildParameters(method, index);

            }
        }
        public void buildFields(TypeMetadata typeMetadata, Type type)
        {
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo f in fields)
            {
                FieldMetadata field;
               TypeMetadata typeObj;
                if (typeList.TryGetValue(f.Name, out typeObj))
                {
                    field = new FieldMetadata(f.Name, typeObj);
                }
                else
                {
                    typeObj = new TypeMetadata(f.FieldType.Name);
                    field = new FieldMetadata(f.Name, typeObj);
                }
                typeMetadata.getFieldsList().Add(field);
            }
        }


        public void buildProperites(TypeMetadata klasa, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyMetadata property;
               TypeMetadata typeObj;
                if (typeList.TryGetValue(p.Name, out typeObj))
                {
                    property = new PropertyMetadata(p.Name, typeObj);
                }
                else
                {
                    typeObj = new TypeMetadata(p.PropertyType.Name);
                    property = new PropertyMetadata(p.Name, typeObj);
                }
                buildMethods(property, p);
                klasa.getPropertiesList().Add(property);
            }
        }
        public void buildParameters(MethodMetadata method, MethodInfo info)
        {
            ParameterInfo[] parameters = info.GetParameters();
            foreach (ParameterInfo p in parameters)
            {
                ParameterMetadata parameter;
               TypeMetadata typeObj;
                if (typeList.TryGetValue(p.Name, out typeObj))
                {
                    parameter = new ParameterMetadata(p.Name, typeObj);
                }
                else
                {
                    typeObj = new TypeMetadata(p.ParameterType.Name);
                    parameter = new ParameterMetadata(p.Name, typeObj);
                }
                method.getParameterList().Add(parameter);
            }
        }

        public List<TypeMetadata> getTypes()
        {
            List<TypeMetadata> tmpList = new List<TypeMetadata>();
            foreach (KeyValuePair<string, TypeMetadata> k in typeList)
            {
                tmpList.Add(k.Value);
            }
            return tmpList;
        }

        public TypeMetadata getType(FieldMetadata field)
        {
            return field.getType();
        }

        public TypeMetadata getType(PropertyMetadata property)
        {
            return property.getType();
        }
    }
}

