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

namespace OutOfOffice_web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        [Authorize(Roles = "Administrator, HRManager, ProjectManager")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(p => p.ProjectManager);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Projects/Details/5
        [Authorize(Roles = "Administrator, HRManager, ProjectManager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Administrator, ProjectManager")]
        public IActionResult Create()
        {
            ViewData["ProjectManagerId"] = new SelectList(_context.Employees.Where(e=>e.Position==Models.Selection.Position.ProjectManager), "Id", "FullName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> Create([Bind("Id,ProjectType,StartDate,EndDate,ProjectManagerId,Comment,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjectManagerId"] = new SelectList(_context.Employees.Where(e => e.Position == Models.Selection.Position.ProjectManager), "Id", "FullName", project.ProjectManagerId);

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectManagerId"] = new SelectList(_context.Employees.Where(e=>e.Position==Models.Selection.Position.ProjectManager), "Id", "FullName", project.ProjectManagerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectType,StartDate,EndDate,ProjectManagerId,Comment,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["ProjectManagerId"] = new SelectList(_context.Employees.Where(e=>e.Position == Models.Selection.Position.ProjectManager), "Id", "FullName", project.ProjectManagerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, ProjectManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
