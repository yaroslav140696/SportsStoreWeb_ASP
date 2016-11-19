using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Net.Mail;
using System.Net;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAdress = "yaroslav140696@gmail.com";
        public string MailFromAdress = "yaroslav140696@gmail.com";
        public bool UseSsl = true;
        public string Username = "yaroslav140696@gmail.com";
        public string Password = "fcdchooligans1";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"d:\sports_store_email";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessorOrder(Cart cart, ShippingDetails shippingdetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("----------------------------------------------------------")
                    .AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    
                    body.AppendFormat("{0} \t{1} x {2} \t\t(subtotal : {3:c})\t",line.Product.Name, line.Quantity, line.Product.Price, subtotal);
                    body.AppendLine();
                }
                body.AppendFormat("Total order value: {0,50:c}", cart.ComputeTotalValue())
                    .AppendLine("\n---------------------------------------------------------")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingdetails.Name)
                    .AppendLine(shippingdetails.City)
                    .AppendLine(shippingdetails.Country)
                    .AppendLine(shippingdetails.Email)
                    .AppendLine(shippingdetails.Line1)
                    .AppendLine(shippingdetails.Line2 ?? "")
                    .AppendLine(shippingdetails.Line3 ?? "")
                    
                    .AppendLine(shippingdetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingdetails.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAdress, emailSettings.MailToAdress,
                    "New order submitted!", body.ToString());
                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }
    }
}
