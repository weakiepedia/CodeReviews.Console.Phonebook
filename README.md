# Simple phonebook app connected to SQL Server database written in C#.

### Technologies and external libraries I used:
- Microsoft SQL Server
- EntityFrameworkCore
- Spectre.Console
- Twilio

### Features: 
- Creating, reading, updating and deleting contacts in the database
- Sending emails to saved contacts with Gmail SMTP
- Sending SMS to saved contacts with Twilio

## How to setup the app?
Open config.json file in a text editor and fill in the needed data
- "UserEmail" - Your email address
- "UserPassword" - Google app password (https://support.google.com/accounts/answer/185833?hl=en)
- "TwilioAccountSID" - Can be found on your twilio account
- "TwilioAuthToken" - Can be found on your twilio account
- "TwilioPhoneNumber" - Can be found on your twilio account
