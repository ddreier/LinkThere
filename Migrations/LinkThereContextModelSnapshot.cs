using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LinkThere.Models;

namespace LinkThere.Migrations
{
    [DbContext(typeof(LinkThereContext))]
    partial class LinkThereContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

            modelBuilder.Entity("LinkThere.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClickCount")
                        .IsConcurrencyToken();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("LinkUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Links");
                });
        }
    }
}
