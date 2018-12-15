namespace LockerApi.Services
{
    public static class HashService
    {
        //private readonly static HashAlgorithm h = new MD5CryptoServiceProvider();
        public static string HashQRCode(string qrCode)
        {
            return qrCode;
        }

        public static string HashDeviceCode(string deviceCode)
        {
            return deviceCode;
        }

        public static string HashDeviceSecret(string deviceSecret)
        {
            return deviceSecret;
        }
    }
}