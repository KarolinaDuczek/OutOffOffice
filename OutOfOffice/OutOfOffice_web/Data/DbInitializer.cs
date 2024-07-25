using Bogus;
using Microsoft.EntityFrameworkCore;
using OutOfOffice_web.Models;
using System.Collections.ObjectModel;

namespace OutOfOffice_web.Data;

public class DbInitializer
{
    private readonly ApplicationDbContext _context;
    public DbInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public static void Initialize(ApplicationDbContext _context)
    {
        _context.Database.EnsureCreated();

        if (_context.Employees.Any())
            return;

        var employees = new List<Employee>
        {
            new Employee {FullName = "Adam Nowak", Subdivision = Models.Selection.Subdivision.HR, Position = Models.Selection.Position.HRmanager, Status = Models.Selection.Status.Active, PeoplePartner = 1, OutOfOfficeBalance = 20 },
            new Employee {FullName = "Anna Kowalska", Subdivision = Models.Selection.Subdivision.IT, Position = Models.Selection.Position.ProjectManager, Status = Models.Selection.Status.Active, PeoplePartner = 1, OutOfOfficeBalance = 22 },
            new Employee {FullName = "Mateusz Borkowski", Subdivision = Models.Selection.Subdivision.Production, Position = Models.Selection.Position.Specialist, Status = Models.Selection.Status.Active, PeoplePartner = 1, OutOfOfficeBalance = 26 },
        };
        foreach(var employee in employees)
        {
            _context.Employees.Add(employee);
        }
         _context.SaveChanges();

        var leaveRequests = new List<LeaveRequest>
        {
            new LeaveRequest {EmployeeId = 2, AbsenceReason = Models.Selection.AbsenceReason.SickLeave, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7), Comment = "-", Status = Models.Selection.RequestStatus.New },
            new LeaveRequest {EmployeeId = 3, AbsenceReason = Models.Selection.AbsenceReason.VacationLeave, StartDate = DateTime.Today.AddDays(12), EndDate = DateTime.Today.AddDays(26), Comment = "-", Status = Models.Selection.RequestStatus.New }
        };
        foreach (var request in leaveRequests)
        {
            _context.LeaveRequests.Add(request);
        }
        _context.SaveChanges();

        var approvalRequests = new List<ApprovalRequest>
        {
            new ApprovalRequest {ApproverId = 1, LeaveRequestId = 1, Status = Models.Selection.RequestStatus.New, Comment = "-" },
            new ApprovalRequest {ApproverId = 1, LeaveRequestId = 2, Status = Models.Selection.RequestStatus.New, Comment = "-" },
        };
        foreach (var req in approvalRequests)
        {
            _context.ApprovalRequests.Add(req);
        }
        _context.SaveChanges();

        var projects = new List<Project>
        {
            new Project {ProjectType = Models.Selection.ProjectType.IT, StartDate = DateTime.Today.AddDays(-360), EndDate = DateTime.Today.AddDays(360), ProjectManagerId = 2, Comment = "-", Status = Models.Selection.Status.Active },
        };
        foreach (var project in projects)
        {
            _context.Projects.Add(project);
        }
        _context.SaveChanges();

    }
}