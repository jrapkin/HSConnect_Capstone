using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9de6e148-822b-4ebb-9194-7ae8c78af9f4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac73005a-2d85-478e-953c-b12675a235be");

            migrationBuilder.AddColumn<bool>(
                name: "SmokingIsAllowed",
                table: "Demographics",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f51492c-baed-421e-8659-f7de61dca06f", "7568be1f-95ec-4453-8cdb-451a4d249185", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0ffb2055-d9f5-4e48-b798-52c1f544e1ab", "6a4dcf8e-9024-4b77-8135-f77fcf47c8bd", "Provider", "PROVIDER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ffb2055-d9f5-4e48-b798-52c1f544e1ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f51492c-baed-421e-8659-f7de61dca06f");

            migrationBuilder.DropColumn(
                name: "SmokingIsAllowed",
                table: "Demographics");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac73005a-2d85-478e-953c-b12675a235be", "a3eb0b92-f94c-4b63-8cd1-dc131c092ab3", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9de6e148-822b-4ebb-9194-7ae8c78af9f4", "9b35d02e-c27b-49da-b992-80a419e3bb08", "Provider", "PROVIDER" });
        }
    }
}
