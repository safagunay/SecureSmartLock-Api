using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LockerApi.Models
{
    public class DeviceActivityLogDTO
    {
        public string Email { get; set; }
        public DateTime TimeUTC { get; set; }
        public bool IsSuccessful { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2};", Email, TimeUTC, IsSuccessful);
        }
    }
}