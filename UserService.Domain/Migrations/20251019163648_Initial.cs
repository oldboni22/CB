using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseRelations_UserRoleData_StudentDataId",
                        column: x => x.StudentDataId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourseRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourseRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCourseRelations_UserRoleData_TeacherDataId",
                        column: x => x.TeacherDataId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherGroupRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherGroupRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherGroupRelations_UserRoleData_TeacherDataId",
                        column: x => x.TeacherDataId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    RoleDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoleData_RoleDataId",
                        column: x => x.RoleDataId,
                        principalTable: "UserRoleData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseRelations_StudentDataId",
                table: "StudentCourseRelations",
                column: "StudentDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourseRelations_TeacherDataId",
                table: "TeacherCourseRelations",
                column: "TeacherDataId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherGroupRelations_TeacherDataId",
                table: "TeacherGroupRelations",
                column: "TeacherDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleDataId",
                table: "Users",
                column: "RoleDataId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourseRelations");

            migrationBuilder.DropTable(
                name: "TeacherCourseRelations");

            migrationBuilder.DropTable(
                name: "TeacherGroupRelations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoleData");
        }
    }
}
