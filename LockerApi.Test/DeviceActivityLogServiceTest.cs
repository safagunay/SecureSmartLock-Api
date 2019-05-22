using LockerApi.Models;
using LockerApi.Services;
using System;

namespace LockerApi.Test
{
    public static class DeviceActivityLogServiceTest
    {
        public static void Test()
        {
            DeviceActivityLogService service = new DeviceActivityLogService();
            DeviceActivityLogDTO data = new DeviceActivityLogDTO()
            {
                Email = "samyeli55@gmail.com",
                TimeUTC = DateTime.UtcNow,
                IsSuccessful = false
            };

            var task1 = service.addLog(1004, data);
            task1.Wait();
            var task = service.getLogs(1004);
            task.Wait();
            Console.WriteLine(task.Result.ToString());
            Console.ReadKey();
        }

    }
}
