using FarmAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.DTO
{
    public class CropDTO
    {
        public int CropID { get; set; }
        [Required]
        public string CropName { get; set; }
        [Required]
        public DateTime HarvestDate { get; set; }
        [Required]
        public decimal YieldPerAcre { get; set; }
        [Required]
        public int EmployeeID { get; set; }
    }
}
