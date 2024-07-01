using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.Services.Equipment
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Models.Equipment>> GetEquipmentAsync();
        Task<bool> ExistsById(int id);
        Task<Models.Equipment> GetEquipmentByIdAsync(int id);
        Task CreateEquipmentAsync(EquipmentDTO equipmentDTO);
        Task DeleteEquipmentByIdAsync(int id);
        Task EditEquipmentAsync(EquipmentDTO equipmentDTO);

    }
}
