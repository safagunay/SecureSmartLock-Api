using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class VerifyQRCodeBindingModel
    {
        [StringLength(500)]
        [Required]
        public string QRCode { get; set; }
    }
}