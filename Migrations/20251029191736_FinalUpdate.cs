using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FinalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "Books",
                newName: "IsAvailable");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Books",
                newName: "Available");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
