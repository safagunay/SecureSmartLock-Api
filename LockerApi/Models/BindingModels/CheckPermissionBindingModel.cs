using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class CheckPermissionBindingModel
    {
        [Required]
        public string DeviceSecret { get; set; }
        [Required]
        public string QRCode { get; set; }
    }
}