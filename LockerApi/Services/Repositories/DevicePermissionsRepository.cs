using LockerApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LockerApi.Services.Repositories
{
    public static class DevicePermissionsRepository
    {
        private static int _insertCount = 0;

        private static void cleanUpTable()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                IDbSet<DevicePermission> table = dbContext.DevicePermissions;
                //IDbSet<DevicePermissionRecord> tableRecord = dbContext.DevicePermissionRecords;
                foreach (var entry in table)
                {
                    if (DateService.isExpiredUTC(entry.ExpiresOnUTC))
                        table.Remove(entry);
                    //tableRecord.Add(new DevicePermissionRecord()
                    //{
                    //    CreatedOnUTC = dp.CreatedOnUTC,
                    //    Description = dp.Description,
                    //    Device_Id = dp.Device_Id,
                    //    ExpiresOnUTC = dp.ExpiresOnUTC,
                    //    RemovedOnUTC = DateService.getCurrentUTC(),
                    //    Givenby_User_Id = dp.Givenby_User_Id,
                    //    User_Id = dp.User_Id
                    //});
                }
                dbContext.SaveChanges();
                _insertCount = 0;
            }
        }

        //private static void insertPermissionRecord(DevicePermission permission)
        //{
        //    _deviceService.AddPermissionRecord(
        //        new DevicePermissionRecord()
        //        {
        //            CreatedOnUTC = permission.CreatedOnUTC,
        //            Description = permission.Description,
        //            Device_Id = permission.Device_Id,
        //            ExpiresOnUTC = permission.ExpiresOnUTC,
        //            RemovedOnUTC = DateService.getCurrentUTC(),
        //            Givenby_User_Id = permission.Givenby_User_Id,
        //            User_Id = permission.User_Id
        //        }
        //        );
        //}

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
                    //if(DateService.isExpiredUTC(entity.ExpiresOnUTC))
                    entity.Description = devicePermission.Description;
                    entity.ExpiresOnUTC = devicePermission.ExpiresOnUTC;
                }
                else
                {
                    dbContext.DevicePermissions.Add(devicePermission);
                    _insertCount++;
                }
                dbContext.SaveChanges();
                if (_insertCount >= SettingsService.DevicePermissionsTableCleanUpPeriod)
                {
                    var task = new Task(cleanUpTable);
                    task.Start();
                }

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