using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entity;

namespace Wallet.DAL.Configuration;

public class ExpensesConfiguration: IEntityTypeConfiguration<Expenses>
{
    public void Configure(EntityTypeBuilder<Expenses> builder)
    {
        builder.ToTable("Expenses").HasKey(x => x.Id);
        builder.HasOne(y => y.UserCard)
            .WithMany(x => x.Expenses)
            .HasForeignKey(x => x.UserCardId);    
    }
}