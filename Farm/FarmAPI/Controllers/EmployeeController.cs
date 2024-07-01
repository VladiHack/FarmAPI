using FarmAPI.DTO;
using FarmAPI.Models;
using FarmAPI.Services.Animals;
using FarmAPI.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace FarmAPI.Controllers
{
    [Route("api/EmployeeAPI")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmloyeesAsync()
        {
            return Ok(await _employeeService.GetEmployeesAsync());
        }

        [HttpGet("{Id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployeeAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _employeeService.ExistsById(Id))
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(Id);

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployeeAsync([FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest(employeeDTO);
            }

            if (await _employeeService.ExistsById(employeeDTO.EmployeeID))
            {
                ModelState.AddModelError("CustomError", "Employee already exists!");
                return BadRequest(ModelState);
            }

            await _employeeService.CreateEmployeeAsync(employeeDTO);

            // Return the created employee as an EmployeeDTO
            return CreatedAtRoute("GetEmployee", new { Id = employeeDTO.EmployeeID }, employeeDTO);
        }

        [HttpDelete("{Id}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployeeAsync(int Id)
        {
            if (Id < 0)
            {
                return BadRequest();
            }

            if (!await _employeeService.ExistsById(Id))
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeByIdAsync(Id);
            return Ok("Successfully deleted employee.");
        }


        [HttpPut("{Id}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployeeAsync(int Id, [FromBody] EmployeeDTO employeeDTO)
        {
            if (employeeDTO == null || Id != employeeDTO.EmployeeID)
            {
                return BadRequest();
            }

            if (!await _employeeService.ExistsById(Id))
            {
                return NotFound();
            }

            await _employeeService.EditEmployeeAsync(employeeDTO);
            return Ok("Successfully edited employee.");
        }
    }
}
