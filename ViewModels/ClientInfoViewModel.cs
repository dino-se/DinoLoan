using System.ComponentModel.DataAnnotations;

namespace dotnet_web_mvc.ViewModels
{
    public class ClientInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public int? UserType { get; set; }
        public string UserTypeName { get; set; } = "";
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string MiddleName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string NameOfFather { get; set; } = null!;
        [Required]
        public string NameOfMother { get; set; } = null!;
        [Required]
        public string CivilStatus { get; set; } = null!;
        [Required]
        public string Religion { get; set; } = null!;
        [Required]
        public string Occupation { get; set; } = null!;
    }
}
