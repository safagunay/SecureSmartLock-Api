using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LockerApi.Models;

namespace LockerApi.Services
{
    public class DeviceActivityLogService
    {
        private static HttpClient _client = new HttpClient();
        private const string baseURL = "http://23.101.70.73:5000";

        public async Task<bool> addLog(int devId, DeviceActivityLogDTO log)
        {
            var url = baseURL + "/addLog";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            string content = "{ \"id\" : " + devId + ", \"log\" : " + "\"" + log.ToString() +  "\"}";
            message.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var responseMessage = await _client.SendAsync(message);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<DeviceActivityLogDTO>> getLogs(int devId)
        {
            var url = baseURL + "/getLogs";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
            string content = "{\"id\" :" + devId + "}";
            message.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var responseMessage = await _client.SendAsync(message);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var logs = responseContent.Split(';');
            List<DeviceActivityLogDTO> list = new List<DeviceActivityLogDTO>();
            foreach(string log in logs)
            {
                if (String.IsNullOrEmpty(log))
                    continue;
                var vals = log.Split(',');
                DeviceActivityLogDTO data = new DeviceActivityLogDTO()
                {
                    Email = vals[0],
                    TimeUTC = DateTime.Parse(vals[1]),
                    IsSuccessful = Boolean.Parse(vals[2])
                };
                list.Add(data);
            }
            return list;
        }
    }
}