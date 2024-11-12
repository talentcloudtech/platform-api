using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;
/// <inheritdoc />
public partial class IsClosedAddedInTask : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsClosed",
            table: "ProjectTasks",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsClosed",
            table: "ProjectTasks");
    }
}
