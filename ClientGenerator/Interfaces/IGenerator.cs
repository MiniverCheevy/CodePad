using ClientGenerator.Extensions;
using ClientGenerator.Models;


namespace ClientGenerator.Interfaces
{
    public interface IGenerator
    {

        public IModelsFileGenerator ModelGenerator { get; }
        public IClientFileGenrator ClientGenrator { get; }
        public GenerationRequest Request { get; set; }
        public void GenerateModels()
        {
            ModelGenerator.GenerateModels(Request);
        }
        public void GenerateClients()
        {
            ClientGenrator.GenerateClients(Request);
        }        
    }
}
