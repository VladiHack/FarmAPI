using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.AutoMapper
{
    public class AnimalProfile:Profile
    {
        public AnimalProfile() 
        {
            CreateMap<AnimalDTO, Animal>();
        }

    }
}
