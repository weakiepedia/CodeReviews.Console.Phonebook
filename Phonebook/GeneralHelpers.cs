using Spectre.Console;

namespace Phonebook;

public class GeneralHelpers
{
    public static string GetUserInput(string message, string inputType)
    {
        AnsiConsole.Markup(message);

        bool validInput = false;
        while (validInput == false)
        {
            string userInput = Console.ReadLine();
            
            if (inputType == "Name")
            {
                if (userInput == "skip") { AnsiConsole.Markup("[indianred1_1]You can't skip name, please try again. (Name): [/]"); continue; }
                
                if (userInput == "" || userInput == null)
                {
                    AnsiConsole.Markup("[indianred1_1]Name can't be empty, please try again. (Name): [/]");
                    validInput = false;
                }
                else { return userInput; }
            }
            
            if (inputType == "Category")
            {
                if (userInput == "skip") { return null; }
                
                if (userInput == "" || userInput == null)
                {
                    AnsiConsole.Markup("[indianred1_1]Wrong format, please try again. (Category): [/]");
                    validInput = false;
                }
                else { return userInput; }
            }
            
            if (inputType == "Email")
            {
                if (userInput == "skip") { return null; }
                
                if (userInput == "" || userInput == null || !userInput.Contains("@") || !userInput.Contains("."))
                {
                    AnsiConsole.Markup("[indianred1_1]Wrong format, please try again. (Email): [/]");
                    validInput = false;
                }
                else { return userInput; }
            }

            if (inputType == "PhoneNumber")
            {
                if (userInput == "skip") { return null; }
                
                if (userInput == "" || userInput == null || userInput.Length > 16 || !userInput.StartsWith("+") || !userInput.Substring(1).All(char.IsDigit))
                {
                    AnsiConsole.Markup("[indianred1_1]Wrong format, please try again. (Phone number): [/]");
                    validInput = false;
                }
                else { return userInput; }
            }
            
            if (inputType == "Other")
            {
                if (userInput == "" || userInput == null)
                {
                    AnsiConsole.Markup("[indianred1_1]Can't be empty, please try again: [/]");
                    validInput = false;
                }
                else { return userInput; }
            }
        }

        return "";
    }
    
    public static void PressAnyKey()
    {
        AnsiConsole.MarkupLine("\n[grey78]Press any key to continue...[/]");
        Console.ReadKey();
    }
}