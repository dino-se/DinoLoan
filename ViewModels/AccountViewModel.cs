using System.ComponentModel.DataAnnotations;

namespace DinoLoan.ViewModels;

public partial class AccountViewModel
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
