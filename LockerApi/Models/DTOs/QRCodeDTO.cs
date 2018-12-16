using System;

namespace LockerApi.Models
{
    public class QRCodeDTO
    {
        public string QRCode { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}