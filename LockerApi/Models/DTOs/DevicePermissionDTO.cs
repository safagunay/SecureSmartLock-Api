using System;

namespace LockerApi.Models
{
    public class DevicePermissionDTO
    {
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
    }
}