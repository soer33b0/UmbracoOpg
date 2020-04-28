using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;

namespace AcmeCorpLander.Data
{
    public class AcmeDbContext : DbContext
    {
        public AcmeDbContext(DbContextOptions<AcmeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Submission> Submission { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
