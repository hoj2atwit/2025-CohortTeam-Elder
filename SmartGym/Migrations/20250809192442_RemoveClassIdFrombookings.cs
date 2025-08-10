using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGym.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClassIdFrombookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Classes_ClassId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClassId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClassId",
                table: "Bookings",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Classes_ClassId",
                table: "Bookings",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
