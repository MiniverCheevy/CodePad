using ClientGenerator.Models;

namespace ClientGenerator.Interfaces
{
    public interface IClientFileGenrator
    {
        public void GenerateClients(GenerationRequest request);
    }
}
