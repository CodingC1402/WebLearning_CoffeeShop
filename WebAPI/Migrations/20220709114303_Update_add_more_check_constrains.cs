using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Update_add_more_check_constrains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_StartResign",
                table: "Employees");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_StartDOB",
                table: "Employees",
                sql: "`StartDate` > `DOB`");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_StartResign",
                table: "Employees",
                sql: "`ResignDate` IS NULL OR `StartDate` < `ResignDate`");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_StartDOB",
                table: "Employees");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_StartResign",
                table: "Employees");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StartResign",
                table: "Employees",
                sql: "`StartDate` < `ResignDate`");
        }
    }
}
