using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.DTO
{
    public class EquipmentDTO
    {

        public int EquipmentID { get; set; }

        [Required]
        public string EquipmentType { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal OperatingCost { get; set; }

        [Required]
        public int EmployeeID { get; set; }
    }
}
