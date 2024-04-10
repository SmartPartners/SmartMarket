using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CencelerCasherId",
                table: "CancelOrders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CancelOrders_CencelerCasherId",
                table: "CancelOrders",
                column: "CencelerCasherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CancelOrders_Users_CencelerCasherId",
                table: "CancelOrders",
                column: "CencelerCasherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CancelOrders_Users_CencelerCasherId",
                table: "CancelOrders");

            migrationBuilder.DropIndex(
                name: "IX_CancelOrders_CencelerCasherId",
                table: "CancelOrders");

            migrationBuilder.DropColumn(
                name: "CencelerCasherId",
                table: "CancelOrders");
        }
    }
}
