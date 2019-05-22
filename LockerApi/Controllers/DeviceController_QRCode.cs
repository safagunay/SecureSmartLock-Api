using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace LockerApi.Controllers
{

    public partial class DeviceController
    {

        //Get api/Device/QrCode
        [Route("QRCode")]
        public QRCodeDTO GetQRCode()
        {
            var userId = User.Identity.GetUserId();
            var qrCode = _qrCodeService.GenerateQRCode();
            var qrEntity = _qrCodeService.CreateQrCodeFor(userId, qrCode);
            return new QRCodeDTO()
            {
                QRCode = qrCode,
                DurationInSeconds = SettingsService.QRCodeDuration,
                CreatedOnUTC = qrEntity.CreatedOnUTC,
                ExpiresOnUTC = qrEntity.ExpiresOnUTC.Value
            };

        }

        //POST api/Device/VerifyQRCode
        [Route("VerifyQRCode")]
        public IHttpActionResult VerifyQRCode(VerifyQRCodeBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.GetUserId();
            if (_qrCodeService.VerifyQRCodeFor(userId, model.QRCode))
                return Ok();
            ModelState.AddModelError("QRCode", "Invalid QR code or the QR code has expired.");
            return BadRequest(ModelState);
        }
    }

}
