using ClientGenerator.Extensions;
using ClientGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Generators.Typescript.Templates
{
    class EnumText
    {
   
        private TemplateModel model;

        public EnumText(TemplateModel model)
        {
            this.model = model;
        }

        public override string ToString()
        {
            return $@"export enum {model.Name} {{
{GetMembers()}}}
";
        }
      
        private object GetMembers()
        {
            var builder = new StringBuilder();
            foreach (var value in Enum.GetValues(model.Type))
            {
                builder.AppendLine($"     {value} = {(int)value},");
            }
            return builder.ToString();
        }

        
    }
}
