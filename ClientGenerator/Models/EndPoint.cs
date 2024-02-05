using ClientGenerator.Extensions;
using ClientGenerator.Generators.Typescript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace ClientGenerator.Models
{
    public class EndPoint
    {
        private string name;

        private TypeScriptModelBuilder builder => new TypeScriptModelBuilder();
        /// <summary>
        /// Route Template from Controller Method
        /// </summary>
        public string? RouteTemplate => Attribute?.Template;
        /// <summary>
        /// Concatonate Controller Route Fragment and Attribute Route Fragement
        /// </summary>
        public string? RouteFragment => $"{Api.RouteFragment}/{RouteTemplate}";
        public Api Api { get; set; }
        public MethodInfo? Method { get; set; }
        public Type ResponseType { get; }
        public Verb Verb { get; set; }
        public HttpMethodAttribute? Attribute { get; set; }
        public List<Parameter> RouteParameters { get; set; } = new List<Parameter>();
        public List<Parameter> BodyParameters { get; set; } = new List<Parameter> { };
        public List<Parameter> AllParamaters => RouteParameters.Union(BodyParameters).ToList();
        public string Name { get => name; set  { name = value; SetRequestTypeName(); } }
        public string RequestTypeName { get; set; }
        public bool HasNoParamaters => !AllParamaters.Any();
        public bool HasOneParameter => AllParamaters.Count() == 1;
        public bool NeedsGeneratedRequestModel { get; set; }

        public EndPoint(Api api, MethodInfo methodInfo)
        {
            this.Api = api;
            this.Method = methodInfo;
            this.ResponseType = methodInfo.ReturnType;
            foreach (var parameter in methodInfo.GetParameters())
            {
                BuildParameter(parameter);
            }
            
        }
        private void SetRequestTypeName()
        {

            if (HasNoParamaters)
            {
                RequestTypeName = string.Empty;
            }
            else if (HasOneParameter)
            {
                var param = AllParamaters.First();
                var type = param.Type;                
                if (type.IsScalar())
                    RequestTypeName = builder.GetTemplateMember(param.Name, type).Type;
                else
                    RequestTypeName = $"models.{builder.GetTemplateModel(type).Name}";
            }
            else
            {
                NeedsGeneratedRequestModel = true;
                RequestTypeName = $"models.{Api.ControllerName}{Name}Request";               
            }
        }

        private void BuildParameter(ParameterInfo parameter)
        {
            var isRouteParameter = default(bool?);
            var fromRoute = parameter.GetCustomAttribute<FromRouteAttribute>();
            if (fromRoute != null)
            {
                isRouteParameter = true;
            }
            else
            {
                var fromBody = parameter.GetCustomAttribute<FromBodyAttribute>();
                if (fromBody != null)
                {
                    isRouteParameter = false;
                }
            }
            if (isRouteParameter == null)
                isRouteParameter = IsRouteParameter();

            var param = new Parameter { Name = parameter.Name, Type = parameter.ParameterType, IsRoute = isRouteParameter.Value, ParameterInfo = parameter };
            if (isRouteParameter.Value)
                this.RouteParameters.Add(param);
            else
                this.BodyParameters.Add(param);

        }

        private bool IsRouteParameter()
        {
            if (this.Verb == Verb.Get || this.Verb == Models.Verb.Delete)
                return true;

            return false;
        }
    }
}
