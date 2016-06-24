using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;


namespace AlpineSkiHouse.Data.Migrations
{
    [DbContext(typeof(SkiCardContext))]
    partial class SkiCardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlpineSkiHouse.Models.SkiCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CardHolderBirthDate");

                    b.Property<string>("CardHolderFirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 70);

                    b.Property<string>("CardHolderLastName")
                        .HasAnnotation("MaxLength", 70);

                    b.Property<string>("CardHolderPhoneNumber");

                    b.Property<DateTime>("CreatedOn");

                    b.HasKey("Id");

                    b.ToTable("SkiCards");
                });
        }
    }
}
