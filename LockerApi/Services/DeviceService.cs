using LockerApi.Models;
using LockerApi.Services.Repositories;
using System.Collections.Generic;

namespace LockerApi.Services
{
    public class DeviceService
    {
        public Device getByCode(string code)
        {
            return DeviceRepository.getByCode(code);
        }

        public Device getByCodeHash(string code)
        {
            return DeviceRepository.getByCodeHash(
                HashService.HashDeviceCode(code));
        }

        public void updateDevice(Device device)
        {
            DeviceRepository.update(device);
        }

        public IEnumerable<Device> getRegisteredDevicesOf(string userId)
        {
            return DeviceRepository.getByUserId(userId);
        }

    }
}