using LockerApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerApi.Test
{
    public static class EthClientTest
    {

        public static void Test()
        {
            var client = new EthClient();
            var result = client.deployContract();
            result.Wait();
            var success = result.Result;
            Task<string> logTask;
            string log = null;
            if (success)
            { 
                Task<string> addTask = client.addLogToDevice(0, "safa");
                addTask.Wait();
                logTask = client.getLogs(0);
                logTask.Wait();
                log = logTask.Result;
            }
            Console.WriteLine("Deployed:" + success);
            Console.WriteLine("Log:" + log);
            Console.ReadKey();
        }

    }
}
