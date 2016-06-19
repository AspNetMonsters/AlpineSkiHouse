using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlpineSkiHouse.Data.Migrations
{
public partial class AddUserNameAndBirthYear : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "BirthYear",
            table: "AspNetUsers",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "AspNetUsers",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "AspNetUsers",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "BirthYear",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "LastName",
            table: "AspNetUsers");
    }
}
}
