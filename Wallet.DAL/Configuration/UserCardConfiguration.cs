using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entity;

namespace Wallet.DAL.Configuration;

public class UserCardConfiguration: IEntityTypeConfiguration<UserCard>
{
    public void Configure(EntityTypeBuilder<UserCard> builder)
    {
        builder.ToTable("UserCards").HasKey(x => x.Id);

        builder.HasData(new UserCard
        {
            Id = 1,
            Number = 0000,
            Description = "My main card",
            Balance = 15000,
            UserId = 1
        });

        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(100).IsRequired();
        builder.Property(x=>x.Balance).HasDefaultValue(0);
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.UserCards)
            .HasForeignKey(y => y.UserId);
    }
}