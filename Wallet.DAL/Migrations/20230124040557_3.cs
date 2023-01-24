using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "EmailLogin",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "UserCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Profits",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Expenses",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "UserCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "EmailLogin",
                value: "marchuk_bogdan@i.ua");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailLogin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Profits");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Expenses");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Login",
                value: "MarBog");
        }
    }
}
