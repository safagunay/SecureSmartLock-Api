using System;

namespace LockerApi.Models
{
    public class DeviceDTO
    {
        public string Name { get; set; }
        public string DeviceCode { get; set; }
        public DateTime RegisteredOnUTC { get; set; }
    }
}