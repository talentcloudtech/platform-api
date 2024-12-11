using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ProjectTaskType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(name: "IsPublic", table: "ProjectTasks", type: "boolean", nullable: false, defaultValue: false);

        migrationBuilder.AddColumn<string>(name: "Type", table: "ProjectTasks", type: "text", nullable: false, defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "IsPublic", table: "ProjectTasks");

        migrationBuilder.DropColumn(name: "Type", table: "ProjectTasks");
    }
}
