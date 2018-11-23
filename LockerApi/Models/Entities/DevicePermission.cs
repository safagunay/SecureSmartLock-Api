using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class DevicePermission
    {
        public int Id { get; set; }
        [Required]
        public int Device_Id { get; set; }
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
        //User defined name
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(128)]
        [Required]
        public string User_Id { get; set; }
        [StringLength(128)]
        [Required]
        public string Givenby_User_Id { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DateRemoved { get; set; }
        public bool IsActive
        {
            get
            {
                return DateRemoved != null;
            }
        }
    }
}