namespace DutchTreat.Services
{
    public interface IMailService
    {
        // Check for SendGrid or MailChip services
        void SendMessage(string to, string subject, string body);
    }
}