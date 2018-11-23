using LockerApi.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LockerApi.Services
{
    public static class ConfirmationTokenRepository
    {
        private static int _insertCount = 0;
        private static void cleanUpTable()
        {
            if (_insertCount >= SettingsService.ConfirmationTokenTableCleanUpPeriod)
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var currentDateTime = System.DateTime.Now;
                    IDbSet<ConfirmationToken> table = dbContext.ConfirmationTokens;
                    foreach (var entry in table.
                        Where(ct => ct.ExpirationDateTime < currentDateTime))
                    {
                        table.Remove(entry);
                    }
                    dbContext.SaveChanges();
                    _insertCount = 0;
                }
        }

        public static bool deleteByUserId(string userId, ConfirmationTokenType type)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.ConfirmationTokens.
                    FirstOrDefault(ct => ct.User_Id == userId && ct.Type == type);
                if (entity != null)
                {
                    dbContext.ConfirmationTokens.Remove(entity);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static ConfirmationToken getById(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.ConfirmationTokens.Find(id);
            }
        }

        public static ConfirmationToken getByUserId(string userId, ConfirmationTokenType type)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.ConfirmationTokens.
                   Where(ct => ct.Type == type && ct.User_Id == userId).FirstOrDefault();
            }
        }

        public static void insert(ConfirmationToken confirmationToken)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.ConfirmationTokens.Add(confirmationToken);
                dbContext.SaveChanges();
            }
            _insertCount++;
            var task = new Task(cleanUpTable);
            task.Start();
        }

        public static void update(ConfirmationToken confirmationToken)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.ConfirmationTokens.
                    SingleOrDefault(ct => ct.Id == confirmationToken.Id);
                if (entity != null)
                {
                    entity.Token = confirmationToken.Token;
                    entity.ExpirationDateTime = confirmationToken.ExpirationDateTime;
                    dbContext.SaveChanges();
                }
            }
            var task = new Task(cleanUpTable);
            task.Start();
        }

        public static void delete(int id)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.ConfirmationTokens.Find(id);
                if (entity != null)
                {
                    dbContext.ConfirmationTokens.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public static void delete(string userId, ConfirmationTokenType type)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.ConfirmationTokens.
                    Where(ct => ct.Type == type && ct.User_Id == userId).FirstOrDefault();
                if (entity != null)
                {
                    dbContext.ConfirmationTokens.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}