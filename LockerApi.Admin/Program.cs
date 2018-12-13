using LockerApi.Admin;
using LockerApi.Libraries;
using System;
using System.IO;

namespace LockerApi.Test
{
    class Program
    {
        public static void AddNewDevice()
        {
            var deviceCode = ShortGuid.NewGuid().Guid.ToString().
                Substring(0, 12);
            var deviceSecret = ShortGuid.NewGuid().Guid.ToString();
            var filename = "deviceEntries.txt";
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = Path.Combine(mydocpath, filename);

            int id = DbQuery.AddNewDevice(deviceCode, deviceSecret);
            string line = String.Format("id={0}, deviceCode={1}, deviceSecret={2}, Date={3}",
                id, deviceCode, deviceSecret, System.DateTime.Now);
            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                outputFile.WriteLine(line);
            }


        }
        static void Main(string[] args)
        {
            AddNewDevice();
        }

    }
}
