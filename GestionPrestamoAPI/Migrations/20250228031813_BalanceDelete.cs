using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionPrestamoAPI.Migrations
{
    /// <inheritdoc />
    public partial class BalanceDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Prestamos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "Prestamos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
