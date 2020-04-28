using AcmeCorpLander.Data;
using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorpLander.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AcmeDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AcmeDbContext>>()))
            {
                // Look for any movies.
                if (context.Customer.Any())
                {
                    return;   // DB has been seeded
                }

                context.Customer.AddRange(
                    new Customer
                    {
                        FirstName = "Casper",
                        LastName = "Christensen",
                        Email = "cc@klovn.dk",
                        Age = 51
                    },

                    new Customer
                    {
                        FirstName = "Frank",
                        LastName = "Hvam",
                        Email = "fh@klovn.dk",
                        Age = 49
                    },

                    new Customer
                    {
                        FirstName = "Mette",
                        LastName = "Frederiksen",
                        Email = "CEO@danmark.dk",
                        Age = 42
                    },

                    new Customer
                    {
                        FirstName = "Mick",
                        LastName = "Øgendahl",
                        Email = "mick@mickoegendahl.dk",
                        Age = 47
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
