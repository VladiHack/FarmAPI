using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmAPI.Models
{
    public class Crop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CropID { get; set; }

        [Required]
        public string CropName { get; set; }
        [Required]
        public DateTime HarvestDate { get; set; }
        [Required]
        public decimal YieldPerAcre { get; set; }
        [Required]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
