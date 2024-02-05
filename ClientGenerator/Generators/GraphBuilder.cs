using ClientGenerator.Generators;
using ClientGenerator.Helpers;
using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class GraphBuilder<TModelBuilder>
        where TModelBuilder : ModelBuilder, new()
    {
        protected TModelBuilder builder;
        protected readonly StringBuilder output;
        private List<Type> types = new List<Type>();

        public GraphBuilder(Type[] modelTypes)
        {
            if (modelTypes != null)
            {
                var walker = new GraphWalker(new GraphWalkerSettings { IncludeScalarTypes = false, TreatNullableTypesAsDistict = false, }, modelTypes);
                types = walker.GetDistinctTypes().ToList();
            }
            builder = new TModelBuilder();
         }
     
        public List<TemplateModel> GetModels()
        {
            return this.types.Select(c=> builder.GetTemplateModel(c)).Where(c=>c != null).ToList();
        }

    }
}