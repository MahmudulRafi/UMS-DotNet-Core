using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Data;
using EMS.Models;

namespace EMS.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UMSContext _context;

        public EmployeesController(UMSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.Include(x => x.Department).Include(x => x.Address).Include(x => x.Educations).ThenInclude(x => x.Degree).Where(x => x.EmployeeID == id)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);

           
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            Employee employee = new Employee();
            employee.Educations.Add(new Education { EmployeeID = 1 });
            

            ViewBag.departments = new SelectList(_context.Departments.ToList(), "DepartmentID","Name");
            ViewBag.degrees = new SelectList(_context.Degrees.ToList(), "DegreeID", "Name");
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

                // Create Employee 
                var newEmployee = new Employee()
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    DateOfBirth = employee.DateOfBirth.Date,
                    DateOfJoining = employee.DateOfJoining.Date,
                    DepartmentID = employee.DepartmentID,
                };

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();

                // Create Address 

                var address = new Address()
                {
                    HouseName = employee.Address.HouseName,
                    HouseNo = employee.Address.HouseNo,
                    RoadNo = employee.Address.RoadNo,
                    WardNo = employee.Address.WardNo,
                    City = employee.Address.City,
                    Country = employee.Address.Country,
                    EmployeeID = newEmployee.EmployeeID
                };

                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                // Create Education
                var educations = new List<Education>();
                foreach (var item in employee.Educations) 
                {
                    var education = new Education()  
                    {
                        EmployeeID = newEmployee.EmployeeID,
                        DegreeID = item.DegreeID,
                        Institution = item.Institution,
                        PassingYear = item.PassingYear,
                        GPA = item.GPA,
                    };
                    educations.Add(education);
                }

                _context.Educations.AddRange(educations);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.departments = new SelectList(_context.Departments.ToList(), "DepartmentID", "Name");
            ViewBag.degrees = new SelectList(_context.Degrees.ToList(), "DegreeID", "Name");

            var employee = await _context.Employees.Include(x => x.Department).Include(x => x.Address).Include(x => x.Educations).Where(x => x.EmployeeID == id)
              .FirstOrDefaultAsync(m => m.EmployeeID == id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationID == id);
        }
    }
}
