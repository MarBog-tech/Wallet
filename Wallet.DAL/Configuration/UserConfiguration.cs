using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entity;
using Wallet.Domain.Helpers;

namespace Wallet.DAL.Configuration;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(x => x.Id);

        builder.HasData(new User
        {
            Id = 1,
            FirstName = "Bogdan",
            LastName = "Marchuk",
            EmailLogin = "marchuk_bogdan@i.ua",
            Password = HashPasswordHelper.HashPassword("12345678")
        });

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.EmailLogin).IsRequired();
        builder.Property(x => x.Password).IsRequired();
    }
}