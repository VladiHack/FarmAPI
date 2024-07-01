using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmAPI.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {

        private readonly FarmDBContext _context;
        private readonly IMapper _mapper;


        public EmployeeService(FarmDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeByIdAsync(int id)
        {
            var employeeToDelete = await GetEmployeeByIdAsync(id);

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task EditEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(int id) => await _context.Employees.AnyAsync(a => a.EmployeeID == id);


        public async Task<Employee> GetEmployeeByIdAsync(int id) => _context.Employees.FirstOrDefault(a => a.EmployeeID == id);


        public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await _context.Employees.ToListAsync();

    }
}
