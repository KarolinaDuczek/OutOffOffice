using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models.Selection
{
    public enum Position
    {
        Specialist,
        [Display(Name ="Project Manager")]
        ProjectManager,
        [Display(Name = "HR Manager")]
        HRmanager,
        CEO,
        [Display(Name = "Senior Specialist")]
        SeniorSpecialist,
        Intern
    }
}
