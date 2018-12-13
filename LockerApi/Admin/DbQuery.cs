using LockerApi.Models;
using LockerApi.Services;

namespace LockerApi.Admin
{
    public static class DbQuery
    {
        public static int AddNewDevice(string deviceCode, string deviceSecret)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var deviceEntity = new Device
                {
                    Code = null,
                    CodeHash = HashService.HashDeviceCode(deviceCode),
                    DateCreated = System.DateTime.Now,
                    DateRegistered = null,
                    IsDeleted = false,
                    Name = null,
                    SecretKeyHash = HashService.HashDeviceSecret(deviceSecret),
                    User_Id = null
                };
                dbContext.Devices.Add(deviceEntity);
                dbContext.SaveChanges();
                return deviceEntity.Id;
            }

        }
    }
}