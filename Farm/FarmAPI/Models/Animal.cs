using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmAPI.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnimalID { get; set; }

        [Required]
        public string AnimalType { get; set; }

        [Required]
        public string AnimalName { get; set; }

        [Required]
        public DateTime DateAcquired { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
