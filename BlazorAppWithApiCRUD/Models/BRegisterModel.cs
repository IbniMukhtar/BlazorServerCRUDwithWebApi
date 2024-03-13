using System.ComponentModel.DataAnnotations;

namespace BlazorAppWithApiCRUD.Models
{
    public class BRegisterModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
            ErrorMessage = "Password should have minimum 8 characters, at least 1 uppercase letter, 1 lowercase letter and 1 number.")]
        public string? Password { get; set; }
    }
}
