using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGenerator.Extensions;

namespace ClientGenerator.Generators.Typescript.Templates
{
    public class ModelsFile : CodeFile
    {
        public override string FileName { get; }
        public override bool GenerateOnce => false;
        public override string Folder { get; }
        private IEnumerable<TemplateModel> models;

        public ModelsFile(string projectName, IEnumerable<TemplateModel> templates)
        {
            this.FileName = $"{projectName.ToKabobCase()}-models.generated.ts";            
            this.Folder = projectName.ToKabobCase();
            this.models = templates;
        }
        

        public override string ToString()
        {
            return $@"{Constants.GeneratedFileDisclaimer}
{GetModels()}   
";
        }

        private object GetModels()
        {
            var builder = new StringBuilder();
            foreach (var model in models)
            { 
                if (!model.IsEnum)
                    builder.AppendLine(new ModelText(model).ToString());
                else
                    builder.AppendLine(new EnumText(model).ToString());
            }
            return builder.ToString();
        }
    }
}
