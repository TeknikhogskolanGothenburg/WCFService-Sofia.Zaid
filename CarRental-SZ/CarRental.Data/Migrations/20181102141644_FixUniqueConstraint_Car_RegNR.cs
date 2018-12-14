using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Data2.Migrations
{
    public partial class FixUniqueConstraint_Car_RegNR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNo",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RegistrationNo",
                table: "Cars",
                column: "RegistrationNo",
                unique: true,
                filter: "[RegistrationNo] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cars_RegistrationNo",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNo",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
