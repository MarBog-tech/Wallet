using System.Linq.Expressions;

namespace Wallet.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T item);

        IQueryable<T> GetAll();

        Task Delete(T item);

        Task<T> Update(T item);
    }
}
