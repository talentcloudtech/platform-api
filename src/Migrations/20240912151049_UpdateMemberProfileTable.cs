using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMemberProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "IndustryExperiences", table: "MemberProfiles", newName: "ProjectInfos");

            migrationBuilder.AddColumn<string>(name: "Address", table: "MemberProfiles", type: "character varying(500)", maxLength: 500, nullable: true);

            migrationBuilder.AddColumn<string>(name: "Certifications", table: "MemberProfiles", type: "jsonb", nullable: true);

            migrationBuilder.AddColumn<string>(name: "Educations", table: "MemberProfiles", type: "jsonb", nullable: true);

            migrationBuilder.AddColumn<string>(name: "Experiences", table: "MemberProfiles", type: "jsonb", nullable: true);

            migrationBuilder.AddColumn<List<string>>(name: "Languages", table: "MemberProfiles", type: "text[]", nullable: true);

            migrationBuilder.AddColumn<string>(name: "Subtitle", table: "MemberProfiles", type: "character varying(200)", maxLength: 200, nullable: true);

            migrationBuilder.AddColumn<string>(name: "UserName", table: "MemberProfiles", type: "character varying(25)", maxLength: 25, nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Address", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "Certifications", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "Educations", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "Experiences", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "Languages", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "Subtitle", table: "MemberProfiles");

            migrationBuilder.DropColumn(name: "UserName", table: "MemberProfiles");

            migrationBuilder.RenameColumn(name: "ProjectInfos", table: "MemberProfiles", newName: "IndustryExperiences");
        }
    }
}
