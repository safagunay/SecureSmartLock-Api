using LockerApi.Models;
using LockerApi.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace LockerApi.Controllers
{

    public partial class DeviceController
    {

        private readonly DeviceActivityLogService _deviceActivityLogService = new DeviceActivityLogService();

        //POST api/Device/CheckPermission
        [Route("CheckPermission")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> CheckPermission(CheckPermissionBindingModel model)
        {
            //Validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var device = _deviceService.getBySecretHash(model.DeviceSecret);
            if (device == null)
            {
                ModelState.AddModelError("DeviceSecret", "Device not found.");
                return BadRequest(ModelState);
            }
            DeviceActivityLogDTO log = new DeviceActivityLogDTO();
            log.TimeUTC = DateService.getCurrentUTC();
            var userId = _qrCodeService.GetUserId(model.QRCode);
            if (userId == null)
            {
                log.Email = "Unregistered";
                log.IsSuccessful = false;
                await _deviceActivityLogService.addLog(device.Id, log);
                ModelState.AddModelError("QRCode", "Invalid QR code or the QR code has expired.");
                return BadRequest(ModelState);
            }
            log.Email = await UserManager.GetEmailAsync(userId);
            //Check if user owns the device
            if (device.User_Id == userId)
            {
                log.IsSuccessful = true;
                await _deviceActivityLogService.addLog(device.Id, log);
                return Ok();
            }

            //Check if user has permission on the device
            if (_deviceService.GetPermission(userId, device.Id) != null)
            {
                log.IsSuccessful = true;
                await _deviceActivityLogService.addLog(device.Id, log);
                return Ok();
            }

            log.IsSuccessful = false;
            await _deviceActivityLogService.addLog(device.Id, log);
            ModelState.AddModelError("PermissionNotFound", "User has no permission.");
            return BadRequest(ModelState);

        }
    }

}
