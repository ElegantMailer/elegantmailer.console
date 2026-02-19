# ElegantMailer

**ElegantMailer** is a simple C# console application that allows users to send beautifully formatted HTML emails using their own credentials. This project leverages [MailKit](https://github.com/jstedfast/MailKit) and [MimeKit](https://github.com/jstedfast/MimeKit) for SMTP email delivery.

**Author:** Sanaz Alipour
**Website:**

---

## Features

- Send HTML emails via SMTP (tested with Gmail).
- Support for **CC** and **BCC** recipients.
- Uses external HTML templates for clean, customizable email content.
- **Easily replace the HTML template** with your own design.
- Easy to configure via a single file and variables.

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download).
- A Gmail account with **2-Step Verification enabled**.
- A **16-digit Google App Password** for SMTP authentication.
- A folder named `Templates` containing your HTML email template (e.g., `email-template.html`).

---

## Getting Started

1. Clone this repository:

```bash
git clone https://github.com/yourusername/elegantmailer.git
cd elegantmailer
```

2. Open the project in Visual Studio or your preferred IDE.
3. Replace the configuration section in Program.cs:

```bash
string senderEmail = "your email";
string appPassword = "your app password";
string recipientEmail = "recipient@example.com";
string templatePath = "Templates/email-template.html";
```

4. Add your HTML template file inside the Templates folder.

- You can easily replace the default template with your own HTML.
- Remember: All CSS styles should be inline to ensure proper rendering in email clients.

Run the project:

```bash
dotnet run
```

You should see console messages confirming the email has been dispatched successfully.

CC and BCC
You can add additional recipients in the configuration section:

```bash
var ccList = new List<string> { "cc@example.com" };
var bccList = new List<string> { "bcc@example.com" };
```

- CC: All recipients are visible to others.
- BCC: Recipients are hidden from others.

**Notes**

Ensure the Templates folder exists next to the executable.
This project is intended for learning and simple email automation purposes.

**Do not share your credentials publicly.**

Inline CSS is recommended for maximum email client compatibility.

**License**

This project is licensed under the MIT License.
