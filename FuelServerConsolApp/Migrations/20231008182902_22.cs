using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelServerConsolApp.Migrations
{
    /// <inheritdoc />
    public partial class _22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refuels_Locations_LocationId",
                table: "Refuels");

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

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Refuels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Locations_LocationId",
                table: "Refuels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refuels_Locations_LocationId",
                table: "Refuels");

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

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Refuels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Locations_LocationId",
                table: "Refuels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refuels_Tracks_TrackId",
                table: "Refuels",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId");
        }
    }
}
