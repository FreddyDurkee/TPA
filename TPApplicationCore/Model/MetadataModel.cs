using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TPApplicationCore.ViewModelAPI;


namespace TPApplicationCore.Model
{
    public class MetadataModel
    {
        private Dictionary<string,TypeMetadata> listaKlas;

        public Dictionary<string,TypeMetadata> ListaKlas
        {
            get { return listaKlas; }
        }

        public TypeMetadata getClass(ParameterMetadata parametr)
        {
            return parametr.getType();
        }

        public List<FieldMetadata> getFields(TypeMetadata klasa)
        {
            return klasa.getFieldsList().ToList();
        }

        internal List<MethodMetadata> getAccessors(PropertyMetadata property)
        {
            return property.getAccessorList();
        }

        public List<MethodMetadata> getMethods(TypeMetadata klasa)
        {
            return klasa.getMethodsList().ToList();
        }

        public List<ParameterMetadata> getParameters(MethodMetadata metoda)
        {
            return metoda.getParameterList().ToList();
        }

        public List<PropertyMetadata> getProperties(TypeMetadata klasa)
        {
            return klasa.getPropertiesList().ToList();
        }

        public MetadataModel(string assemblyFile)
        {
            listaKlas = new Dictionary<string,TypeMetadata>();
            Type[] typy = Assembly.LoadFrom(assemblyFile).GetTypes();
            foreach (Type t in typy)
            {
                buildClass(t);
            }
        }


        public void buildClass(Type type)
        {

           TypeMetadata classObj;
            Logging.Logger.log(System.Diagnostics.TraceEventType.Information, "Loading class: " + type.Name);

            if (listaKlas.TryGetValue(type.Name, out classObj))
            {
                //DO NOTHING
            }
            else
            {
                classObj = new TypeMetadata(type.Name);
                listaKlas.Add(type.Name, classObj);
            }
            if (type.IsPrimitive != true)
            {
                buildMethods(classObj, type);
                buildFields(classObj, type);
                buildProperites(classObj, type);
            }

        }
        public void buildMethods(TypeMetadata klasa, Type type)
        {
            MethodInfo[] metody = type.GetMethods();
            MethodMetadata metoda;
            foreach (MethodInfo index in metody)
            {
               TypeMetadata classObj;
                if (listaKlas.TryGetValue(index.ReturnType.Name, out classObj))
                {
                    metoda = new MethodMetadata(index.Name, classObj);
                }
                else
                {
                    classObj = new TypeMetadata(index.ReturnType.Name);
                    metoda = new MethodMetadata(index.Name, classObj);
                }
                klasa.getMethodsList().Add(metoda);
                buildParameters(metoda, index);

            }
        }
        public void buildMethods(PropertyMetadata property, PropertyInfo type)
        {
            MethodInfo[] metody = type.GetAccessors(true);
            MethodMetadata metoda;
            foreach (MethodInfo index in metody)
            {
               TypeMetadata classObj;
                if (listaKlas.TryGetValue(index.ReturnType.Name, out classObj))
                {
                    metoda = new MethodMetadata(index.Name, classObj);
                }
                else
                {
                    classObj = new TypeMetadata(index.ReturnType.Name);
                    metoda = new MethodMetadata(index.Name, classObj);
                }
                buildParameters(metoda, index);

            }
        }
        public void buildFields(TypeMetadata klasa, Type type)
        {
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo f in fields)
            {
                FieldMetadata field;
               TypeMetadata classObj;
                if (listaKlas.TryGetValue(f.Name, out classObj))
                {
                    field = new FieldMetadata(f.Name, classObj);
                }
                else
                {
                    classObj = new TypeMetadata(f.FieldType.Name);
                    field = new FieldMetadata(f.Name, classObj);
                }
                klasa.getFieldsList().Add(field);
            }
        }
        public void buildProperites(TypeMetadata klasa, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyMetadata property;
               TypeMetadata classObj;
                if (listaKlas.TryGetValue(p.Name, out classObj))
                {
                    property = new PropertyMetadata(p.Name, classObj);
                }
                else
                {
                    classObj = new TypeMetadata(p.PropertyType.Name);
                    property = new PropertyMetadata(p.Name, classObj);
                }
                buildMethods(property, p);
                klasa.getPropertiesList().Add(property);
            }
        }
        public void buildParameters(MethodMetadata metoda, MethodInfo info)
        {
            ParameterInfo[] parameters = info.GetParameters();
            foreach (ParameterInfo p in parameters)
            {
                ParameterMetadata parameter;
               TypeMetadata classObj;
                if (listaKlas.TryGetValue(p.Name, out classObj))
                {
                    parameter = new ParameterMetadata(p.Name, classObj);
                }
                else
                {
                    classObj = new TypeMetadata(p.ParameterType.Name);
                    parameter = new ParameterMetadata(p.Name, classObj);
                }
                metoda.getParameterList().Add(parameter);
            }
        }

        public List<TypeMetadata> getClasses()
        {
            List<TypeMetadata> tmpList = new List<TypeMetadata>();
            foreach (KeyValuePair<string, TypeMetadata> k in listaKlas)
            {
                tmpList.Add(k.Value);
            }
            return tmpList;
        }

        public TypeMetadata getClass(FieldMetadata pole)
        {
            return pole.getType();
        }

        public TypeMetadata getClass(PropertyMetadata property)
        {
            return property.getType();
        }
    }
}

