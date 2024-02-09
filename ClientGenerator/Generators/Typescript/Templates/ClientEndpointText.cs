using ClientGenerator.Extensions;
using ClientGenerator.Models;

namespace ClientGenerator.Generators.Typescript.Templates
{
    public class ClientEndpointText
    {
        private TypeScriptModelBuilder builder = new TypeScriptModelBuilder();
        private TemplateModel typescriptResponse;
        private string returnTypeName;
        private EndPoint endpoint;
        private string projectName;

        public ClientEndpointText(string projectName, EndPoint endpoint)
        {
            this.endpoint = endpoint;
            this.projectName = projectName;
            this.builder = new TypeScriptModelBuilder();
            var type = endpoint.Method.ReturnType;
            if (type.FullName.StartsWith("System.Threading.Tasks.Task"))
                type = type.GetGenericArguments().FirstOrDefault();
            if (type == null)
                throw new Exception($"Could not determine response type for {endpoint.Api.ControllerName}.{endpoint.Method.Name}");

            this.typescriptResponse = builder.GetTemplateModel(type);
            this.returnTypeName = this.typescriptResponse.Name;
            var nonModelCollections = new string[] { "IEnumerableOf", "EnumerableOf" };
            if (nonModelCollections.Any(c => this.returnTypeName.Contains(c)))
            {
                var newName = this.returnTypeName;
                foreach (var item in nonModelCollections)
                {
                    newName = newName.Replace(item, "");
                }
                this.returnTypeName = $"models.{newName}[]";
            }
            else
            {
                this.returnTypeName = $"models.{this.returnTypeName}";
            }


        }
        public string GetRequestParameterName()
        {
            if (endpoint.HasNoParamaters)
                return string.Empty;
            else if (endpoint.NeedsGeneratedRequestModel)
                return "request";
            else
                return endpoint.AllParamaters.First().Name;

        }
        public string GetRequestParameterNameNullIfEmpty()
        {
            var name = GetRequestParameterName();
            return name == string.Empty ? "null" : name;

        }
        public string GetRequestText()
        {
            if (endpoint.HasNoParamaters)
                return string.Empty;
            return $"{GetRequestParameterName()}: {endpoint.RequestTypeName}";
        }
        public override string ToString()
        {

            return $@"public static {endpoint.Method?.Name ?? endpoint.Name} ({GetRequestText()}) : Promise<{returnTypeName}> {{
    const url = `{GetUrl()}`;
    const restRequest:RestRequest = {{ 
        url:url,
        verb: '{endpoint.Verb.ToString().ToUpper()}',
        request:{GetRequestParameterNameNullIfEmpty()},
        requestHandler: new  {projectName}RequestHandler(),
        responseHandler: new {projectName}ResponseHandler()
    }};
    
   return RestService.build{endpoint.Verb}Request(restRequest);
}}";
        }

        private object GetUrl()
        {
            var hasSingleScalarValue = endpoint.HasOneParameter && endpoint.AllParamaters.First().Type.IsScalar();
            var pattern = hasSingleScalarValue ? "${" : "${request.";
            return endpoint.RouteFragment.Replace("[controller]", endpoint.Api.ControllerName)
                .Replace("{", pattern).TrimEnd('/');
        }
    }
}
