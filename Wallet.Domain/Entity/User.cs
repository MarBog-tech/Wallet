using Wallet.Domain.Common;

namespace Wallet.Domain.Entity;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailLogin { get; set; }
    public string? Password { get; set; }
    public ICollection<UserCard>? UserCards { get; set; }
}