using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.DAL;
using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WalletDAL.Repositories
{
    public class ProfitRepository : IRepository<Profit> 
    {
        private AppDbContext db;

        public ProfitRepository(AppDbContext context)
        {
            this.db = context;
        }

        public IQueryable<Profit> GetAll()
        {
            return db.Profits.Include(o => o.UserCard);
        }
        
        public async Task Create(Profit item)
        {
            await db.Profits.AddAsync(item);
        }

        public async Task<Profit> Update(Profit item)
        {
            db.Profits.Update(item);
            return item;
        }

        public async Task Delete(Profit item)
        {
            db.Profits.Remove(item);
        }
    }
}
