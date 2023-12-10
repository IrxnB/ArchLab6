using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ArchLab6
{
    internal class Lab6DbContext : DbContext
    {
        internal DbSet<NewsTopic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public Lab6DbContext(DbContextOptions options) : base(options) {}
    }

    

    internal class NewsTopic
    {
        [Key]
        public String Id { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public int ViewsCount { get; set; }

        public string? ImageSource { get; set; }
    }
}
