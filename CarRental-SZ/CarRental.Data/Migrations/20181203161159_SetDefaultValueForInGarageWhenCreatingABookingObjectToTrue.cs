using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class SetDefaultValueForInGarageWhenCreatingABookingObjectToTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CarInGarage",
                table: "Bookings",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CarInGarage",
                table: "Bookings",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }
    }
}
