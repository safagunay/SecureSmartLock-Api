using LockerApi.Libraries;
using System;

namespace LockerApi.Test
{
    public static class GuidTest
    {
        public static void Test()
        {
            var deviceCodeHash = ShortGuid.NewGuid();
            var deviceSecretHash = ShortGuid.NewGuid();
            var dtUtc = System.DateTime.UtcNow;
            var dtNow = System.DateTime.Now;
            Console.WriteLine(dtNow);
            Console.WriteLine(dtUtc);
            Console.WriteLine(deviceCodeHash);
            Console.WriteLine(deviceSecretHash);
            Console.ReadKey();
        }
    }
}
