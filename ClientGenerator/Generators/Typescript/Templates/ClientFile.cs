using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System.Text;

namespace ClientGenerator.Generators.Typescript.Templates
{
    public class ClientFile : CodeFile
    {
        public override string FileName { get; }
        public override bool GenerateOnce => false;
        public override string Folder { get; }
        private IEnumerable<Api> models;
        private string projectName;

        public ClientFile(string projectName, IEnumerable<Api> apis)
        {
            this.FileName = $"{projectName.ToKabobCase()}-clients.generated.ts";
            this.Folder = projectName.ToKabobCase();
            this.models = apis;
            this.projectName = projectName;
        }


        public override string ToString()
        {
            return $@"{Constants.GeneratedFileDisclaimer}
import * as models from './{projectName.ToKabobCase()}-models.generated';
import {{ {projectName}RequestHandler }} from './{projectName.ToKabobCase()}-request-handler';
import {{ {projectName}ResponseHandler }} from './{projectName.ToKabobCase()}-response-handler';
import {{ RestRequest,RestService }} from '../';

{GetModels()}   
";
        }

        private object GetModels()
        {
            var builder = new StringBuilder();
            foreach (var model in models)
            {
                builder.Append(new ClientApiText(model, projectName).ToString());
            }
            return builder.ToString();
        }
    }
}
