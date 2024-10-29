using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ProjectTableUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(name: "Technologies", table: "Projects", type: "jsonb", nullable: true);

        migrationBuilder.AddColumn<string>(name: "Type", table: "Projects", type: "text", nullable: false, defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Technologies", table: "Projects");

        migrationBuilder.DropColumn(name: "Type", table: "Projects");
    }
}
