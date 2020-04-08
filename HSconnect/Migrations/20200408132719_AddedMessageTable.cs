using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Migrations
{
    public partial class AddedMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "adeeced8-f551-48d3-b2d8-3690ba7f684f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb38878e-b041-4e4d-8157-295eef47ff8c");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFromID = table.Column<string>(nullable: true),
                    UserToId = table.Column<string>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "36a8f961-ab5d-4a34-930d-e9c193fed417", "c4bd7e62-560d-44b1-b84b-b498b92c5f91", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f12efeac-df68-4b53-a60b-ed98c601565f", "f51f6280-d3c4-401e-be18-cef6ff8f0e7e", "Provider", "PROVIDER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36a8f961-ab5d-4a34-930d-e9c193fed417");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f12efeac-df68-4b53-a60b-ed98c601565f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb38878e-b041-4e4d-8157-295eef47ff8c", "5996b71c-0f22-4e66-b6d6-7a5f7c0f92d3", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "adeeced8-f551-48d3-b2d8-3690ba7f684f", "773594d3-0066-4546-843a-0a7f25d69b97", "Provider", "PROVIDER" });
        }
    }
}
