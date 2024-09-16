using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class SkillTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Languages",
                table: "MemberProfiles",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string[]>(
                name: "ExternalLinks",
                table: "MemberProfiles",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldNullable: true
            );

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Skills");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Languages",
                table: "MemberProfiles",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]"
            );

            migrationBuilder.AlterColumn<string[]>(
                name: "ExternalLinks",
                table: "MemberProfiles",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]"
            );
        }
    }
}
