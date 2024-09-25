using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class CascadeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Projects_Workspaces_WorkspaceId", table: "Projects");

            migrationBuilder.DropForeignKey(name: "FK_ProjectTasks_Projects_ProjectId", table: "ProjectTasks");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceMembers_Workspaces_WorkspaceId", table: "WorkspaceMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                table: "WorkspaceMembers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Projects_Workspaces_WorkspaceId", table: "Projects");

            migrationBuilder.DropForeignKey(name: "FK_ProjectTasks_Projects_ProjectId", table: "ProjectTasks");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceMembers_Workspaces_WorkspaceId", table: "WorkspaceMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_Projects_ProjectId",
                table: "ProjectTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                table: "WorkspaceMembers",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }
    }
}
