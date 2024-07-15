using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Display(Name ="Project Type")]
        public Selection.ProjectType ProjectType { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Project Manager")]
        public int ProjectManagerId { get; set; }
        public virtual Employee? ProjectManager { get; set; }
        public string? Comment { get; set; }
        public Selection.Status Status { get; set; }
    }
}

