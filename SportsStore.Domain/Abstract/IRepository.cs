using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> Items { get; }
        void SaveItem(T item);
        T DeleteItem(int itemID);
    }
}
