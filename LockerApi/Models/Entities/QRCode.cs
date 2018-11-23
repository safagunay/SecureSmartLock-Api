using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LockerApi.Models
{
    public class QRCode
    {
        public int Id { get; set; }
        [StringLength(128)]
        [Required]
        public string User_Id { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string Hash { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
    }
}