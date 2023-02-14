using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoice.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPropertyInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Invoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Invoice");
        }
    }
}
