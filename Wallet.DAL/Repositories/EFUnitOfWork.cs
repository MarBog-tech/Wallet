using Wallet.DAL;
using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;
using WalletDAL.Interfaces;

namespace WalletDAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private AppDbContext db;
        private UserRepository userRepository;
        private UserCardRepository userCardRepository;
        private ProfitRepository profitRepository;
        private ExpensesRepository expensesRepository;

        
        public EFUnitOfWork(AppDbContext context)
        {
            db = context;
        }

        public IRepository<User> User 
        { 
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            } 
        }


        public IRepository<UserCard> UserCard 
        { 
            get
            {
                if(userCardRepository == null)
                {
                    userCardRepository = new UserCardRepository(db);
                }
                return userCardRepository;
            } 
        }

        public IRepository<Profit> Profit
        {
            get
            {
                if(profitRepository == null)
                {
                    profitRepository = new ProfitRepository(db);
                }
                return profitRepository;
            }
        }

        public IRepository<Expenses> Expenses
        {
            get
            {
                if (expensesRepository == null)
                {
                    expensesRepository = new ExpensesRepository(db);
                }
                return expensesRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public async Task Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
