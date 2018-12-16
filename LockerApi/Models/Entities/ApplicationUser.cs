using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public partial class ApplicationUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public DateTime RegisteredOnUTC { get; set; }
        public DateTime LastLoginUTC { get; set; }
    }
}