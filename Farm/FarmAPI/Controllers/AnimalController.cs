using FarmAPI.Models;
using FarmAPI.Services.Animals;
using Microsoft.AspNetCore.Mvc;

namespace FarmAPI.Controllers
{
    [Route("api/AnimalAPI")]
    [ApiController]
    public class AnimalController:ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAppointmentsAsync()
        {
            return Ok(await _animalService.GetAnimalsAsync());
        }

        [HttpGet("{Id:int}", Name = "GetAnimal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Animal>> GetAnimalAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _animalService.ExistsById(Id))
            {
                return NotFound();
            }

            var appointment = await _animalService.GetAnimalByIdAsync(Id);

            return Ok(appointment);
        }
    }
}
