using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class CreatedUniqueIndexForBookingEntitySoThatOneCarCanOnlyBeBookedAtOneSpecificTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId_StartTime_EndTime",
                table: "Bookings",
                columns: new[] { "CarId", "StartTime", "EndTime" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_CarId_StartTime_EndTime",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");
        }
    }
}
