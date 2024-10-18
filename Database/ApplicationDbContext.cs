using Microsoft.EntityFrameworkCore;
using payroll.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<DepartmentModel> Departments { get; set; }
    public DbSet<EmployeeModel> Employees { get; set; }
}
