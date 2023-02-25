using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class IsPaidInvoiceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPaid",
                table: "Invoices",
                newName: "IsPaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Invoices",
                newName: "isPaid");
        }
    }
}
