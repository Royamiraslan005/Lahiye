using System.ComponentModel.DataAnnotations;

namespace NailSalon.Core.ViewModels;

public class RegisterVm
{
    [Required]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}
