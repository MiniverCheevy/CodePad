using ClientGenerator.Extensions;
using ClientGenerator.Generators.Typescript;
using ClientGenerator.Generators.Typescript.Templates;
using ClientGenerator.Helpers;
using ClientGenerator.Interfaces;
using ClientGenerator.Models;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;

namespace ClientGenerator.Generators.Typescript.FileGenerators
{
    public class TypescriptModelsFileGenerator : IModelsFileGenerator
    {
        private List<Api> apis;
        private HashSet<Type> types;
        private GenerationRequest request;
        private List<TemplateModel> templates = new List<TemplateModel>();
        private GraphBuilder<TypeScriptModelBuilder> builder;

        public void GenerateModels(GenerationRequest request)
        {
            this.request = request;
            DiscoverRootTypes();
            builder = new GraphBuilder<TypeScriptModelBuilder>(types.ToArray());
            BuildModels();
            WriteFiles();
        }

        private void DiscoverRootTypes()
        {
            apis = request.GetApis().ToList();
            var allEndpoints = apis.SelectMany(c => c.Endpoints).ToList();
            var allTypes = allEndpoints
                .Select(c => c.ResponseType).ToList();

            allTypes.AddRange(allEndpoints.SelectMany(c => c.RouteParameters).Select(c => c.Type).ToList());
            allTypes.AddRange(allEndpoints.SelectMany(c => c.BodyParameters).Select(c => c.Type).ToList());

            types = allTypes.Distinct().ToHashSet();
        }

        private void BuildModels()
        {
            templates = builder.GetModels();
            BuildVirtualModelsForRequests();
        }

        private void BuildVirtualModelsForRequests()
        {
            var factory = new EndpointRequestModelFactory();
            var endpoints = apis.SelectMany(c => c.Endpoints);
            var requestModels = endpoints.Where(c=> c.NeedsGeneratedRequestModel).Select(c => factory.GetTemplateModel(c)).ToArray();
            templates.AddRange(requestModels);
        }

        private void WriteFiles()
        {
            var writer = new FileWriter(request.OutputPath);
            var modelsFile = new ModelsFile(request.GetName(), templates);
            writer.WriteFiles(modelsFile);
        }

    }
}
