using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pampa.InSol.Infrastructure.EmailSender
{
    public class EmailSernder
    {
        public EmailSernder()
        {
            to = new List<string>();
        }

        public string Body { get; set; }
        public string Asunto { get; set; }
        private List<string> to { get; set; }

        public void To(string emailAddress)
        {
            to.Add(emailAddress);
        }

        public void SendHtmlEmail()
        {
            try
            {
                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("mailSettings/smtp");
                SmtpClient mySmtpClient = new SmtpClient
                {
                    Host = section.Network.Host,
                    Port = section.Network.Port,
                    UseDefaultCredentials = section.Network.DefaultCredentials
                };

                MailMessage myMail = new MailMessage
                {
                    From = new MailAddress(section.From),
                    Priority = MailPriority.Normal,
                    Subject = Asunto,
                    SubjectEncoding = Encoding.UTF8,
                    BodyEncoding = Encoding.UTF8,
                    Body = Body,
                    IsBodyHtml = true
                };

                //Recorre lista de destinatarios
                foreach (var address in to)
                {
                    myMail.To.Add(address);
                }

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                    ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
