using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGym.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAndLastNameToUser : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				 name: "Name",
				 table: "Users",
				 type: "nvarchar(100)",
				 maxLength: 100,
				 nullable: false,
				 oldClrType: typeof(string),
				 oldType: "nvarchar(150)",
				 oldMaxLength: 150);

			migrationBuilder.AddColumn<string>(
				 name: "FirstName",
				 table: "Users",
				 type: "nvarchar(100)",
				 maxLength: 100,
				 nullable: false,
				 defaultValue: "");

			migrationBuilder.AddColumn<string>(
				 name: "LastName",
				 table: "Users",
				 type: "nvarchar(100)",
				 maxLength: 100,
				 nullable: false,
				 defaultValue: "");

			migrationBuilder.Sql(
			@"UPDATE Users
          SET FirstName = LEFT(Name, CHARINDEX(' ', Name + ' ') - 1),
              LastName = LTRIM(SUBSTRING(Name, CHARINDEX(' ', Name + ' '), LEN(Name)))
          WHERE Name IS NOT NULL");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
