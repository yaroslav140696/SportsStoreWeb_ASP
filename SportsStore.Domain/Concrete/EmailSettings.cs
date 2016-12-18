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
        public string MailToAdress { get; set; }
        public string MailFromAdress = "yaroslav140696@gmail.com";
        public bool UseSsl = true;
        public string Username = "yaroslav140696@gmail.com";
        public string Password = "fcdchooligans1";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"d:\sports_store_email";
    }
  
}
