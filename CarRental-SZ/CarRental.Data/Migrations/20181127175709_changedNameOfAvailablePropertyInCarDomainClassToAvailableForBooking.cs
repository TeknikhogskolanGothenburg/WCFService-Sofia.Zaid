using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class changedNameOfAvailablePropertyInCarDomainClassToAvailableForBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Cars",
                newName: "AvailableForBooking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableForBooking",
                table: "Cars",
                newName: "Available");
        }
    }
}
