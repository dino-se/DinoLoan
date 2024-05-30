using System.ComponentModel.DataAnnotations;

namespace DinoLoan.ViewModels
{
    public partial class UserTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}