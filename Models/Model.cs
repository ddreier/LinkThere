using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
                .HasIndex(l => l.Key)
                .IsUnique();
            modelBuilder.Entity<Link>()
                .Property(l => l.LinkUrl)
                .IsRequired();
            modelBuilder.Entity<Link>()
                .Property(l => l.ClickCount)
                .IsConcurrencyToken();
        }
    }

    public class Link
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Link Key is required")]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Link URL is required")]
        [Url(ErrorMessage = "The Link URL must be a valid URL")]
        [Display(Name = "URL")]
        public string LinkUrl { get; set; }

        public int ClickCount { get; set; }
    }
}
