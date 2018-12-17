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

        public void updateDeviceName(int deviceId, string name)
        {
            DeviceRepository.setName(deviceId, name);
        }

        public void updateDevice(Device device)
        {
            DeviceRepository.update(device);
        }
        public IEnumerable<Device> getRegisteredDevicesOf(string userId)
        {
            return DeviceRepository.getByUserId(userId);
        }

        public void AddOrUpdatePermission(DevicePermission permission)
        {
            DevicePermissionsRepository.InsertOrUpdate(permission);
        }

        public void AddPermissionRecord(DevicePermissionRecord record)
        {
            DevicePermissionsRepository.InsertPermissionRecord(record);
        }

        public DevicePermission DeletePermission(int deviceId, string userId)
        {
            return DevicePermissionsRepository.Delete(deviceId, userId);
        }

        public IEnumerable<DevicePermission> getDevicePermissionList(int deviceId)
        {
            return DevicePermissionsRepository.GetByDeviceId(deviceId);
        }
    }
}