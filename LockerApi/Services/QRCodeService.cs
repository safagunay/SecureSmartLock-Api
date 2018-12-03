using LockerApi.Libraries;
using LockerApi.Models;
using LockerApi.Services.Repositories;

namespace LockerApi.Services
{
    public class QRCodeService
    {
        public string GenerateQRCode()
        {
            return ShortGuid.NewGuid().ToString();
        }
        public QRCode CreateQrCodeFor(string userId, string qrCode)
        {
            var duration = SettingsService.QRCodeDuration;
            var qrEntity = new QRCode()
            {
                CreationDateTime = System.DateTime.Now,
                ExpirationDateTime = System.DateTime.Now.AddMinutes(duration),
                Hash = HashService.HashQRCode(qrCode),
                User_Id = userId

            };
            QRCodeRepository.insertOrUpdate(qrEntity);
            return qrEntity;
        }

        public bool VerifyQRCodeFor(string userId, string qrCode)
        {
            var qr = QRCodeRepository.getByUserId(userId);
            return qr != null ?
                !qr.IsExpired && HashService.HashQRCode(qrCode) == qr.Hash :
                false;
        }
    }
}