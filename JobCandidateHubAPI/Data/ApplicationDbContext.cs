using JobCandidateHubAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobCandidateHubAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasIndex(c => c.Email).IsUnique();
                entity.Property(c => c.FirstName).IsRequired();
                entity.Property(c => c.LastName).IsRequired();
                entity.Property(c => c.Email).IsRequired();
                entity.Property(c => c.Comment).IsRequired();
            });
        }
    }
}
