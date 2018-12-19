using LockerApi.Models;
using System.Linq;

namespace LockerApi.Services.Repositories
{
    public static class QRCodeRepository
    {
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

        public static QRCode GetByHash(string qrCode)
        {
            if (qrCode == null)
                return null;
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var hash = HashService.HashQRCode(qrCode);
                return dbContext.QRCodes.Where(qr => qr.Hash == hash).SingleOrDefault();
            }
        }

        public static void insert(QRCode qrCode)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.QRCodes.Add(qrCode);
                dbContext.SaveChanges();
            }
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
                    dbContext.QRCodes.Add(qrCode);
                dbContext.SaveChanges();
            }
        }
    }
}