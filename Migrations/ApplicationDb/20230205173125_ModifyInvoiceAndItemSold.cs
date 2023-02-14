using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ModifyInvoiceAndItemSold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ItemsSold",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ItemsSold");
        }
    }
}
