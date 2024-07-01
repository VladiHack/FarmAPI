using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.Models
{
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipmentID { get; set; }

        [Required]
        public string EquipmentType { get; set; }
        
        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public decimal OperatingCost { get; set; }
        
        [Required]
        public int EmployeeID { get; set; }



        public Employee Employee { get; set; }
    }
}
