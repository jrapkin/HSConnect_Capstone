using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HSconnect.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Demographics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyFriendly = table.Column<bool>(nullable: true),
                    LowIncomeThreshold = table.Column<int>(nullable: true),
                    IsAgeSensitive = table.Column<bool>(nullable: false),
                    MemberIncome = table.Column<int>(nullable: true),
                    MemberAge = table.Column<int>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demographics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialWorkers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagedCareOrganizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedCareOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedCareOrganizations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesOffered",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    DemographicId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesOffered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_Demographics_DemographicId",
                        column: x => x.DemographicId,
                        principalTable: "Demographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesOffered_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partnerships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(nullable: true),
                    ManagedCareOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partnerships_ManagedCareOrganizations_ManagedCareOrganizationId",
                        column: x => x.ManagedCareOrganizationId,
                        principalTable: "ManagedCareOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partnerships_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Charts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceIsActive = table.Column<bool>(nullable: false),
                    ReferralAccepted = table.Column<bool>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    SocialWorkerId = table.Column<int>(nullable: true),
                    ServiceOfferedId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charts_ServicesOffered_ServiceOfferedId",
                        column: x => x.ServiceOfferedId,
                        principalTable: "ServicesOffered",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Charts_SocialWorkers_SocialWorkerId",
                        column: x => x.SocialWorkerId,
                        principalTable: "SocialWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    IsActiveMember = table.Column<bool>(nullable: false),
                    AddressId = table.Column<int>(nullable: true),
                    ChartId = table.Column<int>(nullable: true),
                    DemographicId = table.Column<int>(nullable: true),
                    ManagedCareOrganizationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Charts_ChartId",
                        column: x => x.ChartId,
                        principalTable: "Charts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_Demographics_DemographicId",
                        column: x => x.DemographicId,
                        principalTable: "Demographics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_ManagedCareOrganizations_ManagedCareOrganizationId",
                        column: x => x.ManagedCareOrganizationId,
                        principalTable: "ManagedCareOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1271e133-2d25-4f58-936d-213489764d3c", "a5a9765c-5f7a-4299-8c06-9198ad7218bc", "Social Worker", "SOCIALWORKER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e8bdf3d1-ed5b-4723-9f76-fc2d3bb039b6", "a29b9031-b298-49c1-9004-40da0b09fc01", "Provider", "PROVIDER" });

            migrationBuilder.CreateIndex(
                name: "IX_Charts_ServiceOfferedId",
                table: "Charts",
                column: "ServiceOfferedId");

            migrationBuilder.CreateIndex(
                name: "IX_Charts_SocialWorkerId",
                table: "Charts",
                column: "SocialWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedCareOrganizations_AddressId",
                table: "ManagedCareOrganizations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AddressId",
                table: "Members",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ChartId",
                table: "Members",
                column: "ChartId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_DemographicId",
                table: "Members",
                column: "DemographicId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_ManagedCareOrganizationId",
                table: "Members",
                column: "ManagedCareOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnerships_ManagedCareOrganizationId",
                table: "Partnerships",
                column: "ManagedCareOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Partnerships_ProviderId",
                table: "Partnerships",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_IdentityUserId",
                table: "Providers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_AddressId",
                table: "ServicesOffered",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_CategoryId",
                table: "ServicesOffered",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_DemographicId",
                table: "ServicesOffered",
                column: "DemographicId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_ProviderId",
                table: "ServicesOffered",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOffered_ServiceId",
                table: "ServicesOffered",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialWorkers_IdentityUserId",
                table: "SocialWorkers",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Partnerships");

            migrationBuilder.DropTable(
                name: "Charts");

            migrationBuilder.DropTable(
                name: "ManagedCareOrganizations");

            migrationBuilder.DropTable(
                name: "ServicesOffered");

            migrationBuilder.DropTable(
                name: "SocialWorkers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Demographics");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1271e133-2d25-4f58-936d-213489764d3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8bdf3d1-ed5b-4723-9f76-fc2d3bb039b6");
        }
    }
}
