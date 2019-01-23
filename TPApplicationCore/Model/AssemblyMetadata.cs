using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Logging;
using TPApplicationCore.Serialization;

namespace TPApplicationCore.Model
{
    public class AssemblyMetadata
    {
        private static TPALogger LOGGER = ApplicationContext.GetLogger(typeof(AssemblyMetadata));

        private Dictionary<string,TypeMetadata> typeList;

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

        public List<MethodMetadata> getAccessors(PropertyMetadata property)
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

        public AssemblyMetadata(string assemblyFile)
        {
            typeList = new Dictionary<string,TypeMetadata>();
            Type[] typy = Assembly.ReflectionOnlyLoadFrom(assemblyFile).GetTypes();
            string filename = Path.GetFileName(assemblyFile);
            name = filename.Substring(0, filename.Length - 4);
            foreach (Type t in typy)
            {
                TypeMetadata typeMetadata = new TypeMetadata(t.Name);
                typeList.Add(t.Name, typeMetadata);
            }
            foreach (Type t in typy)
            {
                buildTypes(t);
            }
        }

        public AssemblyMetadata(string name, Dictionary<string, TypeMetadata> typeList)
        {
            this.typeList = typeList;
            this.name = name;
        }

        public void buildTypes(Type type)
        {
           TypeMetadata typeMetadata = typeList[type.Name];
           LOGGER.Info( "Loading class: " + type.Name);

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
                    LOGGER.Info( "Class not found, creating: " + index.ReturnType.Name);
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
                    LOGGER.Info( "Class not found, creating: " + index.ReturnType.Name);
                    typeMetadata = new TypeMetadata(index.ReturnType.Name);
                    method = new MethodMetadata(index.Name, typeMetadata);
                }
                buildParameters(method, index);
                property.getAccessorList().Add(method);
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
                    LOGGER.Info( "Class not found, creating: " + f.FieldType.Name);
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
                if (typeList.TryGetValue(p.PropertyType.Name, out typeObj))
                {
                    property = new PropertyMetadata(p.Name, typeObj);
                }
                else
                {
                    LOGGER.Info( "Class not found, creating: " + p.PropertyType.Name);
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
                if (typeList.TryGetValue(p.ParameterType.Name, out typeObj))
                {
                    parameter = new ParameterMetadata(p.ParameterType.Name, typeObj);
                }
                else
                {
                    LOGGER.Info( "Class not found, creating: " + p.ParameterType.Name);
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

        public void Serialize()
        {
            ApplicationContext context = ApplicationContext.CONTEXT;
            SerializationManager manager = context.SerializationManager;
            manager.Serialize(this);
        }
        
        public static AssemblyMetadata Deserialize()
        {
            ApplicationContext context = ApplicationContext.CONTEXT;
            SerializationManager manager = context.SerializationManager;
            return manager.Deserialize();
        }
    }
}

