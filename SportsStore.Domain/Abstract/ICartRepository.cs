using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface ICartRepository
    {

        IQueryable<CartLine> Items { get; }

        void AddItem(int productID, int quantity);

        void DecrementQuantity(int productID);

        decimal ComputeTotalValue();

        void RemoveLine(int productID);

        void Clear();

        IQueryable<CartLine> GetUserCart();

        void SetUserId(int id);

        void EndSession();
    }
}
