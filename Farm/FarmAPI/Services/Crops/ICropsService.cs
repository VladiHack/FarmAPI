using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.Services.Crops
{
    public interface ICropsService
    {
        Task<IEnumerable<Crop>> GetCropsAsync();
        Task<bool> ExistsById(int id);
        Task<Crop> GetCropByIdAsync(int id);
        Task CreateCropAsync(CropDTO cropDTO);
        Task DeleteCropByIdAsync(int id);
        Task EditCropAsync(CropDTO cropDTO);
    }
}
