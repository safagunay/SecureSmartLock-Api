using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LockerApi.Models
{
    public class Device
    {
        public int Id { get; set; }
        //User defined name
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string CodeHash { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string SecretKeyHash { get; set; }
        [StringLength(128)]
        public string User_Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? RegisteredOnUTC { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public bool IsRegistered
        {
            get
            {
                return RegisteredOnUTC != null;
            }
        }
    }
}
