using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class RemovedManyToManyRelationshipBookingCarsNowOneBookingCanOnlyContainOneCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Bookings");

            migrationBuilder.CreateTable(
                name: "BookingCars",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCars", x => new { x.BookingId, x.CarId });
                    table.ForeignKey(
                        name: "FK_BookingCars_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCars_CarId",
                table: "BookingCars",
                column: "CarId");
        }
    }
}
