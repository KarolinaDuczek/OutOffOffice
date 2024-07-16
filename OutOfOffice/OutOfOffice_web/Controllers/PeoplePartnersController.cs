using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OutOfOffice_web.Data;
using OutOfOffice_web.Models;

namespace OutOfOffice_web.Controllers
{
    public class PeoplePartnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeoplePartnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PeoplePartners
        public async Task<IActionResult> Index()
        {
            return View(await _context.PeoplePartners.ToListAsync());
        }

        // GET: PeoplePartners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peoplePartner = await _context.PeoplePartners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peoplePartner == null)
            {
                return NotFound();
            }

            return View(peoplePartner);
        }

        // GET: PeoplePartners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeoplePartners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName")] PeoplePartner peoplePartner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peoplePartner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(peoplePartner);
        }

        // GET: PeoplePartners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peoplePartner = await _context.PeoplePartners.FindAsync(id);
            if (peoplePartner == null)
            {
                return NotFound();
            }
            return View(peoplePartner);
        }

        // POST: PeoplePartners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] PeoplePartner peoplePartner)
        {
            if (id != peoplePartner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peoplePartner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeoplePartnerExists(peoplePartner.Id))
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
            return View(peoplePartner);
        }

        // GET: PeoplePartners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peoplePartner = await _context.PeoplePartners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peoplePartner == null)
            {
                return NotFound();
            }

            return View(peoplePartner);
        }

        // POST: PeoplePartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peoplePartner = await _context.PeoplePartners.FindAsync(id);
            if (peoplePartner != null)
            {
                _context.PeoplePartners.Remove(peoplePartner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeoplePartnerExists(int id)
        {
            return _context.PeoplePartners.Any(e => e.Id == id);
        }
    }
}
