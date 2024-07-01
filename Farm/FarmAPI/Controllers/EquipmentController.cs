using FarmAPI.DTO;
using FarmAPI.Models;
using FarmAPI.Services.Employees;
using FarmAPI.Services.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace FarmAPI.Controllers
{
    [Route("api/EquipmentAPI")]
    [ApiController]
    public class EquipmentController:ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IEmployeeService _employeeService;
        public EquipmentController(IEquipmentService equipmentService, IEmployeeService employeeService)
        {
            _equipmentService = equipmentService;
            _employeeService = employeeService;
        }

        [HttpGet("Get Equipment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Models.Equipment>>> GetEquipmentAsync()
        {
            return Ok(await _equipmentService.GetEquipmentAsync());
        }

        [HttpGet("GetEquipment/{Id:int}", Name = "GetEquipment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Equipment>> GetEquipmentAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _equipmentService.ExistsById(Id))
            {
                return NotFound();
            }

            var equipment = await _equipmentService.GetEquipmentByIdAsync(Id);

            return Ok(equipment);
        }

        [HttpPost("CreateEquipment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EquipmentDTO>> CreateEquipmentAsync([FromBody] EquipmentDTO equipmentDTO)
        {
            if (equipmentDTO is null)
            {
                return BadRequest(equipmentDTO);
            }

            if (await _equipmentService.ExistsById(equipmentDTO.EquipmentID))
            {
                ModelState.AddModelError("CustomError", "Equipment already exists!");
                return BadRequest(ModelState);
            }

            //Add validation to check if employee exists
            if (!await _employeeService.ExistsById(equipmentDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _equipmentService.CreateEquipmentAsync(equipmentDTO);

            // Return the created equipment as an EquipmentDTO
            return CreatedAtRoute("GetEquipment", new { Id = equipmentDTO.EquipmentID }, equipmentDTO);
        }

        [HttpDelete("DeleteEquipment/{Id}", Name = "DeleteEquipment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEquipmentAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _equipmentService.ExistsById(Id))
            {
                return NotFound();
            }

            await _equipmentService.DeleteEquipmentByIdAsync(Id);
            return Ok("Successfully deleted equipment.");
        }


        [HttpPut("UpdateEquipment/{Id}", Name = "UpdateEquipment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEquipmentAsync(int Id, [FromBody] EquipmentDTO equipmentDTO)
        {
            if (equipmentDTO == null || Id != equipmentDTO.EquipmentID)
            {
                return BadRequest();
            }

            if (!await _equipmentService.ExistsById(Id))
            {
                return NotFound();
            }

            //TO DO
            //Check if such Employee exists
            if (!await _employeeService.ExistsById(equipmentDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee doesn't exist!");
                return BadRequest(ModelState);
            }

            await _equipmentService.EditEquipmentAsync(equipmentDTO);
            return Ok("Successfully edited equipment.");
        }
    }
}
