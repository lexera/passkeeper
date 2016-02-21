using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace wpfPass
{
    class EmailingClass
    {
        public void SendEmail(string eAdress, string eSubject, string eMessage)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("passkeeperinfo@gmail.com");
                mail.To.Add(eAdress);
                mail.Subject = eSubject;
                mail.Body = eMessage;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("passkeeperinfo@gmail.com", "passkeeper131");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch(Exception err)
            {
                throw err;
            }
        }
    }
}
