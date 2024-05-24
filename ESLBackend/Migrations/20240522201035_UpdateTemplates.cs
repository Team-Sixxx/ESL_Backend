using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESLBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateContent",
                table: "Templates");

            migrationBuilder.AddColumn<string>(
                name: "Template",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Template",
                table: "Templates");

            migrationBuilder.AddColumn<string>(
                name: "TemplateContent",
                table: "Templates",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
