using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelServerConsolApp.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels");

            migrationBuilder.AlterColumn<int>(
                name: "TrackId",
                table: "Refuels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels");

            migrationBuilder.AlterColumn<int>(
                name: "TrackId",
                table: "Refuels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
