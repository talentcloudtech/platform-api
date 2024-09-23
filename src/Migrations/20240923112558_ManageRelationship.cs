using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ManageRelationship : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_ProjectMembers_AspNetUsers_UserId", table: "ProjectMembers");

        migrationBuilder.DropForeignKey(name: "FK_WorkspaceMembers_AspNetUsers_UserId", table: "WorkspaceMembers");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "ProjectTaskAssignments",
            type: "character varying(25)",
            maxLength: 25,
            nullable: false,
            defaultValue: ""
        );

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDate",
            table: "ProjectTaskAssignments",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
        );

        migrationBuilder.AddColumn<string>(name: "UpdatedBy", table: "ProjectTaskAssignments", type: "character varying(25)", maxLength: 25, nullable: true);

        migrationBuilder.AddColumn<DateTime>(name: "UpdatedDate", table: "ProjectTaskAssignments", type: "timestamp with time zone", nullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "UpdatedBy",
            table: "Contracts",
            type: "character varying(25)",
            maxLength: 25,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true
        );

        migrationBuilder.AlterColumn<string>(
            name: "CreatedBy",
            table: "Contracts",
            type: "character varying(25)",
            maxLength: 25,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "text"
        );

        migrationBuilder.CreateIndex(name: "IX_Workspaces_CreatedBy", table: "Workspaces", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_ProjectTasks_CreatedBy", table: "ProjectTasks", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_ProjectTasks_WorkspaceId", table: "ProjectTasks", column: "WorkspaceId");

        migrationBuilder.CreateIndex(name: "IX_ProjectTaskAssignments_UserId", table: "ProjectTaskAssignments", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_Projects_CreatedBy", table: "Projects", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_Contracts_UserId", table: "Contracts", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_Contracts_WorkspaceId", table: "Contracts", column: "WorkspaceId");

        migrationBuilder.AddForeignKey(
            name: "FK_Contracts_AspNetUsers_UserId",
            table: "Contracts",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
        );

        migrationBuilder.AddForeignKey(
            name: "FK_Contracts_Workspaces_WorkspaceId",
            table: "Contracts",
            column: "WorkspaceId",
            principalTable: "Workspaces",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
        );

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectMembers_AspNetUsers_UserId",
            table: "ProjectMembers",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_Projects_AspNetUsers_CreatedBy",
            table: "Projects",
            column: "CreatedBy",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectTaskAssignments_AspNetUsers_UserId",
            table: "ProjectTaskAssignments",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectTasks_AspNetUsers_CreatedBy",
            table: "ProjectTasks",
            column: "CreatedBy",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectTasks_Workspaces_WorkspaceId",
            table: "ProjectTasks",
            column: "WorkspaceId",
            principalTable: "Workspaces",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_WorkspaceMembers_AspNetUsers_UserId",
            table: "WorkspaceMembers",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            name: "FK_Workspaces_AspNetUsers_CreatedBy",
            table: "Workspaces",
            column: "CreatedBy",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_Contracts_AspNetUsers_UserId", table: "Contracts");

        migrationBuilder.DropForeignKey(name: "FK_Contracts_Workspaces_WorkspaceId", table: "Contracts");

        migrationBuilder.DropForeignKey(name: "FK_ProjectMembers_AspNetUsers_UserId", table: "ProjectMembers");

        migrationBuilder.DropForeignKey(name: "FK_Projects_AspNetUsers_CreatedBy", table: "Projects");

        migrationBuilder.DropForeignKey(name: "FK_ProjectTaskAssignments_AspNetUsers_UserId", table: "ProjectTaskAssignments");

        migrationBuilder.DropForeignKey(name: "FK_ProjectTasks_AspNetUsers_CreatedBy", table: "ProjectTasks");

        migrationBuilder.DropForeignKey(name: "FK_ProjectTasks_Workspaces_WorkspaceId", table: "ProjectTasks");

        migrationBuilder.DropForeignKey(name: "FK_WorkspaceMembers_AspNetUsers_UserId", table: "WorkspaceMembers");

        migrationBuilder.DropForeignKey(name: "FK_Workspaces_AspNetUsers_CreatedBy", table: "Workspaces");

        migrationBuilder.DropIndex(name: "IX_Workspaces_CreatedBy", table: "Workspaces");

        migrationBuilder.DropIndex(name: "IX_ProjectTasks_CreatedBy", table: "ProjectTasks");

        migrationBuilder.DropIndex(name: "IX_ProjectTasks_WorkspaceId", table: "ProjectTasks");

        migrationBuilder.DropIndex(name: "IX_ProjectTaskAssignments_UserId", table: "ProjectTaskAssignments");

        migrationBuilder.DropIndex(name: "IX_Projects_CreatedBy", table: "Projects");

        migrationBuilder.DropIndex(name: "IX_Contracts_UserId", table: "Contracts");

        migrationBuilder.DropIndex(name: "IX_Contracts_WorkspaceId", table: "Contracts");

        migrationBuilder.DropColumn(name: "CreatedBy", table: "ProjectTaskAssignments");

        migrationBuilder.DropColumn(name: "CreatedDate", table: "ProjectTaskAssignments");

        migrationBuilder.DropColumn(name: "UpdatedBy", table: "ProjectTaskAssignments");

        migrationBuilder.DropColumn(name: "UpdatedDate", table: "ProjectTaskAssignments");

        migrationBuilder.AlterColumn<string>(
            name: "UpdatedBy",
            table: "Contracts",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(25)",
            oldMaxLength: 25,
            oldNullable: true
        );

        migrationBuilder.AlterColumn<string>(
            name: "CreatedBy",
            table: "Contracts",
            type: "text",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(25)",
            oldMaxLength: 25
        );

        migrationBuilder.AddForeignKey(
            name: "FK_ProjectMembers_AspNetUsers_UserId",
            table: "ProjectMembers",
            column: "UserId",
            principalTable: "AspNetUsers",
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
}
