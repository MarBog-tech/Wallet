using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.DAL;
using Wallet.DAL.Interfaces;
using Wallet.Domain.Entity;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace WalletDAL.Repositories;

class UserRepository : IRepository<User>
{
    private AppDbContext db;

    public UserRepository(AppDbContext context)
    {
        db = context;
    }

    public IQueryable<User> GetAll()
    {
        return db.Users;
    }

    public async Task Create(User item)
    {
        await db.Users.AddAsync(item);
    }

    public async Task Delete(User item)
    {
       db.Users.Remove(item);
    }

    public async Task<User> Update(User item)
    {
        db.Users.Update(item);
        return item;
    }
}