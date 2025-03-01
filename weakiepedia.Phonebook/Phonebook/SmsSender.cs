using Spectre.Console;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static Phonebook.ConfigurationHelper;

namespace Phonebook;

public class SmsSender
{
    private string accountSid;
    private string authToken;
    
    public void SendSms(string phoneNumber, string body)
    {
        TwilioClient.Init(GetTwilioAccountSid(), GetTwilioAuthToken());

        var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber));
        messageOptions.From = new PhoneNumber(GetTwilioPhoneNumber());
        messageOptions.Body = body;

        try
        {
            var message = MessageResource.Create(messageOptions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        AnsiConsole.MarkupLine("[honeydew2]SMS sent successfully.[/]");
    }
}