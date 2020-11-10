using Foundant.Model.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foundant.ImageStore.Web.Model
{
    public class ImageStoreDbContext : DbContext
    {
        public ImageStoreDbContext(DbContextOptions<ImageStoreDbContext> options)
            : base(options) { }

        public DbSet<AlbumDAO> Albums { get; set; }
        public DbSet<ImageDAO> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageDAO>().Property(b => b._Tags).HasColumnName("Tags");
            modelBuilder.Entity<ImageDAO>().Ignore(c => c.Tags);
        }
    }
}
