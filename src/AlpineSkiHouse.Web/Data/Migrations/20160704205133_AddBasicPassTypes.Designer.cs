using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AlpineSkiHouse.Data;

namespace AlpineSkiHouse.Web.Migrations
{
    [DbContext(typeof(PassTypeContext))]
    [Migration("20160704205133_AddBasicPassTypes")]
    partial class AddBasicPassTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlpineSkiHouse.Models.PassType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.ToTable("PassTypes");
                });

            modelBuilder.Entity("AlpineSkiHouse.Models.PassTypeResort", b =>
                {
                    b.Property<int>("PassTypeId");

                    b.Property<int>("ResortId");

                    b.HasKey("PassTypeId", "ResortId");

                    b.HasIndex("PassTypeId");

                    b.ToTable("PassTypeResort");
                });

            modelBuilder.Entity("AlpineSkiHouse.Models.PassTypeResort", b =>
                {
                    b.HasOne("AlpineSkiHouse.Models.PassType")
                        .WithMany("PassTypeResorts")
                        .HasForeignKey("PassTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
