using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppLibrary.Migrations
{
    /// <inheritdoc />
    public partial class mighy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CreditCarts_UserId",
                table: "CreditCarts");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCarts_UserId",
                table: "CreditCarts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CreditCarts_UserId",
                table: "CreditCarts");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCarts_UserId",
                table: "CreditCarts",
                column: "UserId");
        }
    }
}
