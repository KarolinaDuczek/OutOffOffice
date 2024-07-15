using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name ="Absence Reason")]
        public Selection.AbsenceReason AbsenceReason { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public Selection.RequestStatus Status { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ApprovalRequest? ApprovalRequest { get; set; }
    }
}
