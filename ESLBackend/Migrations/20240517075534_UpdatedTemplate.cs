using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESLBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price1",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Price2",
                table: "Templates");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Templates",
                newName: "Upc");

            migrationBuilder.RenameColumn(
                name: "StoreNumber",
                table: "Templates",
                newName: "Version");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Templates",
                newName: "TemplateType");

            migrationBuilder.AlterColumn<string>(
                name: "ShopCode",
                table: "Templates",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Templates",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Templates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GoodsCode",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GoodsName",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HashCode",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Templates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedTime",
                table: "Templates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "GoodsCode",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "GoodsName",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Items",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTime",
                table: "Templates");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Templates",
                newName: "StoreNumber");

            migrationBuilder.RenameColumn(
                name: "Upc",
                table: "Templates",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "TemplateType",
                table: "Templates",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "ShopCode",
                table: "Templates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Templates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Price1",
                table: "Templates",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price2",
                table: "Templates",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
