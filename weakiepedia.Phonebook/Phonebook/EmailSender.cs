using System.Net;
using System.Net.Mail;
using static Phonebook.ConfigurationHelper;

namespace Phonebook;

public class MailSender
{
    private string smtpAddress = "smtp.gmail.com";
    private int smtpPort = 587;
    private string emailSender = GetUserEmail();
    private string password = GetUserPassword();
    private string emailReceiver;
    private string subject;
    private string body;

    public void SendMail(string emailReceiver, string subject, string body)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(emailSender);
        mail.To.Add(emailReceiver);
        mail.Subject = subject;
        mail.Body = body;
        
        SmtpClient smtpClient = new SmtpClient(smtpAddress, smtpPort);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(emailSender, password);

        try
        {
            smtpClient.Send(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}