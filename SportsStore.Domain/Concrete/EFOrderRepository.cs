using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFOrderRepository : IRepository<OrderMainPart>
    {
        public IQueryable<OrderMainPart> Items
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public OrderMainPart DeleteItem(int itemID)
        {
            throw new NotImplementedException();
        }

        public void SaveItem(OrderMainPart item)
        {
            throw new NotImplementedException();
        }
    }
}
