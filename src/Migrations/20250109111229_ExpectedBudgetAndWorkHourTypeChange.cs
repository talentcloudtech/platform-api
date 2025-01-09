using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.Migrations;

/// <inheritdoc />
public partial class ExpectedBudgetAndWorkHourTypeChange : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "MinutesPerDay", table: "MemberRequests");

        migrationBuilder.AlterColumn<string>(
            name: "ExpectedBudget",
            table: "MemberRequests",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)"
        );

        migrationBuilder.AddColumn<string>(
            name: "WorkHour",
            table: "MemberRequests",
            type: "character varying(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: ""
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "WorkHour", table: "MemberRequests");

        migrationBuilder.AlterColumn<decimal>(
            name: "ExpectedBudget",
            table: "MemberRequests",
            type: "numeric(10,2)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200
        );

        migrationBuilder.AddColumn<int>(name: "MinutesPerDay", table: "MemberRequests", type: "integer", nullable: false, defaultValue: 0);
    }
}
