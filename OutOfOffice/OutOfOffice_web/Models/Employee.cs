using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace OutOfOffice_web.Models;

public class Employee
{
    public int Id { get; init; }
    public Selection.FullName FullName { get; set; }
    public Selection.Subdivision Subdivision { get; set; }
    public Selection.Position Position { get; set; }
    public Selection.Status Status { get; set; }
    public int PeoplePartner { get; set; }
    public double OutOfOfficeBalance { get; set; }
    public virtual ApprovalRequest? ApprovalRequest { get; set; }
    public virtual Project? Project { get; set; }
    public virtual ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
