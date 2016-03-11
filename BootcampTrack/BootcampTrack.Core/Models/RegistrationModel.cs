using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Models
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public class SchoolAdministrator : RegistrationModel
        {
            [Required]
            public string SchoolName { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string State { get; set; }
        }

        public class Instructor : RegistrationModel
        {
            public string Token { get; set; }
        }

        public class Student : RegistrationModel
        {
            public string Token { get; set; }
        }
    }
}