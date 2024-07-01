using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmAPI.Services.Animals
{
    public class AnimalService : IAnimalService
    {
        private readonly FarmDBContext _context;
        private readonly IMapper _mapper;

        public AnimalService(FarmDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAnimalAsync(AnimalDTO animalDTO)
        {
            var animal = _mapper.Map<Animal>(animalDTO);

            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimalByIdAsync(int id)
        {
            var animalToDelete = await GetAnimalByIdAsync(id);

            _context.Animals.Remove(animalToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task EditAnimalAsync(AnimalDTO animalDTO)
        {
            var animal = _mapper.Map<Animal>(animalDTO);

            _context.Animals.Update(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Animals.AnyAsync(a=>a.AnimalID == id);


        public async Task<Animal> GetAnimalByIdAsync(int id) => _context.Animals.FirstOrDefault(a => a.AnimalID == id);
        

        public async Task<IEnumerable<Animal>> GetAnimalsAsync() => await _context.Animals.ToListAsync();
         
    }
}
