using Spectre.Console;
using Phonebook.Models;
using static Phonebook.GeneralHelpers;

namespace Phonebook;

public class UserInterface
{
    public static void Menu()
    {
        using var db = new ContactContext();
        var emailSender = new EmailSender();
        var smsSender = new SmsSender();

        while (true)
        {
            Console.Clear();

            var startChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices("Browse contacts", "Show the details of all contacts", "Add a contact", "Exit")
                    .HighlightStyle(Color.PaleGreen1)
            );

            switch (startChoice)
            {
                case "Browse contacts":
                    var contactChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<Contact>()
                            .Title("Select a contact")
                            .UseConverter(c => $"{c.Name} ({c.Category})")
                            .AddChoices(db.Contacts.Select(c => c))
                            .HighlightStyle(Color.PaleGreen1)
                    );

                    var operationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"Selected contact: '{contactChoice.Name}'")
                            .AddChoices("View details", "Send an email", "Send an SMS", "Update", "Delete")
                            .HighlightStyle(Color.PaleGreen1)
                    );

                    switch (operationChoice)
                    {
                        case "View details":
                            if (contactChoice.Email == null) { contactChoice.Email = "No information provided"; }
                            if (contactChoice.PhoneNumber == null) { contactChoice.PhoneNumber = "No information provided"; }
                            
                            var contactInfoTable = new Table();
                            contactInfoTable.Title($"[palegreen1]{contactChoice.Name} ({contactChoice.Category})[/]");
                            contactInfoTable.AddColumns("ID", "Name", "Email address", "Phone number");
                            contactInfoTable.AddRow(contactChoice.Id.ToString(), contactChoice.Name, contactChoice.Email, contactChoice.PhoneNumber);
                            
                            AnsiConsole.Write(contactInfoTable);
                            PressAnyKey();
                            
                            break;
                        case "Send an email":
                            if (contactChoice.Email != null)
                            {
                                AnsiConsole.MarkupLine($"[underline]Sending an email to: {contactChoice.Email}[/]\n");
                                string emailSubject = GetUserInput("[palegreen1]Subject:[/] ", "Other");
                                string emailBody = GetUserInput("[palegreen1]Body:[/] ", "Other");
                                emailSender.SendEmail(contactChoice.Email, emailSubject, emailBody);
                                PressAnyKey();
                            }
                            else
                            {
                                AnsiConsole.Markup("[indianred1_1]No email address provided for this contact.[/]");
                                PressAnyKey();
                            }
                            
                            break;
                        case "Send an SMS":
                            if (contactChoice.PhoneNumber != null)
                            {
                                AnsiConsole.MarkupLine($"[underline]Sending an SMS to: {contactChoice.PhoneNumber}[/]\n");
                                string body = GetUserInput("[palegreen1]Message content: [/] ", "Other");
                                smsSender.SendSms(contactChoice.PhoneNumber, body);
                                PressAnyKey();
                            }
                            else
                            {
                                AnsiConsole.Markup("[indianred1_1]No phone number provided for this contact.[/]");
                                PressAnyKey();
                            }
                            break;
                        case "Update":
                            var updateChoice = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title($"What do you want to update in: '{contactChoice.Name}'")
                                    .AddChoices("Name", "Category", "Email address", "Phone number")
                                    .HighlightStyle(Color.PaleGreen1)
                            );
                            
                            AnsiConsole.MarkupLine($"[underline]Updating '{contactChoice.Name}'. You can type 'skip' to remove this detail[/]\n");
                            
                            switch (updateChoice)
                            {
                                case "Name":
                                    string updatedName = GetUserInput("[palegreen1]Name:[/] ", "Name");
                                    contactChoice.Name = updatedName;
                                    db.SaveChanges();
                                    break;
                                case "Category":
                                    string updatedCategory = GetUserInput("[palegreen1]Category:[/] ", "Category");
                                    contactChoice.Category = updatedCategory;
                                    db.SaveChanges();
                                    break;
                                case "Email address":
                                    string updatedEmailAddress = GetUserInput("[palegreen1]Email address (e.g. 'john@mailprovider.com'):[/] ", "Email");
                                    contactChoice.Email = updatedEmailAddress;
                                    db.SaveChanges();
                                    break;
                                case "Phone number":
                                    string updatedPhoneNumber = GetUserInput("[palegreen1]Phone number (e.g. '[grey78]+48[/]000333999'):[/] ", "PhoneNumber");
                                    contactChoice.PhoneNumber = updatedPhoneNumber;
                                    db.SaveChanges();
                                    break;
                            }
                            
                            break;
                        case "Delete":
                            db.Remove(contactChoice);
                            db.SaveChanges();
                            AnsiConsole.MarkupLine("[honeydew2]Contact deleted successfully.[/]");
                            PressAnyKey();
                            break;
                    }

                    break;
                case "Show the details of all contacts":
                    var contactsInfoTable = new Table();
                    contactsInfoTable.Title("[palegreen1]All contacts[/]");
                    contactsInfoTable.AddColumns("[palegreen1]ID[/]", "[palegreen1]Name[/]", "[palegreen1]Email address[/]", "[palegreen1]Phone number[/]", "[palegreen1]Category[/]");
                    
                    var allContacts = db.Contacts.ToList(); ;
                    
                    foreach (var contact in allContacts)
                    {
                        if (contact.Email == null) { contact.Email = "No information provided"; }
                        if (contact.PhoneNumber == null) { contact.PhoneNumber = "No information provided"; }
                        if (contact.Category == null) { contact.Category = "No information provided"; }
                        contactsInfoTable.AddRow(contact.Id.ToString(), contact.Name, contact.Email, contact.PhoneNumber, contact.Category);
                    }
                    
                    AnsiConsole.Write(contactsInfoTable);
                    PressAnyKey();
                    
                    break;
                case "Add a contact":
                    AnsiConsole.MarkupLine("[underline]Adding a contact. You can type 'skip' to skip a detail.[/]\n");

                    string name = GetUserInput("[palegreen1]Name:[/] ", "Name");
                    string category = GetUserInput("[palegreen1]Category:[/] ", "Category");
                    string emailAddress = GetUserInput("[palegreen1]Email address (e.g. 'john@mailprovider.com'):[/] ", "Email");
                    string phoneNumber = GetUserInput("[palegreen1]Phone number (e.g. '[grey78]+48[/]000333999'):[/] ", "PhoneNumber");

                    db.Add((new Contact(name, emailAddress, phoneNumber, category)));
                    db.SaveChanges();

                    AnsiConsole.MarkupLine("\n[honeydew2]Contact added successfully.[/]");
                    PressAnyKey();

                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}