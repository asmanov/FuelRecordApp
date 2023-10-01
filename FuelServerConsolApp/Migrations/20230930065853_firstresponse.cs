using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelServerConsolApp.Migrations
{
    /// <inheritdoc />
    public partial class firstresponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OddFuel",
                table: "Refuels",
                newName: "oddFuel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "oddFuel",
                table: "Refuels",
                newName: "OddFuel");
        }
    }
}
