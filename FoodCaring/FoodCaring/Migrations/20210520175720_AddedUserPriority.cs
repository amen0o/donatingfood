using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCaring.Migrations
{
    public partial class AddedUserPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22793bd5-72b9-47b7-b8df-f320b41513d1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aeaeba06-8f15-475c-ad63-33a485b443a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be4dc9f3-f122-4d27-bbc0-987d21a9774e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0256615-c316-400d-93a1-aab89325367b");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e18afc2e-77f0-44cc-829a-cff1e4e46315", "c0a94723-3ab8-4058-bcb8-b531332cd734", "Manager", "MANAGER" },
                    { "c4f2b9a0-1fb3-4e7a-b040-6881c98c9211", "d130375c-4355-4d01-ad3b-92e07f91ba50", "Administrator", "ADMINISTRATOR" },
                    { "bd2dd0a3-4022-4018-ad93-a267406925dc", "579d6223-e90e-4994-8aeb-e8c8ea65ae8a", "Donator", "DONATOR" },
                    { "e5790c3e-bf78-4c35-b74f-3710f1ba8b1b", "3a86248d-0a90-401b-994a-60cf519d3df7", "Defavorizat", "DEFAVORIZAT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2dd0a3-4022-4018-ad93-a267406925dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4f2b9a0-1fb3-4e7a-b040-6881c98c9211");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e18afc2e-77f0-44cc-829a-cff1e4e46315");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5790c3e-bf78-4c35-b74f-3710f1ba8b1b");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "be4dc9f3-f122-4d27-bbc0-987d21a9774e", "ec83d96b-8e9b-4e36-ab71-c5ec71ac8ba5", "Manager", "MANAGER" },
                    { "22793bd5-72b9-47b7-b8df-f320b41513d1", "08f25822-8065-4066-b13b-42f81cdd394a", "Administrator", "ADMINISTRATOR" },
                    { "e0256615-c316-400d-93a1-aab89325367b", "6b8acde5-770f-46bc-9c86-98d48d4a1847", "Donator", "DONATOR" },
                    { "aeaeba06-8f15-475c-ad63-33a485b443a3", "16358232-3c6a-4c97-b385-69e010d58b0f", "Defavorizat", "DEFAVORIZAT" }
                });
        }
    }
}
