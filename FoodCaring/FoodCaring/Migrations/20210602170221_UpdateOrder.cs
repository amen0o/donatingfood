using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCaring.Migrations
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TargetUserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a87a86b8-43e4-4f13-929a-ec55fc1e06f0", "4cffe7c3-f658-4708-ac98-7bcaae97424c", "Manager", "MANAGER" },
                    { "a61041db-f4df-4221-a82c-01d63eddef41", "7ce27d61-362a-4e26-9af2-4bc1629aa0a1", "Administrator", "ADMINISTRATOR" },
                    { "a3230165-a99c-4656-8492-fc251b2a15cc", "fcfa3600-1da1-4a62-80cf-79f64f21ab89", "Donator", "DONATOR" },
                    { "a1afdf85-01c9-43d7-a722-224da170d0be", "2a4ba3f4-04af-40be-bf65-faba812ae194", "Defavorizat", "DEFAVORIZAT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TargetUserId",
                table: "Orders",
                column: "TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_TargetUserId",
                table: "Orders",
                column: "TargetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_TargetUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TargetUserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1afdf85-01c9-43d7-a722-224da170d0be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3230165-a99c-4656-8492-fc251b2a15cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a61041db-f4df-4221-a82c-01d63eddef41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a87a86b8-43e4-4f13-929a-ec55fc1e06f0");

            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TargetUserId",
                table: "Orders");

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
    }
}
