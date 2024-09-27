using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class GigApplicationInvitationTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "GigApplicantInvitations",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                MemberRequestId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                InvitedUserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GigApplicantInvitations", x => x.Id);
                table.ForeignKey(
                    name: "FK_GigApplicantInvitations_AspNetUsers_CreatedBy",
                    column: x => x.CreatedBy,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_GigApplicantInvitations_AspNetUsers_InvitedUserId",
                    column: x => x.InvitedUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_GigApplicantInvitations_MemberRequests_MemberRequestId",
                    column: x => x.MemberRequestId,
                    principalTable: "MemberRequests",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(name: "IX_GigApplicantInvitations_CreatedBy", table: "GigApplicantInvitations", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_GigApplicantInvitations_InvitedUserId", table: "GigApplicantInvitations", column: "InvitedUserId");

        migrationBuilder.CreateIndex(name: "IX_GigApplicantInvitations_MemberRequestId", table: "GigApplicantInvitations", column: "MemberRequestId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "GigApplicantInvitations");
    }
}
