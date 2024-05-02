using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rason",
                table: "CancelOrders",
                newName: "Reason");

            migrationBuilder.AddColumn<int>(
                name: "OlchovBirligi",
                table: "PartnerProducts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OlchovBirligi",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "CancelOrders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OlchovBirligi",
                table: "PartnerProducts");

            migrationBuilder.DropColumn(
                name: "OlchovBirligi",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "CancelOrders",
                newName: "Rason");

            migrationBuilder.AlterColumn<short>(
                name: "DiscountPrice",
                table: "CancelOrders",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
