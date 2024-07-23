using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOffice_web.Data;
using OutOfOffice_web.Models;
using Microsoft.AspNetCore.Identity;

namespace OutOfOffice_web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.ManagerProjects);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.ManagerProjects)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["PeoplePartner"] = new SelectList(_context.Employees.Where(e=>e.Position==Models.Selection.Position.HRmanager), "Id", "FullName");

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,FullName,Subdivision,Position,Status,PeoplePartner,OutOfOfficeBalance")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Projects, "Id", "Id", employee.Id);
            //ViewData["PeoplePartner"] = new SelectList(_context.PeoplePartners, "Id", "FullName", employee.PeoplePartner);
            ViewData["PeoplePartner"] = new SelectList(_context.Employees.Where(e => e.Position == Models.Selection.Position.HRmanager), "Id", "FullName", employee.PeoplePartner);


            return View(employee);
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Projects, "Id", "Id", employee.Id);
            //ViewData["PeoplePartner"] = new SelectList(_context.PeoplePartners, "Id", "FullName", employee.PeoplePartner);
            ViewData["PeoplePartner"] = new SelectList(_context.Employees.Where(e => e.Position == Models.Selection.Position.HRmanager), "Id", "FullName");


            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Subdivision,Position,Status,PeoplePartner,OutOfOfficeBalance")] Employee employee)
        {
            if (id != employee.Id)
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
                    if (!EmployeeExists(employee.Id))
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
            ViewData["Id"] = new SelectList(_context.Projects, "Id", "Id", employee.Id);
            //ViewData["PeoplePartner"] = new SelectList(_context.PeoplePartners, "Id", "FullName", employee.PeoplePartner);
            ViewData["PeoplePartner"] = new SelectList(_context.Employees.Where(e => e.Position == Models.Selection.Position.HRmanager), "Id", "FullName", employee.PeoplePartner);


            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.ManagerProjects)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        //public int GetCurrentUserId()
        //{
        //    return User.Identity.GetUserId();
        //}
        //private bool EnsureIsUserContact(Contact)
        //{
        //    return EnsureIsUserContact().UserId == GetCurrentUserId();
        //}
    }
}
