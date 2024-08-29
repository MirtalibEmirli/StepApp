using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLibrary.Migrations
{
    /// <inheritdoc />
    public partial class mighyw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "CreditCarts");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "CreditCarts");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CreditCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVV",
                table: "CreditCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "CreditCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "CreditCarts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
