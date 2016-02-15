using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace LinkThere.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClickCount = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    LinkUrl = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Link_Key",
                table: "Link",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Link");
        }
    }
}
