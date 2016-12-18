using SportsStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class EmailOrderProcessor
    {
        public EmailSettings emailSettings { get; set; }
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessorOrder(OrderMainPart order)
        {
            using (var smtpClient = new SmtpClient())
            {
                var shippingdetails = order.ShippingDetails;
                var totalValue = order.OrderExtension.Sum(x => x.Product.Price * x.Quantity);

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
                foreach (var line in order.OrderExtension)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} \t{1} x {2} \t\t(subtotal : {3:c})\t", line.Product.Name, line.Quantity, line.Product.Price, subtotal);
                    body.AppendLine();
                }
                body.AppendFormat("Total order value: {0,50:c}", totalValue)
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
