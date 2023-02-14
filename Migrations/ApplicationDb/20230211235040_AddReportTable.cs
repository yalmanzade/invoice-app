using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemsSold = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestSellingItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BestCompanyJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicesGenerated = table.Column<int>(type: "int", nullable: false),
                    LastInvoiceJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
