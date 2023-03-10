// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wallet.DAL;

#nullable disable

namespace Wallet.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230116143116_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("WalletBLL.Profit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("JobSalary")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SideJobSalary")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TransfersOnTheCard")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserCardId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserCardId");

                    b.ToTable("Profit");
                });

            modelBuilder.Entity("WalletDAL.Entities.Expenses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Entertainment")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Food")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Rent")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TransfersFromTheCard")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Transport")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserCardId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UtilityServices")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserCardId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("WalletDAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WalletDAL.Entities.UserCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<long>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserCard");
                });

            modelBuilder.Entity("WalletBLL.Profit", b =>
                {
                    b.HasOne("WalletDAL.Entities.UserCard", "UserCard")
                        .WithMany("Profits")
                        .HasForeignKey("UserCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCard");
                });

            modelBuilder.Entity("WalletDAL.Entities.Expenses", b =>
                {
                    b.HasOne("WalletDAL.Entities.UserCard", "UserCard")
                        .WithMany("Expenses")
                        .HasForeignKey("UserCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCard");
                });

            modelBuilder.Entity("WalletDAL.Entities.UserCard", b =>
                {
                    b.HasOne("WalletDAL.Entities.User", "User")
                        .WithMany("UserCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WalletDAL.Entities.User", b =>
                {
                    b.Navigation("UserCards");
                });

            modelBuilder.Entity("WalletDAL.Entities.UserCard", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Profits");
                });
#pragma warning restore 612, 618
        }
    }
}
