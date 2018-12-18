using System.ComponentModel.DataAnnotations;

namespace LockerApi.Admin
{
    public class DeviceListBindingModel
    {
        [Required]
        public string Password { get; set; }
    }
}