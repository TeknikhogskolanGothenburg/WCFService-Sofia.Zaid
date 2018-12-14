using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class RemoveInGarageFromCarClassAddCarInGarageToBookingClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGarage",
                table: "Cars");

            migrationBuilder.AddColumn<bool>(
                name: "CarInGarage",
                table: "Bookings",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarInGarage",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "InGarage",
                table: "Cars",
                nullable: false,
                defaultValue: false);
        }
    }
}
