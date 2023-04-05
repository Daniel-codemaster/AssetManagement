using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssetCategory_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCycle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ServiceCycle_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Station_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Mobile = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RightsId = table.Column<int>(type: "integer", nullable: true),
                    PreferredEmail = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TwoFactorAuthEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoginId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsMobileConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    SecurityStamp = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ActivationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockoutExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthRecoveryCodes = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    AuthenticatorKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Make = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    SerialNumber = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceCycleId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Asset_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "CategoryId_fk",
                        column: x => x.CategoryId,
                        principalTable: "AssetCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CreatorId_fk",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "ServiceCycleId_fk",
                        column: x => x.ServiceCycleId,
                        principalTable: "ServiceCycle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "StationId_fk",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssetAttribute_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "AssetId_fk",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Notification_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "AssetId_fk",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Service_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "AssetId_fk",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_CategoryId",
                table: "Asset",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_CreatorId",
                table: "Asset",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ServiceCycleId",
                table: "Asset",
                column: "ServiceCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_StationId",
                table: "Asset",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAttribute_AssetId",
                table: "AssetAttribute",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AssetId",
                table: "Notification",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_AssetId",
                table: "Service",
                column: "AssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAttribute");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "AssetCategory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ServiceCycle");

            migrationBuilder.DropTable(
                name: "Station");
        }
    }
}
