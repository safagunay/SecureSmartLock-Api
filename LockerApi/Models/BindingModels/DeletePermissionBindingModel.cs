using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class DeletePermissionBindingModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
    }
}