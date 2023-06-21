using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TableStorage.Migrations
{
    public partial class update_database_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnProperties_Properties_PropertyId1",
                table: "ColumnProperties");

            migrationBuilder.DropIndex(
                name: "IX_ColumnProperties_PropertyId1",
                table: "ColumnProperties");

            migrationBuilder.DropColumn(
                name: "PropertyId1",
                table: "ColumnProperties");

            migrationBuilder.DropPrimaryKey(
               name: "PK_Properties",
               table: "Properties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Properties");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Properties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnProperties_PropertyId",
                table: "ColumnProperties",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnProperties_Properties_PropertyId",
                table: "ColumnProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColumnProperties_Properties_PropertyId",
                table: "ColumnProperties");

            migrationBuilder.DropIndex(
                name: "IX_ColumnProperties_PropertyId",
                table: "ColumnProperties");

            migrationBuilder.DropPrimaryKey(
              name: "PK_Properties",
              table: "Properties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Properties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                "Id");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId1",
                table: "ColumnProperties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColumnProperties_PropertyId1",
                table: "ColumnProperties",
                column: "PropertyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ColumnProperties_Properties_PropertyId1",
                table: "ColumnProperties",
                column: "PropertyId1",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
