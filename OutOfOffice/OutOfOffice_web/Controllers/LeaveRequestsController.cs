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
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRequests
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveRequests.Include(l => l.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveRequests/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // GET: LeaveRequests/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("Id,EmployeeId,AbsenceReason,StartDate,EndDate,Comment,Status")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveRequest);
                await _context.SaveChangesAsync();

                if(leaveRequest.Status == Models.Selection.RequestStatus.Submit)
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == leaveRequest.EmployeeId);
                    var approvalRequest = new ApprovalRequest
                    {
                        ApproverId = employee.PeoplePartner,
                        LeaveRequestId = leaveRequest.Id,
                        Status = Models.Selection.RequestStatus.New
                    };
                    _context.Add(approvalRequest);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,AbsenceReason,StartDate,EndDate,Comment,Status")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (leaveRequest.Status == Models.Selection.RequestStatus.Submit)
                {
                    var employee = await _context.Employees.FirstOrDefaultAsync(a => a.Id == leaveRequest.EmployeeId);
                    var approvalRequest = new ApprovalRequest
                    {
                        ApproverId = employee.PeoplePartner,
                        LeaveRequestId = leaveRequest.Id,
                        Status = Models.Selection.RequestStatus.New
                    };
                    _context.Add(approvalRequest);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.Id == id);
        }
    }
}
