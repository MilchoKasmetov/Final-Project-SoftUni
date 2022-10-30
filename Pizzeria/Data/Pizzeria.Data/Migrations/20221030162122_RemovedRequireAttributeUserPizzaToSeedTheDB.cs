#nullable disable

namespace Pizzeria.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedRequireAttributeUserPizzaToSeedTheDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_AspNetUsers_AddedByUserId",
                table: "Pizzas");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Pizzas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_AspNetUsers_AddedByUserId",
                table: "Pizzas",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_AspNetUsers_AddedByUserId",
                table: "Pizzas");

            migrationBuilder.AlterColumn<string>(
                name: "AddedByUserId",
                table: "Pizzas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_AspNetUsers_AddedByUserId",
                table: "Pizzas",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
