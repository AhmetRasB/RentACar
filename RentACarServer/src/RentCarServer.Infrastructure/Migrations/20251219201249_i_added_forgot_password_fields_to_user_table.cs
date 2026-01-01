using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class i_added_forgot_password_fields_to_user_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ForgotPasswordCreatedAt_Value",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ForgotPasswordId_Value",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForgotPasswordCompleted_Value",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgotPasswordCreatedAt_Value",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ForgotPasswordId_Value",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsForgotPasswordCompleted_Value",
                table: "Users");
        }
    }
}
