namespace OutOfOffice_web.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Selection.AbsenceReason AbsenceReason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }
        public Selection.Status Status { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ApprovalRequest? ApprovalRequest { get; set; }
    }
}
