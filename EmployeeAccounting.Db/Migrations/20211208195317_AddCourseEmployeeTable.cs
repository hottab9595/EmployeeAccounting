using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeAccounting.Db.Migrations
{
    public partial class AddCourseEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEmployee");

            migrationBuilder.CreateTable(
                name: "CourseEmployees",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEmployees", x => new { x.CourseId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_CourseEmployees_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseEmployees",
                columns: new[] { "CourseId", "EmployeeId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "CourseEmployees",
                columns: new[] { "CourseId", "EmployeeId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEmployees_EmployeeId",
                table: "CourseEmployees",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEmployees");

            migrationBuilder.CreateTable(
                name: "CourseEmployee",
                columns: table => new
                {
                    CoursesID = table.Column<int>(type: "int", nullable: false),
                    EmployeesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEmployee", x => new { x.CoursesID, x.EmployeesID });
                    table.ForeignKey(
                        name: "FK_CourseEmployee_Courses_CoursesID",
                        column: x => x.CoursesID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseEmployee_Employees_EmployeesID",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEmployee_EmployeesID",
                table: "CourseEmployee",
                column: "EmployeesID");
        }
    }
}
