using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using payroll.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace payroll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all employees with eager loading for the Department entity
        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.Department).ToList();
            return View(employees);
        }

        // GET: Create Employee
        public IActionResult Create()
        {
            PopulateDepartments(); // Populate departments for the dropdown
            return View();
        }

        // POST: Create Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Re-populate departments if model state is invalid
            PopulateDepartments();
            return View(employee);
        }

        // GET: Edit Employee
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound(); // Return 404 if the employee is not found
            }

            // Populate departments and set the selected department for the employee
            PopulateDepartments(employee.DepartmentId);
            return View(employee); // Pass the employee model to the view
        }

        // POST: Edit Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Employees.Update(employee);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.Id == employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Rethrow the exception if the employee still exists
                    }
                }
            }

            // Re-populate departments if model state is invalid
            PopulateDepartments(employee.DepartmentId);
            return View(employee);
        }

        // GET: Delete Employee
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Confirm Deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Helper method to populate departments
        private void PopulateDepartments(int? selectedDepartmentId = null)
        {
            var departments = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", selectedDepartmentId);
        }
    }
}
