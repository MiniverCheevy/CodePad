using ClientGenerator.Generators.Typescript.FileGenerators;
using ClientGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGenerator.Models.Typescript
{
    public class TypescriptGenerator : IGenerator
    {      
        public IModelsFileGenerator ModelGenerator => new TypescriptModelsFileGenerator();

        public IClientFileGenrator ClientGenrator => new TypescriptClientFileGenerator();

        public GenerationRequest Request { get; set; }

        
    }
}
