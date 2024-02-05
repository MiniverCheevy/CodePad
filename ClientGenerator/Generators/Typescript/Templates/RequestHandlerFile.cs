using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Generators.Typescript.Templates
{
    public class RequestHandlerFile : CodeFile
    {
        private string projectName;

        public override string FileName { get; }
        public override bool GenerateOnce => true;
        public override string Folder { get; }

        public RequestHandlerFile(string projectName)
        {
            this.projectName = projectName;
            this.FileName = $"{projectName.ToKabobCase()}-request-handler.ts";
            this.Folder = projectName.ToKabobCase();
        }


        public override string ToString()
        {
            return $@"{Constants.GeneratedOnceFileDisclaimer}
import {{RequestHandler}} from './../request-handler';

export class {projectName}RequestHandler extends RequestHandler {{
    constructor() {{
        super();
    }}    
}}
";
        }      
    }
}
