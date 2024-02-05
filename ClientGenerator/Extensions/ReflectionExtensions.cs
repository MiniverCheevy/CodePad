using Microsoft.Extensions.DependencyModel;
using System.Collections;
using System.Reflection;

namespace ClientGenerator.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute? GetCustomAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetCustomAttributes(typeof(TAttribute), true).Select(c => c as TAttribute)
                .Where(c => c != null)
                .FirstOrDefault();
        }
        public static bool IsNullable(this Type type)
        {
            return type.GetTypeInfo().IsGenericType &&
                   type.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        public static List<PropertyInfo> GetPropertiesList(this Type type)
        {
            return type.GetTypeInfo().GetProperties().ToList();
        }

        public static List<Type> GetGenericArgumentsList(this Type type)
        {
            return type.GetTypeInfo().GetGenericArguments().ToList();
        }

        public static bool IsEnumerable(this Type t)
        {
            return typeof(IEnumerable).IsAssignableFrom(t);
        }

        public static bool IsScalar(this Type t)
        {

            string[] primitives = new string[] {
             "DateTimeOffset",
             "Int16",
             "Int32",
             "Int64",
             "UInt16",
             "UInt32",
             "UInt64",
             "Decimal",
             "Single",
             "Double",
             "Char",
             "String",
             "Boolean",
             "DateTime",
             "DateOnly",
             "Guid"
            };
            if (t.Name.ToLower().Contains("nullable") && primitives.Contains(t.GetGenericArguments().First().Name))
                return true;

            if (primitives.Contains(t.Name))
                return true;

            if (t.GetTypeInfo().IsEnum)
                return true;

            if (t.GetProperties(BindingFlags.Instance | BindingFlags.Public).All(c => !c.CanWrite) && t.Namespace != null && t.Namespace.StartsWith("System."))
                return true;

            return false;
        }


        public static Type[] GetTypesSafetly(this Assembly assembly)
        {
            try
            {

                var libraries = DependencyContext.Default.RuntimeLibraries;
                var library = libraries.FirstOrDefault(lib => lib.Name == "Microsoft.AspNetCore.Mvc.Core");
                if (library != null)
                {
                    var mvc = Assembly.Load(new AssemblyName(library.Name));
                    // Use the assembly
                }


                return assembly.GetTypes().Where(c => c != null).ToArray();
            }
            catch (ReflectionTypeLoadException rtl)
            {
                return rtl?.Types?.Where(c => c != null)?.ToArray() ?? Array.Empty<Type>();
            }
        }

        public static string? GetTypeNameWithoutGenericArguments(this Type type)
        {
            if (!type.GetGenericArguments().Any())
                return null;

            var index = 0;
            var testedTypeName = type.Name;
            index = testedTypeName.IndexOf("`");
            if (index == -1)
                return null;
            testedTypeName = testedTypeName.Substring(0, index);
            return testedTypeName;
        }

        public static string? GetTypeFullNameWithoutGenericArguments(this Type type)
        {
            if (!type?.GetGenericArguments().Any() ?? false)
                return null;

            var index = 0;
            if (type != null)
            {
                var testedTypeName = type.FullName;
                index = testedTypeName?.IndexOf("`") ?? -1;
                if (index == -1)
                    return null;
                testedTypeName = testedTypeName?.Substring(0, index);
                return testedTypeName;
            }
            return null;
        }

        public static List<KeyValuePair<Type, string>> GetParameterDictionary(this MethodInfo methodInfo)
        {
            var result = new List<KeyValuePair<Type, string>>();
            foreach (var info in methodInfo.GetParameters())
            {
                result.Add(new KeyValuePair<Type, string>(info.ParameterType, info?.Name ?? "UnnamedProperty"));
            }
            return result;
        }

        public static string GetParametersForCodeGeneration(this MethodInfo methodInfo)
        {
            var result = string.Empty;
            foreach (var info in methodInfo.GetParameters())
            {
                result += info.ParameterType.FixUpTypeName();
                result += " ";
                result += info.Name;
                result += ", ";
            }

            result = result.TrimEnd(' ').TrimEnd(',');
            return result;
        }

        /// <summary>
        ///     convert scalar Type names into compilable c# type names
        /// </summary>
        public static string FixUpScalarTypeName(this Type t)
        {
            var type = t.Name;
            type = type.Replace("System.", "");

            switch (type)
            {
                case "String":
                    return "string";
                case "Byte":
                    return "byte";
                case "Byte[]":
                    return "byte[]";
                case "Int16":
                    return "short";
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
                case "Char":
                    return "char";
                case "Single":
                    return "float";
                case "Double":
                    return "double";
                case "Boolean":
                    return "bool";
                case "Decimal":
                    return "decimal";
                case "SByte":
                    return "sbyte";
                case "UInt16":
                    return "ushort";
                case "UInt32":
                    return "uint";
                case "UInt64":
                    return "ulong";
                case "Object":
                    return "object";
                case "Void":
                    return "void";
                default:

                    return type;
            }
        }

        /// <summary>
        ///     /// convert Type name into compilable c# type names
        /// </summary>
        public static string FixUpTypeName(this Type type, bool fullyQualified = false)
        {
            var result = type.FixUpScalarTypeName();
            result = result.Replace(type.Assembly.ToString(), "");
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                result = $"{Nullable.GetUnderlyingType(type)?.FixUpScalarTypeName()}?";
            }
            else if (type.GetTypeInfo().IsGenericType)
            {
                result = GetGenericTypeDefinition(type);
            }
            return result;
        }

        public static string GetGenericTypeDefinition(this Type type)
        {
            var result = string.Empty;
            var inner = string.Empty;
            var outer = type.GetGenericTypeDefinition().Name;
            foreach (var t in type.GetGenericArguments())
            {
                if (t.GetTypeInfo().IsGenericType)
                {
                    inner = GetGenericTypeDefinition(t);
                }
                else
                {
                    inner += t.FixUpTypeName();
                    inner += ",";
                }
            }
            inner = inner.TrimEnd(",".ToCharArray());
            var ary = outer.Split(@"`".ToCharArray());
            outer = ary[0];
            result = string.Format("{1}<{0}>", inner, outer);
            return result;
        }

        public static string GetMethodName(this MethodBase input)
        {
            return input.Name;
        }

        public static bool DoesImplementInterfaceOf(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Contains(interfaceType);
        }

        public static bool IsGenericCollectionTypeOf(this Type type, Type typeDefinition)
        {
            if (type.GetTypeInfo().IsGenericType)
            {
                var collectionTypeInterfaces = new[] { typeof(IEnumerable), typeof(IList), typeof(ICollection) };
                var isCollectionType = type.GetInterfaces().Intersect(collectionTypeInterfaces).Any();
                var canConstructTypeDefinition =
                    type.GetGenericArguments().Any(c => c.GetInterfaces().Contains(typeDefinition));

                if (isCollectionType && canConstructTypeDefinition)
                    return true;
            }
            return false;
        }
    }
}