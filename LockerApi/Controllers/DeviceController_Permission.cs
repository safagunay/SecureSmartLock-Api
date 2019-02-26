using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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

        //POST api/Device/PermissionList
        [Route("PermissionList")]
        public IHttpActionResult PermissionList(PermissionListBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device == null || device.User_Id != userId)
            {
                ModelState.AddModelError("DeviceCode", "Invalid device code.");
                return BadRequest(ModelState);
            }
            var devicePermissionDTOs = new List<DevicePermissionDTO>();
            foreach (var permission in _deviceService.getDevicePermissionList(device.Id))
            {
                if (DateService.isExpiredUTC(permission.ExpiresOnUTC))
                    continue;
                var email = UserManager.FindById(permission.User_Id).Email;
                devicePermissionDTOs.Add
                    (
                        new DevicePermissionDTO()
                        {
                            Email = email,
                            Description = permission.Description,
                            CreatedOnUTC = permission.CreatedOnUTC,
                            ExpiresOnUTC = permission.ExpiresOnUTC
                        }

                    );
            }
            return Ok(devicePermissionDTOs);
        }

        //POST api/Device/DeletePermission
        [HttpPost]
        [Route("DeletePermission")]
        public IHttpActionResult DeletePermission(DeletePermissionBindingModel model)
        {
            //Model check
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userId = User.Identity.GetUserId();
            var device = _deviceService.getByCode(model.DeviceCode);
            if (device == null || device.User_Id != userId)
            {
                ModelState.AddModelError("DeviceCode", "Invalid device code.");
                return BadRequest(ModelState);
            }
            var userToPermit = UserManager.FindByEmail(model.Email);
            if (userToPermit == null || userToPermit.Id == userId || !userToPermit.EmailConfirmed)
            {
                ModelState.AddModelError("Email", "Invalid user email.");
                return BadRequest(ModelState);
            }

            //Delete permission
            var permission = _deviceService.DeletePermission(device.Id, userToPermit.Id);
            if (permission == null)
            {
                ModelState.AddModelError("PermissionNotFound", "No such permission found.");
                return BadRequest(ModelState);
            }
            return Ok();
        }

        //GET api/Device/AcquiredPermissionList
        [Route("AcquiredPermissionList")]
        public IHttpActionResult GetAcquiredPermissionList()
        {
            var userId = User.Identity.GetUserId();
            var list = new List<AcquiredDevicePermissionDTO>();
            foreach (var permission in _deviceService.GetAcquiredDevicePermissionList(userId))
            {
                if (DateService.isExpiredUTC(permission.ExpiresOnUTC))
                    continue;
                var email = UserManager.FindById(permission.Givenby_User_Id).Email;
                var device = _deviceService.GetById(permission.Device_Id);
                list.Add
                    (
                        new AcquiredDevicePermissionDTO()
                        {
                            Name = device.Name,
                            DeviceCode = device.Code,
                            GiverEmail = email,
                            CreatedOnUTC = permission.CreatedOnUTC,
                            ExpiresOnUTC = permission.ExpiresOnUTC
                        }

                    );
            }
            return Ok(list);
        }

    }
}