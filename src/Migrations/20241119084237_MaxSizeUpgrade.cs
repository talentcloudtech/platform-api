using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class MaxSizeUpgrade : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "RemoteJobs",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "ProjectTasks",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Projects",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Body",
            table: "Messages",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000);

        migrationBuilder.AlterColumn<string>(
            name: "Body",
            table: "MessageReplies",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "MemberRequests",
            type: "character varying(100000)",
            maxLength: 100000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(4000)",
            oldMaxLength: 4000,
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "RemoteJobs",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "ProjectTasks",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "Projects",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Body",
            table: "Messages",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000);

        migrationBuilder.AlterColumn<string>(
            name: "Body",
            table: "MessageReplies",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            table: "MemberRequests",
            type: "character varying(4000)",
            maxLength: 4000,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(100000)",
            oldMaxLength: 100000,
            oldNullable: true);
    }
}
