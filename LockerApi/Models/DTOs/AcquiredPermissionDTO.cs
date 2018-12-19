using System;

namespace LockerApi.Models
{
    public class AcquiredDevicePermissionDTO
    {
        public string Name { get; set; }
        public string DeviceCode { get; set; }
        public string GiverEmail { get; set; }
        public DateTime? ExpiresOnUTC { get; set; }
        public DateTime CreatedOnUTC { get; set; }
    }
}