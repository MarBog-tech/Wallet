using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.DAL;
using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WalletDAL.Repositories
{
    class ExpensesRepository : IRepository<Expenses>
    {
        private AppDbContext db;

        public ExpensesRepository(AppDbContext context)
        {
            this.db = context;
        }

        public async Task Create(Expenses item)
        {
            await db.Expenses.AddAsync(item);
        }

        public async Task Delete(Expenses item)
        {
            db.Expenses.Remove(item);
        }

        public IQueryable<Expenses> GetAll()
        {
            return db.Expenses.Include(o => o.UserCard);
        }

        public async Task<Expenses> Update(Expenses item)
        {
            db.Expenses.Update(item);
            return item;
        }
    }
}
