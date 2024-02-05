using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Generators.Typescript.Templates
{
    public class ClientApiText
    {
        private Api api;
        private string projectName;

        public ClientApiText(Api api, string projectName)
        {
            this.api = api;
            this.projectName = projectName;
        }
        public override string ToString()
        {
            return @$"
export class {api.ControllerName}Client {{
{GetMethods()}
}}";
        }

        private string GetMethods()
        {
            var builder = new StringBuilder();
            foreach (var endpoint in api.Endpoints)
            { 
                builder.AppendLine(new ClientEndpointText(projectName,endpoint).ToString());
            }
            return builder.ToString();
        }
    }
    
}
