using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCaring.Migrations
{
    public partial class RestoreRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "acaa0b22-7a70-4008-aab3-1fc65a24014c", "ac2fd380-767b-47e4-aa25-fd50942d82ae", "Manager", "MANAGER" },
                    { "5f873a1e-862c-42c1-8bab-632920506cc9", "9116c3e3-4469-47a5-a4f9-cc80e9fe9bbe", "Administrator", "ADMINISTRATOR" },
                    { "b8fa82dd-1ded-4edc-a02b-74b9fe4996e5", "7f450ce2-3d29-40ab-8b1e-f5116f7f5fc4", "Donator", "DONATOR" },
                    { "0330de48-8bfe-43a0-a6c4-a55e0cac825e", "6e64b52b-62a7-4f5d-96f2-f271a08ab7ac", "Defavorizat", "DEFAVORIZAT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0330de48-8bfe-43a0-a6c4-a55e0cac825e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f873a1e-862c-42c1-8bab-632920506cc9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "acaa0b22-7a70-4008-aab3-1fc65a24014c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8fa82dd-1ded-4edc-a02b-74b9fe4996e5");
        }
    }
}
