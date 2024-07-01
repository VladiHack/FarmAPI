using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<bool> ExistsById(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(EmployeeDTO employeeDTO);
        Task DeleteEmployeeByIdAsync(int id);
        Task EditEmployeeAsync(EmployeeDTO employeeDTO);

    }
}
