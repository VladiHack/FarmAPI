using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.AutoMapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
