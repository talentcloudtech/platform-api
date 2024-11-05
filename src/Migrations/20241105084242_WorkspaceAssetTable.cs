using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class WorkspaceAssetTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WorkspaceAsset",
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
                UserId = table.Column<string>(type: "character varying(25)", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkspaceAsset", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkspaceAsset_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAsset_ProjectTasks_TaskId",
                    column: x => x.TaskId,
                    principalTable: "ProjectTasks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAsset_Projects_ProjectId",
                    column: x => x.ProjectId,
                    principalTable: "Projects",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceAsset_Workspaces_WorkspaceId",
                    column: x => x.WorkspaceId,
                    principalTable: "Workspaces",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAsset_ProjectId", table: "WorkspaceAsset", column: "ProjectId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAsset_TaskId", table: "WorkspaceAsset", column: "TaskId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAsset_UserId", table: "WorkspaceAsset", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceAsset_WorkspaceId", table: "WorkspaceAsset", column: "WorkspaceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "WorkspaceAsset");
    }
}
