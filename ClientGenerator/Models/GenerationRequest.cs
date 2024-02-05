
using ClientGenerator.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
namespace ClientGenerator.Models
{
    public class GenerationRequest
    {

        public Assembly Assembly { get; set; }
        public string OutputPath { get; set; }
        public List<Api> GetApis()
        {

            var types = Assembly.GetTypesSafetly()
                    .OrderBy(c => c.Name)
                    .ToList();

            return types
                    .Where(c => c.BaseType == typeof(ControllerBase) || c.BaseType?.BaseType == typeof(ControllerBase))
                    .Select(c => new Api(c))
                    .ToList();
        }
        public string GetName()
        {
            var parts = Assembly.FullName.Split(',').First().Split('.');
            parts = parts.Reverse().ToArray();

            var name = parts.First();
            name = name.Replace("Api", string.Empty);
            if (!string.IsNullOrWhiteSpace(name))
                return name;
            else
                name = (parts.Skip(1).FirstOrDefault() ?? "Client");

            return name;

        }
    }
}
