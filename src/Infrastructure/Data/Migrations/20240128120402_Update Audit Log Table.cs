using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FruitZA.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "AuditActionLog");

            migrationBuilder.RenameColumn(
                name: "OldValue",
                table: "AuditActionLog",
                newName: "Value");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditActionLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AuditActionLog",
                newName: "OldValue");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AuditActionLog",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "AuditActionLog",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
