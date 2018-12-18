using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class PermissionListBindingModel
    {
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
    }
}