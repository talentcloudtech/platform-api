using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class StringArrayChanges : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Manually change the "Urls" column to text[] with casting
        migrationBuilder.Sql(
            @"ALTER TABLE ""Workspaces""
                  ALTER COLUMN ""Urls"" TYPE text[]
                  USING ""Urls""::text[];"
        );

        // Manually change the "RequiredSkills" column to text[] with casting
        migrationBuilder.Sql(
            @"ALTER TABLE ""MemberRequests""
                  ALTER COLUMN ""RequiredSkills"" TYPE text[]
                  USING ""RequiredSkills""::text[];"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Revert the "Urls" column back to text with casting
        migrationBuilder.Sql(
            @"ALTER TABLE ""Workspaces""
                  ALTER COLUMN ""Urls"" TYPE text
                  USING array_to_string(""Urls"", ',');"
        );

        // Revert the "RequiredSkills" column back to text with casting
        migrationBuilder.Sql(
            @"ALTER TABLE ""MemberRequests""
                  ALTER COLUMN ""RequiredSkills"" TYPE text
                  USING array_to_string(""RequiredSkills"", ',');"
        );
    }
}
