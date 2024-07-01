using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public DateTime HireDate { get; set; }

        public ICollection<Crop> Crops { get; set; }
        public ICollection<Animal> Animals { get; set; }
        public ICollection<Equipment> Equipment { get; set; }
    }
}
