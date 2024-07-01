using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmAPI.Services.Crops
{
    public class CropsService : ICropsService
    {
        private readonly FarmDBContext _context;
        private readonly IMapper _mapper;

        public CropsService(FarmDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateCropAsync(CropDTO cropDTO)
        {
            var crop = _mapper.Map<Crop>(cropDTO);

            await _context.Crops.AddAsync(crop);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCropByIdAsync(int id)
        {
            var cropToDelete = await GetCropByIdAsync(id);

            _context.Crops.Remove(cropToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task EditCropAsync(CropDTO cropDTO)
        {
            var crop = _mapper.Map<Crop>(cropDTO);

            _context.Crops.Update(crop);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Crops.AnyAsync(a => a.CropID == id);


        public async Task<Crop> GetCropByIdAsync(int id) => await _context.Crops.FirstOrDefaultAsync(a => a.CropID == id);


        public async Task<IEnumerable<Crop>> GetCropsAsync() => await _context.Crops.ToListAsync();
       
    }
}
