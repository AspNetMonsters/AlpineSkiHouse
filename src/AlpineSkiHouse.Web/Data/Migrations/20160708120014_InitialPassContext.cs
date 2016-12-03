using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlpineSkiHouse.Web.Migrations.Pass
{
    public partial class InitialPassContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    PassTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassActivations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PassId = table.Column<int>(nullable: false),
                    ScanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassActivations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassActivations_Passes_PassId",
                        column: x => x.PassId,
                        principalTable: "Passes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassActivations_Scans_ScanId",
                        column: x => x.ScanId,
                        principalTable: "Scans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassActivations_PassId",
                table: "PassActivations",
                column: "PassId");

            migrationBuilder.CreateIndex(
                name: "IX_PassActivations_ScanId",
                table: "PassActivations",
                column: "ScanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassActivations");

            migrationBuilder.DropTable(
                name: "Passes");

            migrationBuilder.DropTable(
                name: "Scans");
        }
    }
}
