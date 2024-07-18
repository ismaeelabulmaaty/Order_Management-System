using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oredr_Manag.Repository.Data.Migrations
{
    public partial class InitClint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClintSecret",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClintSecret",
                table: "Orders");
        }
    }
}
