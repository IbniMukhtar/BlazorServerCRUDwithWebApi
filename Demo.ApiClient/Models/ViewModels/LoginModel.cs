using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.ApiClient.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [UserNameValidation]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
