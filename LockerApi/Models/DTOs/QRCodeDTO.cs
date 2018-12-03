using System;

namespace LockerApi.Models
{
    public class QRCodeDTO
    {
        public string QRCode { get; set; }
        public int DurationInSeconds { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}