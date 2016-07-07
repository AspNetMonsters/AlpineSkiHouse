using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlpineSkiHouse.Web.Migrations
{
    public partial class AddBasicPassTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"INSERT INTO PassTypes(Name, Description, ValidFrom, ValidTo)
      VALUES('Any Resort Day Pass (2017)', 
             'A single day pass that can be used at any of our resorts', 
             '04-11-2017', 
            '06-30-2017')");

            migrationBuilder.Sql(
@"INSERT INTO PassTypeResort(PassTypeId, ResortId)
    SELECT pt.Id, r.Id FROM PassTypes pt CROSS JOIN Resorts r ");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE PassTypes WHERE Name = 'Any Resort Day Pass (2017)'");
        }
    }
}
