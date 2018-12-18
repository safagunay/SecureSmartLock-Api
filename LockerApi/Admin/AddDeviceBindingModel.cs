using System.ComponentModel.DataAnnotations;

namespace LockerApi.Admin
{
    public class AddDeviceBindingModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string DeviceCode { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string DeviceSecret { get; set; }
    }
}