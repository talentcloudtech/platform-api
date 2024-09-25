using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class RemoveCodeFromInvitation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Code", table: "WorkspaceMemberInvitations");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Code",
            table: "WorkspaceMemberInvitations",
            type: "character varying(12)",
            maxLength: 12,
            nullable: false,
            defaultValue: ""
        );
    }
}
