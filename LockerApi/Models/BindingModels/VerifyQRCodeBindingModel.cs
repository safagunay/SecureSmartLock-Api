using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class VerifyQRCodeBindingModel
    {
        [StringLength(500)]
        public string QRCode { get; set; }
    }
}