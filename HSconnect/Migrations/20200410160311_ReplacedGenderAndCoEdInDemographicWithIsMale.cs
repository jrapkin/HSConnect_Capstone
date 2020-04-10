using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Migrations
{
    public partial class ReplacedGenderAndCoEdInDemographicWithIsMale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Demographics");

            migrationBuilder.DropColumn(
                name: "IsCoEd",
                table: "Demographics");

            migrationBuilder.AddColumn<bool>(
                name: "IsMale",
                table: "Demographics",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a8f961-ab5d-4a34-930d-e9c193fed417",
                column: "ConcurrencyStamp",
                value: "f0990ffb-88a2-4a09-a6dc-f2dbc5946217");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12efeac-df68-4b53-a60b-ed98c601565f",
                column: "ConcurrencyStamp",
                value: "5d2e591d-31e1-4cd9-95e3-d15fedae7fce");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMale",
                table: "Demographics");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Demographics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCoEd",
                table: "Demographics",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a8f961-ab5d-4a34-930d-e9c193fed417",
                column: "ConcurrencyStamp",
                value: "15a4da8e-0227-47a0-8c42-ab2e4da008d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12efeac-df68-4b53-a60b-ed98c601565f",
                column: "ConcurrencyStamp",
                value: "8a5ac507-c88e-4fdc-9ceb-ba8dffd00b93");
        }
    }
}
