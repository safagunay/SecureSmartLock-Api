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
                CreatedOnUTC = DateService.getCurrentUTC(),
                ExpiresOnUTC = DateService.getCurrentUTC().AddMinutes(duration),
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
                !DateService.isExpiredUTC(qr.ExpiresOnUTC) && HashService.HashQRCode(qrCode) == qr.Hash :
                false;
        }

        public string GetUserId(string qrCode)
        {
            var QRCode = QRCodeRepository.GetByHash(qrCode);
            if (QRCode == null || DateService.isExpiredUTC(QRCode.ExpiresOnUTC))
                return null;
            return QRCode.User_Id;

        }
    }
}