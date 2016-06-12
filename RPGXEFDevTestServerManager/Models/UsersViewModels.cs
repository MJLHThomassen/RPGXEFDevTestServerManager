using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPGXEFDevTestServerManager.Models
{
    public class UsersViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class DeleteViewModel : IValidatableObject
    {
        [Required]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserId == 1)
            {
                yield return new ValidationResult("Root user can not be deleted.", new[] { nameof(UserId) });
            }
        }
    }
}
