using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddJsonItemsInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Invoices_InvoiceId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsSold_Invoices_InvoiceId",
                table: "ItemsSold");

            migrationBuilder.DropIndex(
                name: "IX_ItemsSold_InvoiceId",
                table: "ItemsSold");

            migrationBuilder.DropIndex(
                name: "IX_Fees_InvoiceId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Fees");

            migrationBuilder.AddColumn<string>(
                name: "ItemsJson",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsJson",
                table: "Invoices");

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceId",
                table: "Fees",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSold_InvoiceId",
                table: "ItemsSold",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_InvoiceId",
                table: "Fees",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fees_Invoices_InvoiceId",
                table: "Fees",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsSold_Invoices_InvoiceId",
                table: "ItemsSold",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
