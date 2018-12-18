using System.ComponentModel.DataAnnotations;

namespace LockerApi.Admin
{
    public class GetDeviceBindingModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public int? DeviceId { get; set; }

    }
}