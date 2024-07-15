using System.ComponentModel.DataAnnotations;

namespace OutOfOffice_web.Models.Selection
{
    public enum AbsenceReason
    {
        [Display(Name= "Sick Leave")]
        SickLeave,
        [Display(Name = "Vacation Leave")]
        VacationLeave,
        [Display(Name = "Maternity Leave")]
        MaternityLeave,
        [Display(Name = "Special Leave")]
        SpecialLeave,
        [Display(Name = "Bank Holiday")]
        BankHoliday
    }
}
