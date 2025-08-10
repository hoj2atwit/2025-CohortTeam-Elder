using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGym.Migrations
{
    /// <inheritdoc />
    public partial class TrafficData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessPoint",
                table: "Checkins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassSessionId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClassSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    SessionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSessions_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Waitlist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    JoinedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waitlist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waitlist_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waitlist_ClassSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ClassSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClassSessionId",
                table: "Bookings",
                column: "ClassSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSessions_ClassId",
                table: "ClassSessions",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Waitlist_MemberId",
                table: "Waitlist",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Waitlist_SessionId",
                table: "Waitlist",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_ClassSessions_ClassSessionId",
                table: "Bookings",
                column: "ClassSessionId",
                principalTable: "ClassSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_ClassSessions_ClassSessionId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Waitlist");

            migrationBuilder.DropTable(
                name: "ClassSessions");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClassSessionId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AccessPoint",
                table: "Checkins");

            migrationBuilder.DropColumn(
                name: "ClassSessionId",
                table: "Bookings");
        }
    }
}
