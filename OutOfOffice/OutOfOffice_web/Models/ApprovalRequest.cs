namespace OutOfOffice_web.Models
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public virtual Employee? Approver {get; set;}
        public int LeaveRequestId { get; set; }
        public Selection.Status Status { get; set; }
        public string? Comment { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
    }
}
