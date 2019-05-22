using System.Threading.Tasks;

namespace LockerApi.Services
{
    public interface IDeviceActivityLogService
    {
        //void addDevice(uint devId);
        Task<string> addLogToDevice(uint devId, string log);
        Task<string> getLogs(uint devId);
    }
}