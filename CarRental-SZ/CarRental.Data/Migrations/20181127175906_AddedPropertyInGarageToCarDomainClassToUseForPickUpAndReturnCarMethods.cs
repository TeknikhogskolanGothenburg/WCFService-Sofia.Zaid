using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class AddedPropertyInGarageToCarDomainClassToUseForPickUpAndReturnCarMethods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InGarage",
                table: "Cars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGarage",
                table: "Cars");
        }
    }
}
