using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LockerApi.Controllers
{
    public partial class DeviceController
    {
        //POST api/Device/AddOrUpdatePermission
        [Route("AddOrUpdatePermission")]
        public IHttpActionResult AddOrUpdatePermission(AddOrUpdatePermissionBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (DateService.isExpiredUTC(model.ExpiresOnUTC))
            {
                ModelState.AddModelError("ExpiresOnUTC", "Permission is already expired.");
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device != null && device.User_Id == userId)
            {
                var userToPermit = UserManager.FindByEmail(model.Email);
                if (userToPermit != null && userToPermit.Id != userId && userToPermit.EmailConfirmed)
                {
                    _deviceService.AddOrUpdatePermission(
                        new DevicePermission()
                        {
                            CreatedOnUTC = DateService.getCurrentUTC(),
                            Givenby_User_Id = userId,
                            ExpiresOnUTC = model.ExpiresOnUTC,
                            Description = model.Description,
                            Device_Id = device.Id,
                            User_Id = userToPermit.Id

                        }
                    );
                    return Ok();
                }
                ModelState.AddModelError("Email", "Invalid user email.");
                return BadRequest(ModelState);
            }
            ModelState.AddModelError("DeviceCode", "Invalid device code.");
            return BadRequest(ModelState);
        }

        //Get api/Device/PermissionList
        [Route("PermissionList")]
        [HttpGet]
        public IEnumerable<DevicePermissionDTO> GetPermissionList(GetPermissionListModel model)
        {
            var devicePermissionDTOs = new List<DevicePermissionDTO>();
            if (!ModelState.IsValid)
                return devicePermissionDTOs;
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device != null && device.User_Id == userId)
                foreach (var permission in _deviceService.getDevicePermissionList(device.Id))
                {
                    var email = UserManager.FindById(permission.User_Id).Email;
                    devicePermissionDTOs.Add
                        (
                            new DevicePermissionDTO()
                            {
                                Email = email,
                                Description = permission.Description,
                                CreatedOnUTC = permission.CreatedOnUTC,
                                ExpiresOnUTC = permission.ExpiresOnUTC.Value
                            }

                        );
                }
            return devicePermissionDTOs;
        }

        //POST api/Device/DeletePermission
        [Route("AddPermission")]
        public IHttpActionResult DeletePermission(DeletePermissionBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device != null && device.User_Id == userId)
            {
                var userToPermit = UserManager.FindByEmail(model.Email);
                if (userToPermit != null && userToPermit.Id != userId && userToPermit.EmailConfirmed)
                {

                    var permission = _deviceService.DeletePermission(device.Id, userToPermit.Id);
                    if (permission != null)
                    {
                        var task = new Task(() => insertPermissionRecord(permission));
                        task.Start();
                        return Ok();
                    }
                }

                ModelState.AddModelError("Email", "Invalid user email.");
                return BadRequest(ModelState);
            }
            ModelState.AddModelError("DeviceCode", "Invalid device code.");
            return BadRequest(ModelState);
        }




        private void insertPermissionRecord(DevicePermission permission)
        {
            _deviceService.AddPermissionRecord(
                new DevicePermissionRecord()
                {
                    CreatedOnUTC = permission.CreatedOnUTC,
                    Description = permission.Description,
                    Device_Id = permission.Device_Id,
                    ExpiresOnUTC = permission.ExpiresOnUTC,
                    RemovedOnUTC = DateService.getCurrentUTC(),
                    Givenby_User_Id = permission.Givenby_User_Id,
                    User_Id = permission.User_Id
                }
                );
        }
    }
}