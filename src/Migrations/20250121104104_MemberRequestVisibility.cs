using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class MemberRequestVisibility : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(name: "Type", table: "MemberRequests", type: "text", nullable: false, defaultValue: "");

        migrationBuilder.AddColumn<string>(name: "Visibility", table: "MemberRequests", type: "text", nullable: false, defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Type", table: "MemberRequests");

        migrationBuilder.DropColumn(name: "Visibility", table: "MemberRequests");
    }
}
