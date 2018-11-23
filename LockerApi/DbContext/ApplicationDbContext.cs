using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LockerApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LockerDbConnectionRemote", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual IDbSet<Device> Devices { get; set; }
        public virtual IDbSet<DevicePermission> DevicePermissions { get; set; }
        public virtual IDbSet<ConfirmationToken> ConfirmationTokens { get; set; }
        public virtual IDbSet<QRCode> QRCodes { get; set; }
    }
}