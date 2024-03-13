using System.ComponentModel.DataAnnotations;

namespace Demo.ApiClient.Models.ViewModels
{
    public class UserNameValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string username && username.ToLower().Contains("admin"))
            {
                string? memberName = validationContext?.MemberName;
                if (memberName != null)
                {
                    return new ValidationResult("The UserName cannot contain the word admin",
                        new[] { memberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}
