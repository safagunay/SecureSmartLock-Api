using LockerApi.Models;
using LockerApi.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Web.Http;

namespace LockerApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiController
    {
        private readonly QRCodeService _qrCodeService = new QRCodeService();
        private ApplicationUserManager _userManager;
        public DeviceController()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
                DurationInSeconds = SettingsService.QRCodeDuration * 60,
                CreationDateTime = qrEntity.CreationDateTime,
                ExpirationDateTime = qrEntity.ExpirationDateTime.Value
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
