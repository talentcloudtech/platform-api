using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations
{
    /// <inheritdoc />
    public partial class WorkspaceRelatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Name", table: "Projects");

            migrationBuilder.AlterColumn<string>(name: "Status", table: "Projects", type: "text", nullable: false, oldClrType: typeof(int), oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AddColumn<decimal>(name: "Budget", table: "Projects", type: "numeric(18,2)", nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Projects",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(name: "EndDate", table: "Projects", type: "timestamp with time zone", nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Projects",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(name: "UpdatedBy", table: "Projects", type: "character varying(25)", maxLength: 25, nullable: true);

            migrationBuilder.AddColumn<DateTime>(name: "UpdatedDate", table: "Projects", type: "timestamp with time zone", nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkspaceId",
                table: "Projects",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    BillingType = table.Column<string>(type: "text", nullable: false),
                    AvailablePerDay = table.Column<int>(type: "integer", nullable: false),
                    BillingRate = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    SenderId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProjectId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    MessageType = table.Column<string>(type: "text", nullable: false),
                    ReplyAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecipientCount = table.Column<int>(type: "integer", nullable: false),
                    TotalReplyCount = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "ProjectMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProjectId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    AllocatedMinMinutePerDay = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProjectId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    Estimate = table.Column<int>(type: "integer", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Urls = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Logo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "MessageRecipients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    MessageId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    RecipientId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsMuted = table.Column<bool>(type: "boolean", nullable: false),
                    MutedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageRecipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageRecipients_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "MessageReplies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    OriginalMessageId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    SenderId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Body = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReplies_Messages_OriginalMessageId",
                        column: x => x.OriginalMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ProjectTaskAssignments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    TaskId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    TimeSpent = table.Column<int>(type: "integer", nullable: false),
                    AssignedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTaskAssignments_ProjectTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "WorkspaceMembers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "MessageAttachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    MessageId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ReplyId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ProjectId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageAttachments_MessageReplies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "MessageReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey(
                        name: "FK_MessageAttachments_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_MessageAttachments_MessageId", table: "MessageAttachments", column: "MessageId");

            migrationBuilder.CreateIndex(name: "IX_MessageAttachments_ReplyId", table: "MessageAttachments", column: "ReplyId");

            migrationBuilder.CreateIndex(name: "IX_MessageRecipients_MessageId", table: "MessageRecipients", column: "MessageId");

            migrationBuilder.CreateIndex(name: "IX_MessageReplies_OriginalMessageId", table: "MessageReplies", column: "OriginalMessageId");

            migrationBuilder.CreateIndex(name: "IX_ProjectMembers_ProjectId", table: "ProjectMembers", column: "ProjectId");

            migrationBuilder.CreateIndex(name: "IX_ProjectTaskAssignments_TaskId", table: "ProjectTaskAssignments", column: "TaskId");

            migrationBuilder.CreateIndex(name: "IX_ProjectTasks_ProjectId", table: "ProjectTasks", column: "ProjectId");

            migrationBuilder.CreateIndex(name: "IX_WorkspaceMembers_WorkspaceId", table: "WorkspaceMembers", column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Contracts");

            migrationBuilder.DropTable(name: "MessageAttachments");

            migrationBuilder.DropTable(name: "MessageRecipients");

            migrationBuilder.DropTable(name: "ProjectMembers");

            migrationBuilder.DropTable(name: "ProjectTaskAssignments");

            migrationBuilder.DropTable(name: "WorkspaceMembers");

            migrationBuilder.DropTable(name: "MessageReplies");

            migrationBuilder.DropTable(name: "ProjectTasks");

            migrationBuilder.DropTable(name: "Workspaces");

            migrationBuilder.DropTable(name: "Messages");

            migrationBuilder.DropColumn(name: "Budget", table: "Projects");

            migrationBuilder.DropColumn(name: "CreatedBy", table: "Projects");

            migrationBuilder.DropColumn(name: "CreatedDate", table: "Projects");

            migrationBuilder.DropColumn(name: "EndDate", table: "Projects");

            migrationBuilder.DropColumn(name: "StartDate", table: "Projects");

            migrationBuilder.DropColumn(name: "Title", table: "Projects");

            migrationBuilder.DropColumn(name: "UpdatedBy", table: "Projects");

            migrationBuilder.DropColumn(name: "UpdatedDate", table: "Projects");

            migrationBuilder.DropColumn(name: "WorkspaceId", table: "Projects");

            migrationBuilder.AlterColumn<int>(name: "Status", table: "Projects", type: "integer", nullable: false, oldClrType: typeof(string), oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000,
                oldNullable: true
            );

            migrationBuilder.AddColumn<string>(name: "Name", table: "Projects", type: "text", nullable: false, defaultValue: "");
        }
    }
}
