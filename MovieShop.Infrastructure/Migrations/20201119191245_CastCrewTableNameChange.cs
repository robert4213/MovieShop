using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Infrastructure.Migrations
{
    public partial class CastCrewTableNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Crews",
                table: "Crews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Casts",
                table: "Casts");

            migrationBuilder.RenameTable(
                name: "Crews",
                newName: "Crew");

            migrationBuilder.RenameTable(
                name: "Casts",
                newName: "Cast");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crew",
                table: "Crew",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cast",
                table: "Cast",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Crew",
                table: "Crew");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cast",
                table: "Cast");

            migrationBuilder.RenameTable(
                name: "Crew",
                newName: "Crews");

            migrationBuilder.RenameTable(
                name: "Cast",
                newName: "Casts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crews",
                table: "Crews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Casts",
                table: "Casts",
                column: "Id");
        }
    }
}
