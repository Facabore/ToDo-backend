using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo_backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tasktypes",
                columns: new[] { "id", "color_hex", "created_by", "name" },
                values: new object[] { 1, "#CCCCCC", new Guid("00000000-0000-0000-0000-000000000000"), "Uncategorized" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tasktypes",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
