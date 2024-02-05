using ClientGenerator.Extensions;

namespace ClientGenerator.Helpers
{
    public class GraphWalkerSettings
    {
        /// <summary>
        /// Return both T and Nullable<T> in the list of distinct types
        /// </summary>
        public bool TreatNullableTypesAsDistict { get; set; }

        /// <summary>
        /// Include Primitives in addition to string, date and guid
        /// </summary>
        public bool IncludeScalarTypes { get; set; }
    }


    public class GraphWalker
    {
        private readonly HashSet<Type> distinctTypes = new HashSet<Type>();
        private readonly GraphWalkerSettings settings = new GraphWalkerSettings();
        private Type[] types;

        public GraphWalker(params Type[] types)
        {
            this.types = types.Distinct().ToArray();
        }

        public GraphWalker(GraphWalkerSettings settings, params Type[] types) : this(types)
        {
            this.settings = settings;
        }

        public HashSet<Type> GetDistinctTypes()
        {
            foreach (var type in types)
            {
                Read(type);
            }
            var orderedResult = new HashSet<Type>();
            var names = new HashSet<string>();
            foreach (var item in distinctTypes.Distinct().OrderBy(c => c.Name))
            {
                if (!orderedResult.Contains(item))
                {
                    names.Add(item.FixUpTypeName());
                    orderedResult.Add(item);
                }
            }
            return orderedResult;
        }       

        private void Read(Type type)
        {
            var name = type.Name;
            var isNullable = type.IsNullable();
            var isScalar = type.IsScalar();
            var isEnum = type.IsEnum();

            if (type.IsGenericType())
            {
                foreach (var argument in type.GetGenericArgumentsList())
                {
                    Read(argument);
                }
            }
            if (type.Name.Contains("KeyValuePair"))
            {
                distinctTypes.Add(type);
                return;
            }
            if (isScalar && !settings.IncludeScalarTypes && !isEnum && !isNullable)
                return;

            if (isNullable && !settings.TreatNullableTypesAsDistict)
            {
                type = type.GetGenericArgumentsList().First();
                isNullable = type.IsNullable();
                isScalar = type.IsScalar();
                isEnum = type.IsEnum();
            }
            

            if (type.FullName.StartsWith("System.") && !isScalar && !isNullable)
                return;

            if (distinctTypes.Contains(type))
                return;

            if (isEnum)
            {
                distinctTypes.Add(type);
                return;
            }
            if (isNullable && settings.TreatNullableTypesAsDistict)
            {
                distinctTypes.Add(type);
                return;
            }
            if (isScalar && settings.IncludeScalarTypes)
            {
                distinctTypes.Add(type);
                return;
            }
            else if (isScalar)
                return;

            distinctTypes.Add(type);

            foreach (var property in type.GetPropertiesList())
            {
                Read(property.PropertyType);
            }
        }
    }
}