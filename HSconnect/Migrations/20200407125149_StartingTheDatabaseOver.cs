using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Migrations
{
    public partial class StartingTheDatabaseOver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "227a8888-1d61-4850-8ecb-618fe5df3734");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bb30e45-2976-4cfe-93fe-a33d904eca97");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb38878e-b041-4e4d-8157-295eef47ff8c", "5996b71c-0f22-4e66-b6d6-7a5f7c0f92d3", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "adeeced8-f551-48d3-b2d8-3690ba7f684f", "773594d3-0066-4546-843a-0a7f25d69b97", "Provider", "PROVIDER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adeeced8-f551-48d3-b2d8-3690ba7f684f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb38878e-b041-4e4d-8157-295eef47ff8c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bb30e45-2976-4cfe-93fe-a33d904eca97", "e3f9af44-000f-42e8-9550-bec72cdb1f4b", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "227a8888-1d61-4850-8ecb-618fe5df3734", "d5148efa-4728-4092-a959-f96365f9c669", "Provider", "PROVIDER" });
        }
    }
}
