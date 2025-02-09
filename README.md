# 📒 Phonebook
A simple console application for managing contacts.

## 🚀 Features:
- Contact management
- Sending e-mails using Gmail SMTP
- Sending SMS using Twilio

## 🛠️ Technologies used:
- C# (.NET Core)
- Entity Framework Core
- Spectre.Console
- Twilio API
- SMTP (Gmail)

## ⚙️ How to setup the app?
Edit config.json:

```
"ConnectionString": "YourDatabaseConnectionString",
"UserEmail": "youremail@gmail.com",
"UserPassword": "yourpassword",
"TwilioAccountSID": "your_twilio_sid",
"TwilioAuthToken": "your_twilio_auth_token",
"TwilioPhoneNumber": "+1234567890"
```

- ConnectionString: Your SQL Server connection string
- UserEmail & UserPassword: SMTP credentials (preferably Gmail with app password)
- Twilio Credentials: Get these from your Twilio dashboard
