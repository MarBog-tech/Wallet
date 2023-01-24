using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<long>(type: "INTEGER", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCard_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Food = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransfersFromTheCard = table.Column<decimal>(type: "TEXT", nullable: false),
                    UtilityServices = table.Column<decimal>(type: "TEXT", nullable: false),
                    Entertainment = table.Column<decimal>(type: "TEXT", nullable: false),
                    Transport = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rent = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserCardId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_UserCard_UserCardId",
                        column: x => x.UserCardId,
                        principalTable: "UserCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    SideJobSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransfersOnTheCard = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserCardId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profit_UserCard_UserCardId",
                        column: x => x.UserCardId,
                        principalTable: "UserCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserCardId",
                table: "Expenses",
                column: "UserCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Profit_UserCardId",
                table: "Profit",
                column: "UserCardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCard_UserId",
                table: "UserCard",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Profit");

            migrationBuilder.DropTable(
                name: "UserCard");
        }
    }
}
