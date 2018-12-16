using System;
using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class ConfirmationToken
    {
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Token { get; set; }
        public DateTime? ExpiresOnUTC { get; set; }
        [StringLength(128)]
        public string User_Id { get; set; }
        [Required]
        public ConfirmationTokenType Type { get; set; }
    }
}