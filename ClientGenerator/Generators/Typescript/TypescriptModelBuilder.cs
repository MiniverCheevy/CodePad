using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System.Reflection;

namespace ClientGenerator.Generators.Typescript
{
    public class TypeScriptModelBuilder : ModelBuilder
    {
        public static Dictionary<string, string> Mappings => new Dictionary<string, string>
        {
            {"System.DateTimeOffset", "Date" },
            { "System.DateOnly","Date"},
            {"System.Object", "any" },
            {"System.Int16", "number"},
            {"System.Int32", "number"},
            {"System.Int64", "number"},
            {"System.UInt16", "number"},
            {"System.UInt32", "number"},
            {"System.UInt64", "number"},
            {"System.Decimal", "number"},
            {"System.Single", "number"},
            {"System.Double", "number"},
            {"System.Char", "string"},
            {"System.String", "string"},
            {"System.Boolean", "boolean"},
            {"System.DateTime", "Date"},
            {"System.Guid", "any"},

        };

        public override string RewritePropertyType(Type type)
        {

            if (type == typeof(byte[]))
                return "any";
            type = Nullable.GetUnderlyingType(type) ?? type;
            var name = type.FixUpTypeName();

            if (Mappings.ContainsKey(type.FullName))
            {
                name = Mappings[type.FullName];
                return name;
            }
            var primitiveArrayName = type.FullName.TrimEnd(']').TrimEnd('[');
            if (Mappings.ContainsKey(primitiveArrayName) && type.GetInterface("IEnumerable") != null)
            {
                name = Mappings[primitiveArrayName] + "[]";
                return name;
            }
            if (type.GetInterface("IEnumerable") == null)
                return name.Replace("<", "Of").Replace(">", "").Replace(",", "");

            name = string.Empty;
            var elementType = type.GetGenericArguments().FirstOrDefault();

            name = elementType == null
                ? RewritePropertyType(type).Replace("[]", "")
                : RewritePropertyType(elementType);
            if (!name.Contains("[]"))
                name = $"{name}[]";
            if (type.FullName.Contains("+"))
            {
                var nestedType = elementType ?? type;
                var nestedName = nestedType.FullName.Replace(nestedType.Assembly.ToString(), "")
                    .Split(".").Reverse().First().Replace("+", "");


                if (nestedType.IsGenericType)
                {
                    nestedName = $"{name}{type.GetGenericTypeDefinition()}";
                }
                name = name.Replace(nestedType.Name, nestedName);
            }
            return name.Replace("<", "Of").Replace(">", "").Replace(",", "");
        }

        public override TemplateModel GetTemplateModel(Type modelType, params string[] exclusions)
        {

            exclusions = exclusions ?? new string[] { };

            var result = new TemplateModel();
            result.Name = modelType.FixUpTypeName();
            if (modelType.FullName.Contains("+"))
            {

                result.Name = modelType.FullName.Replace(modelType.Assembly.ToString(), "")
                    .Split(".").Reverse().First().Replace("+", "");


                if (modelType.IsGenericType)
                {
                    result.Name = $"{result.Name}{modelType.GetGenericTypeDefinition()}";
                }

            }

            if (modelType.BaseType != null
                && modelType.BaseType != typeof(object))
                result.BaseClass = RewritePropertyType(modelType.BaseType);

            result.IsEnum = modelType.IsEnum();
            result.Type = modelType;

            var declaredProperties = modelType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance
            );

            foreach (var property in declaredProperties)
            {
                if (exclusions.Contains(property.Name))
                    continue;

                var response = GetTemplateMember(property);
                result.Members.Add(response);

            }
            result.Name = result.Name.Replace("<", "Of").Replace(">", "").Replace(",", "").Replace("`", "");
            return result;
        }

        public override TemplateMember GetTemplateMember(PropertyInfo property)
        {
            var name = property.Name;

            var type = RewritePropertyType(property.PropertyType);
            return new TemplateMember { Name = name, Type = type };
        }
        public TemplateMember GetTemplateMember(string name, Type propertyType)
        {
            var type = RewritePropertyType(propertyType);
            return new TemplateMember { Name = name, Type = type };
        }
    }
}