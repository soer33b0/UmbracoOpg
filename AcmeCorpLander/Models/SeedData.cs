using AcmeCorpLander.Data;
using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
                if (context.Submission.Any())
                {
                    return;   // DB has been seeded
                }

                context.Submission.AddRange(
                    new Submission
                    {
                        FullName = "Rasmus Paludan",
                        Email = "rp@stramkurs.dk",
                        Age = 22,
                        SerialNum = 58079554
                    },
                    new Submission
                    {
                        FullName = "Kaj Boyesen",
                        Email = "abe@hotmail.com",
                        Age = 45,
                        SerialNum = 82514261
                    },
                    new Submission
                    {
                        FullName = "Søren Malling",
                        Email = "sm@hotmail.com",
                        Age = 55,
                        SerialNum = 38875640
                    },
                    new Submission
                    {
                        FullName = "Gordon Ramsey",
                        Email = "gordon@idiotsandwich.com",
                        Age = 50,
                        SerialNum = 46632564
                    },
                    new Submission
                    {
                        FullName = "Morgan Freeman",
                        Email = "MoFo@god.com",
                        Age = 100,
                        SerialNum = 93172768
                    },
                    new Submission
                    {
                        FullName = "Kanye West",
                        Email = "kanye4pres@2020.com",
                        Age = 48,
                        SerialNum = 88059526
                    },
                    new Submission
                    {
                        FullName = "Egon Olsen",
                        Email = "egon@danskebank.dk",
                        Age = 90,
                        SerialNum = 35581572
                    },
                    new Submission
                    {
                        FullName = "Casper Christensen",
                        Email = "cc@klovn.dk",
                        Age = 52,
                        SerialNum = 65996900
                    },
                    new Submission
                    {
                        FullName = "Ole Thestrup",
                        Email = "hvaskød@duen.me",
                        Age = 80,
                        SerialNum = 67800879
                    },
                    new Submission
                    {
                        FullName = "Kim Larsen",
                        Email = "kl@gasolin.dk",
                        Age = 63,
                        SerialNum = 74457705
                    },
                    new Submission
                    {
                        FullName = "Margrethe II",
                        Email = "majestæten@kongehuset.dk",
                        Age = 80,
                        SerialNum = 59395710
                    },
                    new Submission
                    {
                        FullName = "Mads Mikkelsen",
                        Email = "mm@casinoroyale.dk",
                        Age = 54,
                        SerialNum = 32427143
                    },
                    new Submission
                    {
                        FullName = "Nikolaj-Lie Kaas",
                        Email = "eigil@groenslagter.dk",
                        Age = 46,
                        SerialNum = 95676611
                    },
                    new Submission
                    {
                        FullName = "Peter Madsen",
                        Email = "pm@ubåd.dk",
                        Age = 49,
                        SerialNum = 10057238
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
