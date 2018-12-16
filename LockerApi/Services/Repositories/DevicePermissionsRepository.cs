using LockerApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace LockerApi.Services.Repositories
{
    public static class DevicePermissionsRepository
    {

        public static DevicePermission getById(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Find(id);
            }
        }

        public static DevicePermission getByDeviceAndUserId(int deviceId, string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Where(
                    dp => dp.Device_Id == deviceId &&
                    dp.User_Id == userId
                    ).SingleOrDefault();
            }
        }

        public static IEnumerable<DevicePermission> getByDeviceId(int deviceId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Where(dp => dp.Device_Id == deviceId);
            }
        }

        public static IEnumerable<DevicePermission> getByUserId(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Where(dp => dp.User_Id == userId);
            }
        }

        public static void insert(DevicePermission devicePermission)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.DevicePermissions.Add(devicePermission);
                dbContext.SaveChanges();
            }
        }

        public static void setDescription(int deviceId, string userId, string description)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.DevicePermissions.Where(
                    dp => dp.Device_Id == deviceId && dp.User_Id == userId)
                    .SingleOrDefault();
                entity.Description = description;
                dbContext.SaveChanges();
            }
        }

        public static void Delete(int deviceId, string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.DevicePermissions.Where(
                    dp => dp.Device_Id == deviceId && dp.User_Id == userId).
                    SingleOrDefault();
                dbContext.DevicePermissions.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public static void update(DevicePermission devicePermission)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.DevicePermissions.Find(devicePermission.Id);
                entity.CreatedOnUTC = devicePermission.CreatedOnUTC;
                entity.Description = devicePermission.Description;
                entity.ExpiresOnUTC = devicePermission.ExpiresOnUTC;
                entity.Givenby_User_Id = devicePermission.Givenby_User_Id;
                entity.User_Id = devicePermission.User_Id;
                dbContext.SaveChanges();
            }
        }
    }
}