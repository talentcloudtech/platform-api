using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class MemberRequestTableUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Convert existing data from text[] to jsonb
        migrationBuilder.Sql(
            @"
            ALTER TABLE ""MemberRequests""
            ALTER COLUMN ""RequiredSkills"" TYPE jsonb
            USING COALESCE(to_jsonb(""RequiredSkills""), '[]'::jsonb)
        "
        );

        // Update the column definition
        migrationBuilder.AlterColumn<string>(
            name: "RequiredSkills",
            table: "MemberRequests",
            type: "jsonb",
            nullable: true,
            oldClrType: typeof(string[]),
            oldType: "text[]"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Convert data back from jsonb to text[]
        migrationBuilder.Sql(
            @"
            ALTER TABLE ""MemberRequests""
            ALTER COLUMN ""RequiredSkills"" TYPE text[]
            USING ARRAY(SELECT jsonb_array_elements_text(""RequiredSkills""))
        "
        );

        // Update the column definition
        migrationBuilder.AlterColumn<string[]>(
            name: "RequiredSkills",
            table: "MemberRequests",
            type: "text[]",
            nullable: false,
            defaultValue: new string[0],
            oldClrType: typeof(string),
            oldType: "jsonb",
            oldNullable: true
        );
    }
}
