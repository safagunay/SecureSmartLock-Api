using LockerApi.Models;
using System;
using System.Linq;
using System.Net.Mail;

namespace LockerApi.Services
{
    public class TokenConfirmationService
    {
        private static readonly Random random = new Random();
        private static readonly SmtpClient _smtpClient = new SmtpClient();

        public bool deleteConfirmationToken(ApplicationUser user, ConfirmationTokenType type)
        {
            return ConfirmationTokenRepository.deleteByUserId(user.Id, type);
        }

        public void sendConfirmationTokenTo(ApplicationUser user, ConfirmationTokenType type)
        {
            var token = randomString(SettingsService.ConfirmationTokenLength);
            DateTime expirationDateTime = System.DateTime.Now.AddMinutes(SettingsService.ConfirmationTokenDuration);
            var confirmationToken = new ConfirmationToken()
            {
                Token = token,
                Type = type,
                ExpirationDateTime = expirationDateTime,
                User_Id = user.Id
            };
            ConfirmationTokenRepository.insertOrUpdate(confirmationToken);
            sendConfirmationMessage(user, confirmationToken);
        }

        public bool validateConfirmationTokenFor(ApplicationUser user, string token, ConfirmationTokenType type)
        {
            var confirmationToken = ConfirmationTokenRepository.getByUserId(user.Id, type);
            return confirmationToken.IsExpired ? false : token == confirmationToken.Token;
        }

        #region Utilities
        private string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            String result = null;
            lock (random)
            {
                result = new string(Enumerable.Repeat(chars, length).
                    Select(s => s[random.Next(s.Length)]).ToArray());
            }

            return result;
        }

        private void sendConfirmationMessage(ApplicationUser user, ConfirmationToken confirmationToken)
        {
            if (confirmationToken.Type == ConfirmationTokenType.EMAIL ||
                confirmationToken.Type == ConfirmationTokenType.PASSWORD_RESET)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.Email)); //replace with valid value
                message.IsBodyHtml = false;
                if (confirmationToken.Type == ConfirmationTokenType.EMAIL)
                {
                    message.Subject = "Smart Locker: verify your email";
                    message.Body = string.Format("Please use the following confirmation token in {0} minutes " +
                        "to verify your email address.\n" +
                        confirmationToken.Token, SettingsService.ConfirmationTokenDuration);
                }
                if (confirmationToken.Type == ConfirmationTokenType.PASSWORD_RESET)
                {
                    message.Subject = "Smart Locker: Password Reset";
                    message.Body = string.Format("We received a request to reset your password.\n" +
                        "Please use the below confirmation token in {0} minutes to reset your password.\n" +
                        confirmationToken.Token, SettingsService.ConfirmationTokenDuration);
                }
                _smtpClient.Send(message);
            }
            else if (confirmationToken.Type == ConfirmationTokenType.PHONE_NUMBER)
            {

            }

        }
        #endregion
    }


}