﻿using System;
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
    public class ApprovalRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApprovalRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApprovalRequests
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApprovalRequests
                .Include(a => a.Approvers)
                .Include(a => a.LeaveRequest).ThenInclude(a=>a.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApprovalRequests/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalRequest = await _context.ApprovalRequests
                .Include(a => a.Approvers)
                .Include(a => a.LeaveRequest).ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            return View(approvalRequest);
        }

        // GET: ApprovalRequests/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["Approvers"] = new SelectList(_context.Employees, "Id", "FullName");
            ViewData["LeaveRequestId"] = new SelectList(_context.LeaveRequests, "Id", "Id");
            return View();
        }

        // POST: ApprovalRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,ApproverId,LeaveRequestId,Status,Comment,Approvers")] ApprovalRequest approvalRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(approvalRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Approvers"] = new SelectList(_context.Employees, "Id", "FullName", approvalRequest.ApproverId);
            ViewData["LeaveRequestId"] = new SelectList(_context.LeaveRequests, "Id", "Id", approvalRequest.LeaveRequestId);
            return View(approvalRequest);
        }

        // GET: ApprovalRequests/Edit/5
        [Authorize(Roles = "Administrator,HRManager,ProjectManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalRequest = await _context.ApprovalRequests.FindAsync(id);
            if (approvalRequest == null)
            {
                return NotFound();
            }
            ViewData["Approvers"] = new SelectList(_context.Employees, "Id", "FullName", approvalRequest.Approvers);
            ViewData["LeaveRequestId"] = new SelectList(_context.LeaveRequests, "Id", "Id", approvalRequest.LeaveRequestId);
            return View(approvalRequest);
        }

        // POST: ApprovalRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,HRManager,ProjectManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApproverId,LeaveRequestId,Status,Comment,Approvers")] ApprovalRequest approvalRequest)
        {
            if (id != approvalRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(approvalRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalRequestExists(approvalRequest.Id))
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
            ViewData["Approvers"] = new SelectList(_context.Employees, "Id", "FullName", approvalRequest.Approvers);
            ViewData["LeaveRequestId"] = new SelectList(_context.LeaveRequests, "Id", "Id", approvalRequest.LeaveRequestId);
            return View(approvalRequest);
        }

        // GET: ApprovalRequests/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalRequest = await _context.ApprovalRequests
                .Include(a => a.Approvers)
                .Include(a => a.LeaveRequest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            return View(approvalRequest);
        }

        // POST: ApprovalRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalRequest = await _context.ApprovalRequests.FindAsync(id);
            if (approvalRequest != null)
            {
                _context.ApprovalRequests.Remove(approvalRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalRequestExists(int id)
        {
            return _context.ApprovalRequests.Any(e => e.Id == id);
        }
    }
}
