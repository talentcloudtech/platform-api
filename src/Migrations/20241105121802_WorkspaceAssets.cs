using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class WorkspaceAssets : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WorkspaceAssets",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                ProjectId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                TaskId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                FileUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                FileType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                FileSize = table.Column<long>(type: "bigint", nullable: false),
                Scope = table.Column<string>(type: "text", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkspaceAssets", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkspaceAssets_AspNetUsers_CreatedBy",
                    column: x => x.CreatedBy,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAssets_ProjectTasks_TaskId",
                    column: x => x.TaskId,
                    principalTable: "ProjectTasks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAssets_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAssets_Workspaces_WorkspaceId",
                    column: x => x.WorkspaceId,
                    principalTable: "Workspaces",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAssets_CreatedBy", table: "WorkspaceAssets", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAssets_ProjectId", table: "WorkspaceAssets", column: "ProjectId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAssets_TaskId", table: "WorkspaceAssets", column: "TaskId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAssets_WorkspaceId", table: "WorkspaceAssets", column: "WorkspaceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "WorkspaceAssets");
    }
}
