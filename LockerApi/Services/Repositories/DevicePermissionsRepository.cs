using LockerApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace LockerApi.Services.Repositories
{
    public static class DevicePermissionsRepository
    {

        public static DevicePermission GetById(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Find(id);
            }
        }

        public static DevicePermission GetByDeviceAndUserId(int deviceId, string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Where(
                    dp => dp.Device_Id == deviceId &&
                    dp.User_Id == userId
                    ).SingleOrDefault();
            }
        }

        public static IEnumerable<DevicePermission> GetByDeviceId(int deviceId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.
                    Where(dp => dp.Device_Id == deviceId).
                    ToList();
            }
        }

        public static void InsertPermissionRecord(DevicePermissionRecord record)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.DevicePermissionRecords.Add(record);
                dbContext.SaveChanges();
            }
        }

        public static IEnumerable<DevicePermission> GetByUserId(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.DevicePermissions.Where(dp => dp.User_Id == userId)
                    .ToList();
            }
        }

        public static void InsertOrUpdate(DevicePermission devicePermission)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.DevicePermissions.Where(dp =>
                   dp.User_Id == devicePermission.User_Id &&
                   dp.Device_Id == devicePermission.Device_Id
                    ).SingleOrDefault();
                if (entity != null)
                {
                    entity.Description = devicePermission.Description;
                    entity.ExpiresOnUTC = devicePermission.ExpiresOnUTC;
                }
                else
                    dbContext.DevicePermissions.Add(devicePermission);
                dbContext.SaveChanges();
            }
        }

        public static DevicePermission Delete(int deviceId, string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.DevicePermissions.Where(
                    dp => dp.Device_Id == deviceId && dp.User_Id == userId).
                    SingleOrDefault();
                if (entity == null)
                    return null;
                dbContext.DevicePermissions.Remove(entity);
                dbContext.SaveChanges();
                return entity;
            }
        }

        public static void Update(DevicePermission devicePermission)
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