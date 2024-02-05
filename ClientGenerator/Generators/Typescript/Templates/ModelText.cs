using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Generators.Typescript.Templates
{
    class ModelText
    {
        private TemplateModel model;

        public ModelText(TemplateModel model)
        {
            this.model = model;
        }

        public override string ToString()
        {
return $@"export class {model.Name} {{
{GetMembers()}}}
";
        }        

        private object GetBaseType()
        {
            if (!string.IsNullOrWhiteSpace(model.BaseClass))
                return $"extends {model.BaseClass}";
            return string.Empty;
        }
        private object GetMembers()
        {
            var builder = new StringBuilder();
            foreach(var member in model.Members)
                builder.AppendLine(GetMember(member));

            return builder.ToString();
        }

        private string GetMember(TemplateMember member)
        {
            return $"     {member.Name.ToCamelCase()}? : {member.Type}";
        }
    }
}
