using LockerApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace LockerApi.Services.Repositories
{
    public class DeviceRepository
    {
        public static Device getById(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Devices.Find(id);
            }
        }

        public static Device getByCode(string code)
        {
            if (code == null)
                return null;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Devices.
                    Where(dv => dv.Code == code).SingleOrDefault();
            }
        }

        public static Device getByCodeHash(string codeHash)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Devices.
                    Where(dv => dv.CodeHash == codeHash).SingleOrDefault();
            }
        }

        public static Device getBySecretHash(string secretHash)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Devices.
                    Where(dv => dv.SecretKeyHash == secretHash).SingleOrDefault();
            }
        }

        public static IEnumerable<Device> getByUserId(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Devices.Where(dv => dv.User_Id == userId)
                    .ToList();
            }
        }

        public static void insert(Device device)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.Devices.Add(device);
                dbContext.SaveChanges();
            }
        }

        public static void setName(int deviceId, string name)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Devices.Find(deviceId);
                entity.Name = name;
                dbContext.SaveChanges();
            }
        }

        public static void update(Device device)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Devices.Find(device.Id);
                entity.IsDeleted = device.IsDeleted;
                entity.Name = device.Name;
                entity.SecretKeyHash = device.SecretKeyHash;
                entity.User_Id = device.User_Id;
                entity.Code = device.Code;
                entity.CodeHash = device.CodeHash;
                entity.SecretKeyHash = device.SecretKeyHash;
                entity.CreatedOnUTC = device.CreatedOnUTC;
                entity.RegisteredOnUTC = device.RegisteredOnUTC;
                dbContext.SaveChanges();
            }
        }
    }
}