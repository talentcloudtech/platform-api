using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ProjectTaskOpenTaskStatus : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "IsPublished", table: "ProjectTasks");

        migrationBuilder.AddColumn<string>(name: "OpenTaskStatus", table: "ProjectTasks", type: "text", nullable: false, defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "OpenTaskStatus", table: "ProjectTasks");

        migrationBuilder.AddColumn<bool>(name: "IsPublished", table: "ProjectTasks", type: "boolean", nullable: false, defaultValue: false);
    }
}
