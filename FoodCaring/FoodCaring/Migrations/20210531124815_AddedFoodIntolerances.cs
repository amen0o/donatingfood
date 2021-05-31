using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodCaring.Migrations
{
    public partial class AddedFoodIntolerances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "FoodIntolerances",
                columns: table => new
                {
                    FoodIntoleranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIntolerances", x => x.FoodIntoleranceId);
                });

            migrationBuilder.CreateTable(
                name: "UserFoodIntolerance",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FoodIntoleranceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFoodIntolerance", x => new { x.UserId, x.FoodIntoleranceId });
                    table.ForeignKey(
                        name: "FK_UserFoodIntolerance_FoodIntolerances_FoodIntoleranceId",
                        column: x => x.FoodIntoleranceId,
                        principalTable: "FoodIntolerances",
                        principalColumn: "FoodIntoleranceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFoodIntolerance_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FoodIntolerances",
                columns: new[] { "FoodIntoleranceId", "Name" },
                values: new object[,]
                {
                    { 1, "Gluten" },
                    { 2, "Lactose" },
                    { 3, "Caffeine" },
                    { 4, "Sulfites" },
                    { 5, "Fructose" },
                    { 6, "Salicylates" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFoodIntolerance_FoodIntoleranceId",
                table: "UserFoodIntolerance",
                column: "FoodIntoleranceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFoodIntolerance");

            migrationBuilder.DropTable(
                name: "FoodIntolerances");

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
        }
    }
}
