using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class AddOrUpdatePermissionBindingModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
        public DateTime? ExpiresOnUTC { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }
}