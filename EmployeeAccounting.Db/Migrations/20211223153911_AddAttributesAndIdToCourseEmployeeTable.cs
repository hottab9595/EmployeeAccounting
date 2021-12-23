using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeAccounting.Db.Migrations
{
    public partial class AddAttributesAndIdToCourseEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEmployees",
                table: "CourseEmployees");

            migrationBuilder.DeleteData(
                table: "CourseEmployees",
                keyColumns: new[] { "CourseId", "EmployeeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CourseEmployees",
                keyColumns: new[] { "CourseId", "EmployeeId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Departments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "CourseEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEmployees",
                table: "CourseEmployees",
                column: "ID");

            migrationBuilder.InsertData(
                table: "CourseEmployees",
                columns: new[] { "ID", "CourseId", "EmployeeId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "CourseEmployees",
                columns: new[] { "ID", "CourseId", "EmployeeId" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEmployees_CourseId",
                table: "CourseEmployees",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEmployees",
                table: "CourseEmployees");

            migrationBuilder.DropIndex(
                name: "IX_CourseEmployees_CourseId",
                table: "CourseEmployees");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "CourseEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEmployees",
                table: "CourseEmployees",
                columns: new[] { "CourseId", "EmployeeId" });
        }
    }
}
