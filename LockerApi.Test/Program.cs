using System;
using System.Threading.Tasks;

namespace LockerApi.Test
{
    class Program
    {
        public static void testGuid()
        {
            //Console.WriteLine(ShortGuid.NewGuid());
            Console.WriteLine(Guid.NewGuid());
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => testGuid());
            }
            Console.ReadKey();
        }

    }
}
