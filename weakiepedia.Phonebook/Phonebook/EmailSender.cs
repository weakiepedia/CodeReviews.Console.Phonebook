using Spectre.Console;
using System.Net;
using System.Net.Mail;
using static Phonebook.ConfigurationHelper;

namespace Phonebook;

public class EmailSender
{
    private string smtpAddress = "smtp.gmail.com";
    private int smtpPort = 587;
    private string emailSender = GetUserEmail();
    private string password = GetUserPassword();
    private string emailReceiver;
    private string subject;
    private string body;

    public void SendEmail(string emailReceiver, string subject, string body)
    {
        MailMessage email = new MailMessage();
        email.From = new MailAddress(emailSender);
        email.To.Add(emailReceiver);
        email.Subject = subject;
        email.Body = body;
        
        SmtpClient smtpClient = new SmtpClient(smtpAddress, smtpPort);
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(emailSender, password);

        try
        {
            smtpClient.Send(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        AnsiConsole.MarkupLine("[honeydew2]Email sent successfully.[/]");
    }
}