using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IpAddressViaMailCSharp.Network
{
    class MailServer
    {
        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if(e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if(e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Email sent.");
            }
            mailSent = true;
        }
        public static void SendMail(string body, string subject)
        {
            // Specify the e-mail sender. 
            // Create a mailing address that includes a UTF8 character 
            // in the display name.
            MailAddress from = new MailAddress(Config.CurrentConfig.FromMailAddress, "WenX", System.Text.Encoding.UTF8);
            
            // Specify the message content.
            MailMessage message = new MailMessage();
            message.From = from;
            foreach (var i in Config.CurrentConfig.ToMailAddresses)
            {
                // Set destinations for the e-mail message.
                MailAddress to = new MailAddress(i);
                message.To.Add(to);
            }
            message.Body = body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            SendMail(message);
        }
        private static void SendMail(MailMessage message)
        {
            // SMTP host.
            SmtpClient client = new SmtpClient(Config.CurrentConfig.SmtpServer);
            client.Credentials =
                new System.Net.NetworkCredential(Config.CurrentConfig.SmtpUsername, Config.CurrentConfig.SmtpPassword);

            // Set the method that is called back when the send operation ends.
            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);
            // The userState can be any object that allows your callback  
            // method to identify this send operation. 
            // For this example, the userToken is a string constant. 
            string userState = "test message1";
            client.SendAsync(message, userState);
            Console.WriteLine("Sending message...");
        }
    }
}
