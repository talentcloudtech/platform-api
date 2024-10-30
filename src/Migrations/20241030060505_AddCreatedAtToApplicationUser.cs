using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class AddCreatedAtToApplicationUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(name: "CreatedAt", table: "AspNetUsers", type: "timestamp with time zone", nullable: false, defaultValue: "NOW()");

        // Update existing users with today's date - fixed PostgreSQL syntax
        migrationBuilder.Sql("UPDATE \"AspNetUsers\" SET \"CreatedAt\" = NOW() WHERE \"CreatedAt\" IS NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "CreatedAt", table: "AspNetUsers");
    }
}
