using LockerApi.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LockerApi.Services.Repositories
{
    public static class QRCodeRepository
    {
        private static int _insertCount = 0;
        private static void cleanUpTable()
        {
            if (_insertCount >= SettingsService.QRCodeTableCleanUpPeriod)
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    IDbSet<QRCode> table = dbContext.QRCodes;
                    foreach (var entry in table.
                        Where(qr => DateService.isExpiredUTC(qr.ExpiresOnUTC)))
                    {
                        table.Remove(entry);
                    }
                    dbContext.SaveChanges();
                    _insertCount = 0;
                }
        }

        public static QRCode getById(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.QRCodes.Find(id);
            }
        }

        public static QRCode getByUserId(string userId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.QRCodes.
                   Where(qr => qr.User_Id == userId).FirstOrDefault();
            }
        }

        public static void insert(QRCode qrCode)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.QRCodes.Add(qrCode);
                dbContext.SaveChanges();
            }
            _insertCount++;
            var task = new Task(cleanUpTable);
            task.Start();
        }

        public static void insertOrUpdate(QRCode qrCode)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.QRCodes.
                    SingleOrDefault(qr => qr.User_Id == qrCode.User_Id);
                if (entity != null)
                {
                    entity.Hash = qrCode.Hash;
                    entity.CreatedOnUTC = qrCode.CreatedOnUTC;
                    entity.ExpiresOnUTC = qrCode.ExpiresOnUTC;
                }
                else
                {
                    dbContext.QRCodes.Add(qrCode);
                    _insertCount++;
                }
                dbContext.SaveChanges();
            }
            var task = new Task(cleanUpTable);
            task.Start();
        }
    }
}