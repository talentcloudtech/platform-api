using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ContactEmailNotRequired : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "ContactEmail",
            table: "Workspaces",
            type: "character varying(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255
        );

        migrationBuilder.CreateIndex(name: "IX_WorkspaceMembers_UserId", table: "WorkspaceMembers", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_Projects_WorkspaceId", table: "Projects", column: "WorkspaceId");

        migrationBuilder.CreateIndex(name: "IX_ProjectMembers_UserId", table: "ProjectMembers", column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectMembers_AspNetUsers_UserId",
            table: "ProjectMembers",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
        );

        migrationBuilder.AddForeignKey(
            name: "FK_Projects_Workspaces_WorkspaceId",
            table: "Projects",
            column: "WorkspaceId",
            principalTable: "Workspaces",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
        );

        migrationBuilder.AddForeignKey(
            name: "FK_WorkspaceMembers_AspNetUsers_UserId",
            table: "WorkspaceMembers",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_ProjectMembers_AspNetUsers_UserId", table: "ProjectMembers");

        migrationBuilder.DropForeignKey(name: "FK_Projects_Workspaces_WorkspaceId", table: "Projects");

        migrationBuilder.DropForeignKey(name: "FK_WorkspaceMembers_AspNetUsers_UserId", table: "WorkspaceMembers");

        migrationBuilder.DropIndex(name: "IX_WorkspaceMembers_UserId", table: "WorkspaceMembers");

        migrationBuilder.DropIndex(name: "IX_Projects_WorkspaceId", table: "Projects");

        migrationBuilder.DropIndex(name: "IX_ProjectMembers_UserId", table: "ProjectMembers");

        migrationBuilder.AlterColumn<string>(
            name: "ContactEmail",
            table: "Workspaces",
            type: "character varying(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(255)",
            oldMaxLength: 255,
            oldNullable: true
        );
    }
}
