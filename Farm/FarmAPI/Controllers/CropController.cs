using FarmAPI.DTO;
using FarmAPI.Models;
using FarmAPI.Services.Animals;
using FarmAPI.Services.Crops;
using FarmAPI.Services.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace FarmAPI.Controllers
{
    [Route("api/CropAPI")]
    [ApiController]
    public class CropController: ControllerBase
    {
        private readonly ICropsService _cropService;
        private readonly IEmployeeService _employeeService;
        public CropController(ICropsService cropService, IEmployeeService employeeService)
        {
            _cropService = cropService;
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Crop>>> GetCropsAsync()
        {
            return Ok(await _cropService.GetCropsAsync());
        }

        [HttpGet("{Id:int}", Name = "GetCrop")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Crop>> GetCropAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _cropService.ExistsById(Id))
            {
                return NotFound();
            }

            var crop = await _cropService.GetCropByIdAsync(Id);

            return Ok(crop);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CropDTO>> CreateCropAsync([FromBody] CropDTO cropDTO)
        {
            if (cropDTO == null)
            {
                return BadRequest(cropDTO);
            }

            if (await _cropService.ExistsById(cropDTO.CropID))
            {
                ModelState.AddModelError("CustomError", "Crop already exists!");
                return BadRequest(ModelState);
            }

            //Add validation to check if employee exists
            if (!await _employeeService.ExistsById(cropDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _cropService.CreateCropAsync(cropDTO);

            // Return the created crop as a CropDTO
            return CreatedAtRoute("GetCrop", new { Id = cropDTO.CropID }, cropDTO);
        }

        [HttpDelete("{Id}", Name = "DeleteCrop")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCropAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _cropService.ExistsById(Id))
            {
                return NotFound();
            }

            await _cropService.DeleteCropByIdAsync(Id);
            return Ok("Successfully deleted crop.");
        }


        [HttpPut("{Id}", Name = "UpdateCrop")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCropAsync(int Id, [FromBody] CropDTO cropDTO)
        {
            if (cropDTO == null || Id != cropDTO.CropID)
            {
                return BadRequest();
            }

            if (!await _cropService.ExistsById(Id))
            {
                return NotFound();
            }

            //TO DO
            //Check if such Employee exists
            if (!await _employeeService.ExistsById(cropDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _cropService.EditCropAsync(cropDTO);
            return Ok("Successfully edited crop.");
        }
    }
}
