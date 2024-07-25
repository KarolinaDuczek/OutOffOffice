using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models
{

    [Display(Name = "Approval Request")]
    public class ApprovalRequest
    {
        public int Id { get; set; }
        [Display(Name ="Approver Id")]
        public int ApproverId { get; set; }
        [Display(Name = "Leave Request Id")]
        public int LeaveRequestId { get; set; }
        public Selection.RequestStatus Status { get; set; }
        public string? Comment { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
        public ICollection<Employee> Approvers { get; set; } = new List<Employee>();
    }
}
