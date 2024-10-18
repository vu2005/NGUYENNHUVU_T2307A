using Microsoft.AspNetCore.Mvc;
using payroll.Models;
using System.Linq;

public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all departments
    public IActionResult Index()
    {
        var departments = _context.Departments.ToList();
        return View(departments);
    }

    // Add a new department (GET)
    public IActionResult Create()
    {
        return View();
    }

    // Add a new department (POST)
    [HttpPost]
    public IActionResult Create(DepartmentModel department)
    {
        if (ModelState.IsValid)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); // Redirect to Index after creating
        }
        return View(department);
    }

    // Edit a department (GET)
    public IActionResult Edit(int id)
    {
        var department = _context.Departments.Find(id);
        return department == null ? NotFound() : View(department);
    }

    // Edit a department (POST)
    [HttpPost]
    public IActionResult Edit(DepartmentModel department)
    {
        if (ModelState.IsValid)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); // Redirect to Index after editing
        }
        return View(department);
    }

    // Delete a department (GET)
    public IActionResult Delete(int id)
    {
        var department = _context.Departments.Find(id);
        return department == null ? NotFound() : View(department);
    }

    // Delete a department (POST)
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        Console.WriteLine($"DeleteConfirmed called with id: {id}"); // Log the ID
        var department = _context.Departments.Find(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
            Console.WriteLine("Department deleted successfully.");
        }
        else
        {
            Console.WriteLine("Department not found.");
        }
        return RedirectToAction(nameof(Index));
    }
}
