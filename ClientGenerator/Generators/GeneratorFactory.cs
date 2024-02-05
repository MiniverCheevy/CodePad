using ClientGenerator.Interfaces;
using ClientGenerator.Models.Typescript;
using System.Reflection;

namespace ClientGenerator.Models
{
    public class GeneratorFactory
    {
        public const string Typescript = nameof(Typescript);
        private string assemblyLocation;

        public IGenerator GetGenerator(string assemblyLocation, string type, string folder = "/typescript-clients/")
        {
            this.assemblyLocation = assemblyLocation;
            var path = FindSolutionPath();
            if (!string.IsNullOrEmpty(folder))
            {
                path = $"{path}{folder}";
            }
            var request = new GenerationRequest { Assembly = Assembly.LoadFrom(assemblyLocation), OutputPath = path };

            switch (type)
            {
                case Typescript:
                    request.OutputPath = $"{request.OutputPath}";
                    return new TypescriptGenerator()
                    {
                        Request = request
                    };

                default:
                    throw new NotImplementedException();
            }
        }
        private string FindSolutionPath()
        {
            string solutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            while (!string.IsNullOrWhiteSpace(solutionPath))
            {
                if (Directory.GetFiles(solutionPath).Select(c => new FileInfo(c)).Any(c => c.Extension == ".sln"))
                    return solutionPath;

                solutionPath = Directory.GetParent(solutionPath).FullName;

            }
            return solutionPath;
        }
    }
}
