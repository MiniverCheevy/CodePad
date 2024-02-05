using ClientGenerator.Models;

namespace ClientGenerator.Interfaces
{
    public interface IModelsFileGenerator
    {
        public void GenerateModels(GenerationRequest request);
    }
}
