﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountMangement.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class PermissionIgnoreName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RolePermissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RolePermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
