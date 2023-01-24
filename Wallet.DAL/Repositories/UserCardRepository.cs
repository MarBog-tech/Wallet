using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.DAL;
using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WalletDAL.Repositories
{
    public class UserCardRepository : IRepository<UserCard>
    {
        private AppDbContext db;

        public UserCardRepository(AppDbContext context)
        {
            this.db = context;
        }

        public IQueryable<UserCard> GetAll()
        {
            return db.UserCards;
        }

        public async Task Create(UserCard item)
        {
            await db.UserCards.AddAsync(item);
        }

        public async Task Delete(UserCard item)
        {
            db.UserCards.Remove(item);
        }

        public async Task<UserCard> Update(UserCard item)
        {
            db.UserCards.Update(item);
            return item;
        }
    }
}
