using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IOrderRepository 
    {
        IQueryable<OrderMainPart> AllOrders { get; }
        IQueryable<OrderMainPart> GetUserOrders(int userID);
        OrderMainPart OrderExtension(int orderID);
        void AddUserOrder(int userID, ShippingDetails shippingDetails);
    }
}
