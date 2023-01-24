using Wallet.Domain.Common;

namespace Wallet.Domain.Entity;
public sealed class UserCard: BaseEntity
{
    public int Number { get; set; }
    public string? Description { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Profit>? Profits { get; set; }
    public ICollection<Expenses>? Expenses { get; set; }
}