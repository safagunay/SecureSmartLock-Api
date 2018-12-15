using System;
using System.Text;

namespace LockerApi.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            char c1 = '\x0048';
            char c2 = '\x0065';
            char[] ca = new char[] { c1, c2 };
            string s = new string(ca);
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(s);
            string s1 = Encoding.Default.GetString(bytes);

            Console.WriteLine(s1);
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
