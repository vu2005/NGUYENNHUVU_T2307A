using payroll.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payroll.Models
{
    [Table("employee")]
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(33)]
        public string ?Name { get; set; }

        [Required]
        [Range(0, 9999)] // Salary must be less than 10000
        public int Salary { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        // Foreign key for department
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentModel ?Department { get; set; }
    }
}
