using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;

namespace WalletDAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> User { get; }
        IRepository<UserCard> UserCard { get; }
        IRepository<Profit> Profit { get; }
        IRepository<Expenses> Expenses { get; }
        Task Save();
        Task Dispose();
    }
}
