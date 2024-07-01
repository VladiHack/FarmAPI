using AutoMapper;
using FarmAPI.DTO;
using Equipment = FarmAPI.Models.Equipment;
using FarmAPI.Services.Employees;
using Microsoft.EntityFrameworkCore;
using FarmAPI.Models;
namespace FarmAPI.Services.Equipment
{
    public class EquipmentService : IEquipmentService
    {

        private readonly FarmDBContext _context;
        private readonly IMapper _mapper;

        public EquipmentService(FarmDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEquipmentAsync(EquipmentDTO equipmentDTO)
        {
            var equipment = _mapper.Map<Models.Equipment>(equipmentDTO);

            await _context.Equipment.AddAsync(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEquipmentByIdAsync(int id)
        {
            var equipmentToDelete = await GetEquipmentByIdAsync(id);

            _context.Equipment.Remove(equipmentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task EditEquipmentAsync(EquipmentDTO equipmentDTO)
        {
            var equipment = _mapper.Map<Models.Equipment>(equipmentDTO);

            _context.Equipment.Update(equipment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Equipment.AnyAsync(a => a.EquipmentID == id);


        public async Task<IEnumerable<Models.Equipment>> GetEquipmentAsync() => await _context.Equipment.ToListAsync();


        public async Task<Models.Equipment> GetEquipmentByIdAsync(int id) => _context.Equipment.FirstOrDefault(a => a.EquipmentID == id);

    }
}
