using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SportsStore.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();
        public IQueryable<OrderMainPart> AllOrders
        {
            get
            {
                return context.Orders.Include("User");
            }
        }

        public IQueryable<OrderMainPart> GetUserOrders(int userID)
        {
            var user = context.Users.Include(x=>x.Orders).FirstOrDefault(x=>x.userID==userID);
            return user.Orders.AsQueryable();
        }

        public void AddUserOrder(int userID, ShippingDetails shippingDetails)
        {
            try
            {
                var user = context.Users
                    .Include(x=>x.Orders)
                    .Include(x => x.ShoppingCart)
                    .Include(x=>x.ShoppingCart.Select(p=>p.Product))
                    .FirstOrDefault(x => x.userID == userID);
                DateTime now = DateTime.Now;
                DateTime timeNow = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                List<OrderExtension> extension = new List<OrderExtension>();
                foreach (var line in user.ShoppingCart)
                {
                    extension.Add(new OrderExtension { Product = line.Product, Quantity = line.Quantity });
                }
                OrderMainPart order = new OrderMainPart
                {
                    OrderTime = timeNow,
                    User = user,
                    ShippingDetails = shippingDetails,
                    OrderExtension = extension
                };
               
                user.Orders.Add(order);
                context.CartLines.RemoveRange(user.ShoppingCart.AsEnumerable());
                context.SaveChanges();
                ComleteOrder(order);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
        public OrderMainPart OrderExtension(int orderID)
        {
            return context.Orders
                .Include(x => x.ShippingDetails)
                .Include(x => x.User)
                .Include(x => x.OrderExtension)
                .Include(x => x.OrderExtension.Select(p => p.Product))
                .FirstOrDefault(x => x.orderMainPartID == orderID);
        }

        void ComleteOrder(OrderMainPart order)
        {
            EmailSettings es = new EmailSettings();
            es.MailToAdress = order.ShippingDetails.Email;
            try
            {
                EmailOrderProcessor emailProc = new EmailOrderProcessor(es);
                emailProc.ProcessorOrder(order);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
