using System;

namespace LockerApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dtUtc = System.DateTime.UtcNow;
            var dtNow = System.DateTime.Now;
            Console.WriteLine(dtNow);
            Console.WriteLine(dtUtc);
            Console.ReadKey();
        }
    }
}
