using System.ComponentModel.DataAnnotations;

namespace Order_Management.DTOS
{
    public class RegisterDto
    {

        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhonNumber { get; set; }
        [Required]
        //[RegularExpression("(?=.*{6,10}$)(?=^.[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*",
        //    ErrorMessage ="Password must contanis 1 Uppercase , 1 Lowercase , 1 Digit , 1 Spaecial Character")]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
