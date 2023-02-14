using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceId",
                table: "Fees",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fees_Invoices_InvoiceId",
                table: "Fees");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsSold_Invoices_InvoiceId",
                table: "ItemsSold");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_ItemsSold_InvoiceId",
                table: "ItemsSold");

            migrationBuilder.DropIndex(
                name: "IX_Fees_InvoiceId",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Fees");
        }
    }
}
