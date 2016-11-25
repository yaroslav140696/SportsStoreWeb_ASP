using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public class EFUserRepository : IRepository<User>
    {
        EFDbContext context = new EFDbContext();
        public EFUserRepository()
        {
            Database.SetInitializer(new MyInitializer());
        }
        public IQueryable<User> Items
        {
            get
            {
                return context.Users;
            }
        }

        public User DeleteItem(int userID)
        {
            throw new NotImplementedException();
        }

        public void SaveItem(User user)
        {
            if (user.userID == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                var dbEntry = context.Users.Find(user.userID);
                if (dbEntry != null)
                {
                    dbEntry.Email = user.Email;
                    dbEntry.FirtsName = user.FirtsName;
                    dbEntry.SecondName = user.SecondName;
                    dbEntry.Password = user.Password;
                }
            }
            context.SaveChanges();
        }
    }
}
