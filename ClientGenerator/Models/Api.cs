using ClientGenerator.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Models
{
    public class Api
    {
        public Type Type { get; set; }

        public List<EndPoint> Endpoints { get; }

        public Api(Type type)
        {
            this.Type = type;
            this.Endpoints = new EndPointFactory().GetEndPoints(this);
        }

        public string? RouteTemplate => Type.GetCustomAttribute<RouteAttribute>(true)?.Template;
        public string? ControllerName => Type.Name.Replace("Controller", string.Empty);        
        public string? RouteFragment => RouteTemplate?.Replace("{controller}", ControllerName);
    }
}
