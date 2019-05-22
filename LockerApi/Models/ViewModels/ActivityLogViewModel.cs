using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LockerApi.Models
{
    public class ActivityLogViewModel
    {
        public bool DevFound { get; set; }
        public List<DeviceActivityLogDTO> LogList { get; set; }
        public int DeviceId { get; set; }
        public string DeviceOwnerEmail { get; set; }
        public string DeviceName { get; set; }
    }
}