using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGenerator.Extensions;
using System.Reflection;

namespace ClientGenerator.Generators.Typescript
{
    public class EndpointRequestModelFactory
    {
        TypeScriptModelBuilder builder = new TypeScriptModelBuilder();
        public TemplateModel GetTemplateModel(EndPoint endpoint)
        {
            var model = new TemplateModel { Name = endpoint.RequestTypeName.Replace("models.", "") };
            foreach (var parameter in endpoint.BodyParameters.Union(endpoint.RouteParameters).ToArray())
            {
                model.Members.Add(new TemplateMember { 
                 Name = parameter.Name.ToCamelCase(),
                 Type = builder.GetTemplateMember(parameter.Name, parameter.Type).Type
                });
            }
            return model;
        }
    }
}
