using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Update_Change_Constrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "Point",
                table: "Customers",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderDetails_Count_Range",
                table: "OrderDetails",
                sql: "`Count` >= 1 AND `Count` <= 100");

            migrationBuilder.AddCheckConstraint(
                name: "CK_StartResign",
                table: "Employees",
                sql: "`StartDate` < `ResignDate`");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderDetails_Count_Range",
                table: "OrderDetails");

            migrationBuilder.DropCheckConstraint(
                name: "CK_StartResign",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Point",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");
        }
    }
}
