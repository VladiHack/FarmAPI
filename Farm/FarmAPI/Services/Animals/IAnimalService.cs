using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.Services.Animals
{
    public interface IAnimalService
    {
        Task<IEnumerable<Animal>> GetAnimalsAsync();
        Task<bool> ExistsById(int id);
        Task<Animal> GetAnimalByIdAsync(int id);
        Task CreateAnimalAsync(AnimalDTO animalDTO);
        Task DeleteAnimalByIdAsync(int id);
        Task EditAnimalAsync(AnimalDTO animalDTO);

    }
}
