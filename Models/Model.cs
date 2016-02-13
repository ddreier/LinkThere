using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace LinkThere.Models
{
    public class LinkThereContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>()
                .Property(l => l.Key)
                .IsRequired();
            modelBuilder.Entity<Link>()
                .Property(l => l.LinkUrl)
                .IsRequired();
        }
    }

    public class Link
    {
        public string Key { get; set; }
        public string LinkUrl { get; set; }
    }
}
