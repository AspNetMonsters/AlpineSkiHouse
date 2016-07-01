using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlpineSkiHouse.Web.Migrations
{
    public partial class AddResortsAndLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"INSERT INTO Resorts(Name)
                    VALUES('Alpine Ski House'),
                           ('Nearby Resort'),
                           ('Competing Resort')");

            migrationBuilder.Sql(
                @"  DECLARE @AlpineSkiHouseResortId int
                    SELECT @AlpineSkiHouseResortId = Id FROM Resorts WHERE Name = 'Alpine Ski House'

                    INSERT INTO Locations(Name, Latitude, Longitude, Altitude, ResortId)
                    VALUES('Main Lift',   50.942981, -115.154111, 1534, @AlpineSkiHouseResortId),
                          ('Tow Rope',    50.942306, -115.153015, 1534, @AlpineSkiHouseResortId),
                          ('Upper Lift',  50.946147, -115.176919, 1742, @AlpineSkiHouseResortId),
                          ('Lower Lift',  50.945184, -115.147446, 1494, @AlpineSkiHouseResortId)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"DELETE Locations 
                  WHERE ResortId = 
                     (SELECT ResortId 
                      FROM Resorts 
                      WHERE Name = 'Alpine Ski House')");

            migrationBuilder.Sql(
                @"DELETE Resorts WHERE Name IN 
                    ('Alpine Ski House',
                     'Nearby Resort',
                     'Competing Resort')");
        }
    }
}
