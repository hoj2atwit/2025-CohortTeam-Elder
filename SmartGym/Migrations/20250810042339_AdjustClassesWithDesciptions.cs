using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGym.Migrations
{
    /// <inheritdoc />
    public partial class AdjustClassesWithDesciptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "ClassSessions",
                newName: "MaxCapacity");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Classes",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Classes",
                newName: "MaxCapacity");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClassSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HeadCount",
                table: "ClassSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageRef",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "HeadCount",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ImageRef",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "ClassSessions",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "MaxCapacity",
                table: "Classes",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Classes",
                newName: "CategoryId");
        }
    }
}
