using BankofEduTech.Core.Application.Abstractions;
using BankofEduTech.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BankofEduTech.Infrastructure.Infrastructure.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public async Task SendMailAsync(EmailMessageModel model)
        {

            try
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("omerolmez.012@gmail.com");
                mail.To.Add(new MailAddress(model.To));
                mail.Subject = model.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = model.Body;


                SmtpClient client = new SmtpClient();

                client.Credentials = new System.Net.NetworkCredential("omerolmez.012@gmail.com", "yctilbfcyaznpozp");
                client.Port = 587; //25 
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;


                 await client.SendMailAsync(mail);

            }
            catch (Exception ex)
            {

            }
        }

    }
}

