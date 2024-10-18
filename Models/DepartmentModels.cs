using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payroll.Models
{
    [Table("department")]
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3)]
        public string?Name { get; set; }
    }
}
