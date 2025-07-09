using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NailSalon.Core.Helpers
{
    public static class EmailService
    {
        public static string SendEmail(string inputEmail, string subject, string body)
        {

            string returnString = "";

            try
            {
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                // set up the Gmail server
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("emiraslanovaroya@gmail.com", "jboj vpyf wtir mrps");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
               
                // draft the email
                MailAddress fromAddress = new MailAddress("emiraslanovaroya@gmail.com");
                email.From = fromAddress;
                email.To.Add(inputEmail);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;

                smtp.Send(email);

                returnString = "Success! Please check your e-mail.";
            }
            catch (Exception ex)
            {
                returnString = "Error: " + ex.Message;
                if (ex.InnerException != null)
                {
                    returnString += " - Inner: " + ex.InnerException.Message;
                }
            }
            return returnString;
        }


    }
}
