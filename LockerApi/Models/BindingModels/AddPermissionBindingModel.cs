using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class AddPermissionBindingModel
    {
        [Required]
        [StringLength(256)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        public string DeviceCode { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
    }
}