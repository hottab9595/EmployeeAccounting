using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeAccounting.Db.Migrations
{
    public partial class AddCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                });

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

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "ID", "Duration", "Signature" },
                values: new object[] { 1, 6, ".NET" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "ID", "Duration", "Signature" },
                values: new object[] { 2, 6, "Java" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "ID", "Duration", "Signature" },
                values: new object[] { 3, 1, "SQL" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEmployee_EmployeesID",
                table: "CourseEmployee",
                column: "EmployeesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEmployee");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
