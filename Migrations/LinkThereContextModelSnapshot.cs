using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using LinkThere.Models;

namespace LinkThere.Migrations
{
    [DbContext(typeof(LinkThereContext))]
    partial class LinkThereContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

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
                });
        }
    }
}
