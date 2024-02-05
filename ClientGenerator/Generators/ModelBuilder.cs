using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClientGenerator.Generators
{
    public abstract class ModelBuilder
    {
        public abstract string RewritePropertyType(Type type);
        public abstract TemplateModel GetTemplateModel(Type modelType, params string[] exclusions);
        public abstract TemplateMember GetTemplateMember(PropertyInfo property);

        public Type[] GetComplexPropertyTypes(Type type)
        {
            var result = new List<Type>();
            GetComplexPropertyTypes(type, ref result);
            return result.Distinct().Where(c => !c.IsScalar()).ToArray();
        }

        public Type[] GetEnumPropertyTypes(Type type)
        {
            var result = new List<Type>();
            GetEnumPropertyTypes(type, ref result);
            return result.Distinct().ToArray();
        }

        private void GetComplexPropertyTypes(Type type, ref List<Type> result)
        {
            if (result.Contains(type))
                return;
            foreach (var property in type.GetProperties())
            {
                var family = GetTypeFamily(property.PropertyType);

                if (family == TypeFamily.Model)
                {
                    if (!result.Contains(property.PropertyType))
                    {
                        result.Add(property.PropertyType);
                        GetComplexPropertyTypes(property.PropertyType, ref result);
                    }
                }
                else if (family == TypeFamily.Collection)
                {
                    if (property.PropertyType.GetGenericArguments().Any())
                    {
                        var collectionType = property.PropertyType.GetGenericArguments().First();
                        if (!result.Contains(collectionType))
                        {
                            result.Add(collectionType);
                            GetComplexPropertyTypes(collectionType, ref result);
                        }
                    }
                }
            }
        }

        private void GetEnumPropertyTypes(Type type, ref List<Type> result)
        {
            foreach (var property in type.GetProperties())
            {
                var family = GetTypeFamily(property.PropertyType);

                if (family == TypeFamily.Enum && !result.Contains(property.PropertyType))
                    result.Add(property.PropertyType);
            }
        }

        protected TypeFamily GetTypeFamily(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            var isString = type == typeof(string);
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
            var isDictionary = type.FullName.StartsWith(typeof(IDictionary).FullName)
                               || type.FullName.StartsWith(typeof(IDictionary<,>).FullName)
                               || type.FullName.StartsWith(typeof(Dictionary<,>).FullName);           

            if (!isString && !isDictionary && isEnumerable)
                return TypeFamily.Collection;
            if (type.IsEnum || nullableUnderlyingType != null && nullableUnderlyingType.IsEnum)
                return TypeFamily.Enum;
            if (type.Module.ScopeName == "CommonLanguageRuntimeLibrary" || type.Module.ScopeName == "System.Private.CoreLib.dll")
                return TypeFamily.System;
            return TypeFamily.Model;
        }

        protected enum TypeFamily
        {
            System = 1,
            Model,
            Collection,
            Enum
        }
    }
}