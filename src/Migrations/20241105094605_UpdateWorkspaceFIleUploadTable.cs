using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkspaceFIleUploadTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAsset_AspNetUsers_UserId", table: "WorkspaceAsset");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAsset_ProjectTasks_TaskId", table: "WorkspaceAsset");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAsset_Projects_ProjectId", table: "WorkspaceAsset");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAsset_Workspaces_WorkspaceId", table: "WorkspaceAsset");

            migrationBuilder.DropPrimaryKey(name: "PK_WorkspaceAsset", table: "WorkspaceAsset");

            migrationBuilder.RenameTable(name: "WorkspaceAsset", newName: "WorkspaceAssets");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAsset_WorkspaceId", table: "WorkspaceAssets", newName: "IX_WorkspaceAssets_WorkspaceId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAsset_UserId", table: "WorkspaceAssets", newName: "IX_WorkspaceAssets_UserId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAsset_TaskId", table: "WorkspaceAssets", newName: "IX_WorkspaceAssets_TaskId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAsset_ProjectId", table: "WorkspaceAssets", newName: "IX_WorkspaceAssets_ProjectId");

            migrationBuilder.AddPrimaryKey(name: "PK_WorkspaceAssets", table: "WorkspaceAssets", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAssets_AspNetUsers_UserId",
                table: "WorkspaceAssets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAssets_ProjectTasks_TaskId",
                table: "WorkspaceAssets",
                column: "TaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAssets_Projects_ProjectId",
                table: "WorkspaceAssets",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAssets_Workspaces_WorkspaceId",
                table: "WorkspaceAssets",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAssets_AspNetUsers_UserId", table: "WorkspaceAssets");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAssets_ProjectTasks_TaskId", table: "WorkspaceAssets");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAssets_Projects_ProjectId", table: "WorkspaceAssets");

            migrationBuilder.DropForeignKey(name: "FK_WorkspaceAssets_Workspaces_WorkspaceId", table: "WorkspaceAssets");

            migrationBuilder.DropPrimaryKey(name: "PK_WorkspaceAssets", table: "WorkspaceAssets");

            migrationBuilder.RenameTable(name: "WorkspaceAssets", newName: "WorkspaceAsset");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAssets_WorkspaceId", table: "WorkspaceAsset", newName: "IX_WorkspaceAsset_WorkspaceId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAssets_UserId", table: "WorkspaceAsset", newName: "IX_WorkspaceAsset_UserId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAssets_TaskId", table: "WorkspaceAsset", newName: "IX_WorkspaceAsset_TaskId");

            migrationBuilder.RenameIndex(name: "IX_WorkspaceAssets_ProjectId", table: "WorkspaceAsset", newName: "IX_WorkspaceAsset_ProjectId");

            migrationBuilder.AddPrimaryKey(name: "PK_WorkspaceAsset", table: "WorkspaceAsset", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAsset_AspNetUsers_UserId",
                table: "WorkspaceAsset",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAsset_ProjectTasks_TaskId",
                table: "WorkspaceAsset",
                column: "TaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAsset_Projects_ProjectId",
                table: "WorkspaceAsset",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceAsset_Workspaces_WorkspaceId",
                table: "WorkspaceAsset",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }
    }
}
