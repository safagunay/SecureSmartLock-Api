using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    // Models used as parameters to AccountController actions.

    //public class AddExternalLoginBindingModel
    //{
    //    [Required]
    //    [Display(Name = "External access token")]
    //    public string ExternalAccessToken { get; set; }
    //}

    public class PasswordResetBindingModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class VerifyPasswordResetBindingModel
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(1000)]
        public string ConfirmationToken { get; set; }
    }

    public class ConfirmPasswordResetBindingModel
    {
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(1000)]
        public string ConfirmationToken { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ConfirmEmailBindingModel
    {
        [Required]
        [StringLength(1000)]
        [Display(Name = "Email confirmation token")]
        public string ConfirmEmailToken { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(50)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    //public class RegisterExternalBindingModel
    //{
    //    [Required]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }
    //}

    //public class RemoveLoginBindingModel
    //{
    //    [Required]
    //    [Display(Name = "Login provider")]
    //    public string LoginProvider { get; set; }

    //    [Required]
    //    [Display(Name = "Provider key")]
    //    public string ProviderKey { get; set; }
    //}

    //public class SetPasswordBindingModel
    //{
    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "New password")]
    //    public string NewPassword { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm new password")]
    //    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}
}
