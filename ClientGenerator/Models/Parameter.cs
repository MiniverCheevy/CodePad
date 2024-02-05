using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Models
{
    public class Parameter
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public bool IsRoute { get; set; }
        public ParameterInfo ParameterInfo { get; internal set; }
    }
}
