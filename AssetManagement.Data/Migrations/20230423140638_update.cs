using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OfficeId",
                table: "Asset",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Office_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "StationId_fkey",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_OfficeId",
                table: "Asset",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Office_StationId",
                table: "Office",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "OfficeId_fk",
                table: "Asset",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "OfficeId_fk",
                table: "Asset");

            migrationBuilder.DropTable(
                name: "Office");

            migrationBuilder.DropIndex(
                name: "IX_Asset_OfficeId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Asset");
        }
    }
}
