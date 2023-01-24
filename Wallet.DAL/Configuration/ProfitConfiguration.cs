using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entity;

namespace Wallet.DAL.Configuration;

public class ProfitConfiguration: IEntityTypeConfiguration<Profit>
{
    public void Configure(EntityTypeBuilder<Profit> builder)
    {
        builder.ToTable("Profits").HasKey(x => x.Id);
        
        builder.HasOne(y => y.UserCard)
            .WithMany(x => x.Profits)
            .HasForeignKey(x => x.UserCardId);
    }
}