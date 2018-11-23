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
        public DateTime? ExpirationDateTime { get; set; }
        [StringLength(128)]
        public string User_Id { get; set; }
        [Required]
        public ConfirmationTokenType Type { get; set; }
        public bool IsExpired
        {
            get
            {
                DateTime expirationDateTime;
                if (ExpirationDateTime != null)
                {
                    expirationDateTime = ExpirationDateTime.Value;
                    if (DateTime.Now > expirationDateTime)
                        return true;
                }
                return false;
            }
        }
    }
}