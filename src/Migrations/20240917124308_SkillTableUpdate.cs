using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class SkillTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AddColumn<string>(name: "Key", table: "Skills", type: "character varying(100)", maxLength: 100, nullable: true);

            migrationBuilder.AddColumn<string[]>(name: "Keywords", table: "Skills", type: "text[]", nullable: true);

            migrationBuilder.AddColumn<string>(name: "Path", table: "Skills", type: "character varying(500)", maxLength: 500, nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Key", table: "Skills");

            migrationBuilder.DropColumn(name: "Keywords", table: "Skills");

            migrationBuilder.DropColumn(name: "Path", table: "Skills");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100
            );
        }
    }
}
