using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models
{

    [Display(Name = "Approval Request")]
    public class ApprovalRequest
    {
        public int Id { get; set; }
        [Display(Name ="Approver")]
        public int ApproverId { get; set; }
        public virtual Employee? Approver {get; set;}
        [Display(Name = "Leave Request Id")]
        public int LeaveRequestId { get; set; }
        public Selection.RequestStatus Status { get; set; }
        public string? Comment { get; set; }
        public virtual LeaveRequest? LeaveRequest { get; set; }
    }
}
