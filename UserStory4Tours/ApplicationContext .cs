using Microsoft.EntityFrameworkCore;
using UserStory4Tours.models;

namespace UserStory4Tours
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Набор сущностей класса Tours
        /// </summary>
        public DbSet<Tours> ToursDB { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tours>().HasKey(u => u.Id);
        }

    }
}
