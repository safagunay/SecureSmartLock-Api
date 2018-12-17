using System.ComponentModel.DataAnnotations;

namespace LockerApi.Models
{
    public class GetPermissionListModel
    {
        [StringLength(50)]
        [Required]
        public string DeviceCode { get; set; }
    }
}