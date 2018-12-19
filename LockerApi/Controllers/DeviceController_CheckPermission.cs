using LockerApi.Models;
using System.Web.Http;

namespace LockerApi.Controllers
{

    public partial class DeviceController
    {

        //POST api/Device/CheckPermission
        [Route("CheckPermission")]
        [AllowAnonymous]
        public IHttpActionResult CheckPermission(CheckPermissionBindingModel model)
        {
            //Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _qrCodeService.GetUserId(model.QRCode);
            if (userId == null)
            {
                ModelState.AddModelError("QRCode", "Invalid QR code or the QR code has expired.");
                return BadRequest(ModelState);
            }
            var device = _deviceService.getBySecretHash(model.DeviceSecret);
            if (device == null)
            {
                ModelState.AddModelError("DeviceSecret", "Device not found.");
                return BadRequest(ModelState);
            }

            //Check if user owns the device
            if (device.User_Id == userId)
                return Ok();

            //Check if user has permission on the device
            if (_deviceService.GetPermission(userId, device.Id) != null)
            {
                return Ok();
            }

            ModelState.AddModelError("PermissionNotFound", "User has no permission.");
            return BadRequest(ModelState);

        }
    }

}
