using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class IsDefaultColAddedInWorkspace : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(name: "IsDefault", table: "Workspaces", type: "boolean", nullable: false, defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "IsDefault", table: "Workspaces");
    }
}
