using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Models
{
    public class TemplateModel
    {
        public string Name { get; set; }
        public List<TemplateMember> Members { get; set; } = new List<TemplateMember>();
        public string BaseClass { get; set; }
        public Type Type { get; set; }
        public bool IsEnum { get; set; }
    }
    public class TemplateMember
    { 
        public string Name { get; set; }
        public string Type { get; set; }        
    }
}
