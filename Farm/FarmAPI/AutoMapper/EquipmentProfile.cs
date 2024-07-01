using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.AutoMapper
{
    public class EquipmentProfile:Profile
    {
        public EquipmentProfile()
        {
            CreateMap<EquipmentDTO, Equipment>();
        }
    }
}
