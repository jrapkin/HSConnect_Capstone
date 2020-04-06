using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Services
{
    public static class MailKitAPI
    {
        public static MimeMessage CreateEmail (MailboxAddress fromAddress, List<MailboxAddress> toAddresses, string textContent)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(fromAddress);
            foreach (MailboxAddress address in toAddresses)
            {
                message.To.Add(address);
            }
            message.Body = new TextPart("plain")
            {
                Text = textContent
            };
            return message;
        }
        public static void SendEmail (MimeMessage message)
        {

        }
    }
}
