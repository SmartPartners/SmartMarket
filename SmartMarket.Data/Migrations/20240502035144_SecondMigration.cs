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
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Cards",
                newName: "CamePrice");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                table: "PartnerProducts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "Cards",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "PartnerProducts");

            migrationBuilder.RenameColumn(
                name: "CamePrice",
                table: "Cards",
                newName: "Price");

            migrationBuilder.AlterColumn<short>(
                name: "DiscountPrice",
                table: "Cards",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
