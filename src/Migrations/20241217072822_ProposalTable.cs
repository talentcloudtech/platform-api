using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ProposalTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Proposals",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                TaskId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CostEstimation = table.Column<decimal>(type: "numeric", nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Proposals", x => x.Id);
                table.ForeignKey(
                    name: "FK_Proposals_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );

                table.ForeignKey(
                    name: "FK_Proposals_ProjectTasks_TaskId",
                    column: x => x.TaskId,
                    principalTable: "ProjectTasks",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateIndex(name: "IX_Proposals_TaskId", table: "Proposals", column: "TaskId");

        migrationBuilder.CreateIndex(name: "IX_Proposals_UserId", table: "Proposals", column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Proposals");
    }
}
