using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Guesty.Web.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "You are")]
        public string Role { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$@$!%*?&])[A-Za-z\\d$@#$!%*?&]{6,}", ErrorMessage = "Password must be alphanumeric with a special chatacter")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public bool IsTrue => true;

        [Required]
        [Display(Name = "I agree to the terms and conditions")]
        [Compare(nameof(IsTrue), ErrorMessage = "Please agree to Terms and Conditions")]    
        public bool IsAgreedTermCondition { get; set; }
    }
}
