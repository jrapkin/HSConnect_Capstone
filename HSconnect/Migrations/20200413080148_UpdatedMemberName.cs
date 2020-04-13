using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Migrations
{
    public partial class UpdatedMemberName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Members",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a8f961-ab5d-4a34-930d-e9c193fed417",
                column: "ConcurrencyStamp",
                value: "57fc15f1-3b9e-489d-88eb-7344a9bff32b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12efeac-df68-4b53-a60b-ed98c601565f",
                column: "ConcurrencyStamp",
                value: "9cc0ee3a-7012-42a6-a442-7c62eb38c4cf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a8f961-ab5d-4a34-930d-e9c193fed417",
                column: "ConcurrencyStamp",
                value: "50a5846d-86c5-45ae-ad4f-9cf062d4f3a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12efeac-df68-4b53-a60b-ed98c601565f",
                column: "ConcurrencyStamp",
                value: "01206ecc-2248-4cd3-b1a2-079bcea02076");
        }
    }
}
