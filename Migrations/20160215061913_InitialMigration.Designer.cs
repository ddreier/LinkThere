using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using LinkThere.Models;

namespace LinkThere.Migrations
{
    [DbContext(typeof(LinkThereContext))]
    [Migration("20160215061913_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
