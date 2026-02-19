using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

class Program
{
    static async Task Main(string[] args)
    {
        // ==========================================
        // CONFIGURATION SECTION
        // ==========================================

        // Sender credentials (Ensure 2-Step Verification is enabled for App Passwords)
        string senderEmail = "your email";
        string appPassword = "your pass"; // Your 16-digit Google App Password

        // Primary recipient
        string recipientEmail = "email@example.com";

        // Path to the HTML template file
        // Assumes the file is inside a folder named "Templates" next to the .exe
        string templatePath = "Templates/email-template.html";

        try
        {
            Console.WriteLine("ℹ️  Initializing email client...");

            // 1. Load HTML Template
            // Validates if the template file exists in the 'Templates' folder.
            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"❌ The template file was not found!");
                Console.WriteLine("ℹ️  Please ensure a 'Templates' folder exists next to your application and contains template'.");
                return;
            }
            string htmlBody = await File.ReadAllTextAsync(templatePath);

            // 2. Construct the MIME Message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Semantics 2026 Team", senderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));

            // ---------------------------------------------------------
            // CC (Carbon Copy) Configuration
            // ---------------------------------------------------------
            // These recipients are visible to everyone else.
            var ccList = new List<string>
            {
                // "example@example.com",
                // "example2@example.com"
            };

            foreach (var ccEmail in ccList)
            {
                if (!string.IsNullOrWhiteSpace(ccEmail))
                    message.Cc.Add(MailboxAddress.Parse(ccEmail));
            }

            // ---------------------------------------------------------
            // BCC (Blind Carbon Copy) Configuration
            // ---------------------------------------------------------
            // These recipients receive the email but their addresses are hidden from others.
            var bccList = new List<string>
            {
                // "hidden-manager@example.com",
                // "backup-list@example.com"
            };

            foreach (var bccEmail in bccList)
            {
                if (!string.IsNullOrWhiteSpace(bccEmail))
                    message.Bcc.Add(MailboxAddress.Parse(bccEmail));
            }

            // Set Subject and Body
            message.Subject = "Proposal: Semantics 2026 Email Template";
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;
            message.Body = builder.ToMessageBody();

            // 3. Send Email via SMTP
            Console.WriteLine("ℹ️  Connecting to Gmail SMTP server...");
            using (var client = new SmtpClient())
            {
                // Connect to Gmail's SMTP server on port 587 using TLS
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Authenticate using the App Password
                await client.AuthenticateAsync(senderEmail, appPassword);

                // Send the message
                await client.SendAsync(message);

                // Disconnect gracefully
                await client.DisconnectAsync(true);
            }

            Console.WriteLine("✅ Email dispatched successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ [CRITICAL ERROR] An exception occurred: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}