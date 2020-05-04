using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorpLander.Data
{
    public class AcmeDbContext : DbContext
    {
        public AcmeDbContext(DbContextOptions<AcmeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> Submission { get; set; }
    }
}
