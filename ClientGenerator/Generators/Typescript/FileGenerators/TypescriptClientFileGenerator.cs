using ClientGenerator.Generators.Typescript.Templates;
using ClientGenerator.Helpers;
using ClientGenerator.Interfaces;
using ClientGenerator.Models;

namespace ClientGenerator.Generators.Typescript.FileGenerators
{
    public class TypescriptClientFileGenerator : IClientFileGenrator
    {
        private GenerationRequest request;
        private List<Api> apis;

        public void GenerateClients(GenerationRequest request)
        {
            this.request = request;
            apis = request.GetApis().ToList();
            WriteFiles();
        }

        private void WriteFiles()
        {
            var projectName = request.GetName();
            var writer = new FileWriter(request.OutputPath);
            writer.WriteFiles(new RequestHandlerFile(projectName), new ResponseHandlerFile(projectName), new ClientFile(projectName, apis));
        }
    }
}
