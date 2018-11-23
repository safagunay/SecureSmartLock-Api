namespace LockerApi.Services
{
    public static class SettingsService
    {
        public const int ConfirmationTokenLength = 4;
        //In minutes
        public const int ConfirmationTokenDuration = 20;
        //In minutes
        public const int ConfirmationTokenTableCleanUpPeriod = 50;
        //In Minutes
        public const int QRCodeDuration = 1;
        //In minutes
        public const int QRCodeTableCleanUpPeriod = 10;
    }
}