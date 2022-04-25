using Microsoft.EntityFrameworkCore;
using SensitiveWordsAPI.Entities;

namespace SensitiveWordsAPI
{
    public class SensitiveWordDbContext : DbContext
    {
        public SensitiveWordDbContext(DbContextOptions<SensitiveWordDbContext> context): base(context)
        {}
        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
