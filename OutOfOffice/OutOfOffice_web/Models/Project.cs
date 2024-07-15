namespace OutOfOffice_web.Models
{
    public class Project
    {
        public int Id { get; set; }
        public Selection.ProjectType ProjectType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public virtual Employee? ProjectManager { get; set; }
        public string? Comment { get; set; }
        public Selection.Status Status { get; set; }
    }
}

