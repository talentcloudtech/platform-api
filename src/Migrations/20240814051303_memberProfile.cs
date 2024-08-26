using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class memberProfile : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "MemberProfiles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                ShortBio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                ProfilePictureUrl = table.Column<string>(
                    type: "character varying(500)",
                    maxLength: 500,
                    nullable: false
                ),
                Timezone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                ExternalLinks = table.Column<string[]>(type: "text[]", nullable: true),
                ResumeFileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                IndustryExperiences = table.Column<string>(type: "jsonb", nullable: true),
                Skills = table.Column<string>(type: "jsonb", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemberProfiles", x => x.Id);
            }
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "MemberProfiles");
    }
}
