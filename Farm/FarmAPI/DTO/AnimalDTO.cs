using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.DTO
{
    public class AnimalDTO
    {
        public int AnimalID { get; set; }

        [Required]
        public string AnimalType { get; set; }

        [Required]
        public string AnimalName { get; set; }

        [Required]
        public DateTime DateAcquired { get; set; }

        [Required]
        public int EmployeeID { get; set; }

    }
}
