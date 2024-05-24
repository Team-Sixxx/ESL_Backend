using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESLBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTemplates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Templates_TemplatesId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Upcs_Templates_TemplatesId",
                table: "Upcs");

            migrationBuilder.DropIndex(
                name: "IX_Upcs_TemplatesId",
                table: "Upcs");

            migrationBuilder.DropIndex(
                name: "IX_Items_TemplatesId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "TemplatesId",
                table: "Upcs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TemplatesId1",
                table: "Upcs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Templates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "templateId",
                table: "MeetingRooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplatesId",
                table: "Items",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TemplatesId1",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Upcs_TemplatesId1",
                table: "Upcs",
                column: "TemplatesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TemplatesId1",
                table: "Items",
                column: "TemplatesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Templates_TemplatesId1",
                table: "Items",
                column: "TemplatesId1",
                principalTable: "Templates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Upcs_Templates_TemplatesId1",
                table: "Upcs",
                column: "TemplatesId1",
                principalTable: "Templates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Templates_TemplatesId1",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Upcs_Templates_TemplatesId1",
                table: "Upcs");

            migrationBuilder.DropIndex(
                name: "IX_Upcs_TemplatesId1",
                table: "Upcs");

            migrationBuilder.DropIndex(
                name: "IX_Items_TemplatesId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TemplatesId1",
                table: "Upcs");

            migrationBuilder.DropColumn(
                name: "TemplatesId1",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "TemplatesId",
                table: "Upcs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Templates",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "templateId",
                table: "MeetingRooms",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplatesId",
                table: "Items",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Upcs_TemplatesId",
                table: "Upcs",
                column: "TemplatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TemplatesId",
                table: "Items",
                column: "TemplatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Templates_TemplatesId",
                table: "Items",
                column: "TemplatesId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Upcs_Templates_TemplatesId",
                table: "Upcs",
                column: "TemplatesId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
