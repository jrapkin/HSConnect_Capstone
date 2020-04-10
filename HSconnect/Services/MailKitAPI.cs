using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;

namespace HSconnect.Services
{
    public static class MailKitAPI
    {
        public static MimeMessage CreateEmail(string from, List<string> to, string messageBodyText)
        {
            //string[] fromAddressParts = from.Split(", ");
            MailboxAddress fromAddress = new MailboxAddress("", from);
            List<MailboxAddress> toAddresses = new List<MailboxAddress>();
            foreach (string address in to)
            {
                //string[] toAddressParts = address.Split(", ");
                toAddresses.Add(new MailboxAddress("", address));
            }
            return CreateEmail(fromAddress, toAddresses, messageBodyText);
        }
        public static MimeMessage CreateEmail(MailboxAddress fromAddress, List<MailboxAddress> toAddresses, string messageBodyText)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(fromAddress);
            foreach (MailboxAddress address in toAddresses)
            {
                message.To.Add(address);
            }
            message.Body = new TextPart("plain")
            {
                Text = messageBodyText
            };
            return message;
        }
        public static void SendEmail (MimeMessage message, string username, string password)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(username, password);
                client.Send(message);
                client.Disconnect(true);
            };
        }
    }
}
