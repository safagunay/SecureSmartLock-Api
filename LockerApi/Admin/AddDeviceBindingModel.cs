using System.ComponentModel.DataAnnotations;

namespace LockerApi.Admin
{
    public class AddDeviceBindingModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }
    }
}