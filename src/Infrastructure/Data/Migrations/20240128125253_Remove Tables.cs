using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitZA.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuditActionLog_AuditAction_AuditActionId",
                table: "AuditActionLog");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditActionLog_AuditItem_AuditItemId",
                table: "AuditActionLog");

            migrationBuilder.DropTable(
                name: "AuditAction");

            migrationBuilder.DropTable(
                name: "AuditItem");

            migrationBuilder.DropIndex(
                name: "IX_AuditActionLog_AuditActionId",
                table: "AuditActionLog");

            migrationBuilder.DropIndex(
                name: "IX_AuditActionLog_AuditItemId",
                table: "AuditActionLog");

            migrationBuilder.DropColumn(
                name: "AuditActionId",
                table: "AuditActionLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuditActionId",
                table: "AuditActionLog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeatureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditActionLog_AuditActionId",
                table: "AuditActionLog",
                column: "AuditActionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditActionLog_AuditItemId",
                table: "AuditActionLog",
                column: "AuditItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditActionLog_AuditAction_AuditActionId",
                table: "AuditActionLog",
                column: "AuditActionId",
                principalTable: "AuditAction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuditActionLog_AuditItem_AuditItemId",
                table: "AuditActionLog",
                column: "AuditItemId",
                principalTable: "AuditItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
