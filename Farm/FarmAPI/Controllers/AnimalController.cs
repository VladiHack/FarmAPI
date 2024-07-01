using FarmAPI.DTO;
using FarmAPI.Models;
using FarmAPI.Services.Animals;
using FarmAPI.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace FarmAPI.Controllers
{
    [Route("api/AnimalAPI")]
    [ApiController]
    public class AnimalController:ControllerBase
    {
        private readonly IAnimalService _animalService;
        private readonly IEmployeeService _employeeService;
        public AnimalController(IAnimalService animalService, IEmployeeService employeeService)
        {
            _animalService = animalService;
            _employeeService = employeeService;
        }

        [HttpGet("GetAnimals")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalsAsync()
        {
            return Ok(await _animalService.GetAnimalsAsync());
        }

        [HttpGet("GetAnimal/{Id:int}", Name = "GetAnimal")]
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

            var animal = await _animalService.GetAnimalByIdAsync(Id);

            return Ok(animal);
        }

        [HttpPost("CreateAnimal")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AnimalDTO>> CreateAnimalAsync([FromBody] AnimalDTO animalDTO)
        {
            if (animalDTO == null)
            {
                return BadRequest(animalDTO);
            }

            if (await _animalService.ExistsById(animalDTO.AnimalID))
            {
                ModelState.AddModelError("CustomError", "Animal already exists!");
                return BadRequest(ModelState);
            }

            //Add validation to check if employee exists
            if(!await _employeeService.ExistsById(animalDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _animalService.CreateAnimalAsync(animalDTO);

            // Return the created animal as an AnimalDTO
            return CreatedAtRoute("GetAnimal", new { Id = animalDTO.AnimalID}, animalDTO);
        }

        [HttpDelete("DeleteAnimal/{Id}", Name = "DeleteAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAnimalAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _animalService.ExistsById(Id))
            {
                return NotFound();
            }

            await _animalService.DeleteAnimalByIdAsync(Id);
            return Ok("Successfully deleted animal.");
        }


        [HttpPut("UpdateAnimal/{Id}", Name = "UpdateAnimal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAnimalAsync(int Id, [FromBody] AnimalDTO animalDTO)
        {
            if (animalDTO == null || Id != animalDTO.AnimalID)
            {
                return BadRequest();
            }

            if (!await _animalService.ExistsById(Id))
            {
                return NotFound();
            }

            //TO DO
            //Check if such Employee exists
            if (!await _employeeService.ExistsById(animalDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _animalService.EditAnimalAsync(animalDTO);
            return Ok("Successfully edited animal.");
        }

    }
}
