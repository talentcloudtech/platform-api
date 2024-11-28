using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class UserNameMaxSizeIncrease : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "MemberProfiles",
            type: "character varying(32)",
            maxLength: 32,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(25)",
            oldMaxLength: 25,
            oldNullable: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "UserName",
            table: "MemberProfiles",
            type: "character varying(25)",
            maxLength: 25,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(32)",
            oldMaxLength: 32,
            oldNullable: true
        );
    }
}
