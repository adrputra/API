using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "EMPLOYEE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EMPLOYEE",
                table: "EMPLOYEE",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EMPLOYEE",
                table: "EMPLOYEE");

            migrationBuilder.RenameTable(
                name: "EMPLOYEE",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");
        }
    }
}
