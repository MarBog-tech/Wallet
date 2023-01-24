using Wallet.Domain.Common;
using Wallet.Domain.Enum;

namespace Wallet.Domain.Entity;

public sealed class Expenses: BaseEntity
{
    public TypeExpenses Description { get; set; }
    public decimal Value { get; set; }
    public DateTime Time { get; set; }
    public int UserCardId { get; set; }
    public UserCard? UserCard { get; set; }
}