using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class DevicePermissionRecord
    {
        public int Id { get; set; }
        [Required]
        public int Device_Id { get; set; }
        //User defined description
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(128)]
        [Required]
        public string User_Id { get; set; }
        [StringLength(128)]
        [Required]
        public string Givenby_User_Id { get; set; }
        public DateTime? ExpiresOnUTC { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime RemovedOnUTC { get; set; }

    }
}