using AutoMapper;
using FarmAPI.DTO;
using FarmAPI.Models;

namespace FarmAPI.AutoMapper
{
    public class CropProfile:Profile
    {
        public CropProfile()
        {
            CreateMap<CropDTO, Crop>();
        }
    }
}
