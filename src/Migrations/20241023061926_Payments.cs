using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class Payments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(name: "CurrentBalance", table: "Workspaces", type: "numeric", nullable: false, defaultValue: 0m);

        migrationBuilder.CreateTable(
            name: "MemberTransactions",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UserId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                PaymentMethod = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                TransactionId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                PayerDetails = table.Column<string>(type: "jsonb", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MemberTransactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_MemberTransactions_AspNetUsers_CreatedBy",
                    column: x => x.CreatedBy,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_MemberTransactions_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "WorkspaceTransactions",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                WorkspaceId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                PaymentMethod = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                TransactionId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                PayeeDetails = table.Column<string>(type: "jsonb", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkspaceTransactions", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkspaceTransactions_AspNetUsers_CreatedBy",
                    column: x => x.CreatedBy,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
                table.ForeignKey(
                    name: "FK_WorkspaceTransactions_Workspaces_WorkspaceId",
                    column: x => x.WorkspaceId,
                    principalTable: "Workspaces",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict
                );
            }
        );

        migrationBuilder.CreateIndex(name: "IX_MemberTransactions_CreatedBy", table: "MemberTransactions", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_MemberTransactions_UserId", table: "MemberTransactions", column: "UserId");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceTransactions_CreatedBy", table: "WorkspaceTransactions", column: "CreatedBy");

        migrationBuilder.CreateIndex(name: "IX_WorkspaceTransactions_WorkspaceId", table: "WorkspaceTransactions", column: "WorkspaceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "MemberTransactions");

        migrationBuilder.DropTable(name: "WorkspaceTransactions");

        migrationBuilder.DropColumn(name: "CurrentBalance", table: "Workspaces");
    }
}
