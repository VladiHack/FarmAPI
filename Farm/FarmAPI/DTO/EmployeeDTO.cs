using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmAPI.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public DateTime HireDate { get; set; }

    }
}
