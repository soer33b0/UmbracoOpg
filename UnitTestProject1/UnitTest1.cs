using AcmeCorpLander.Controllers;
using AcmeCorpLander.Data;
using AcmeCorpLander.Models;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        private AcmeDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AcmeDbContext>()
                .UseInMemoryDatabase(databaseName: "UnitTestDB")
                .Options;
            var _db = new AcmeDbContext(options);
            _db.Database.EnsureCreated();
            
            return _db;
        }

        [TestMethod]
        public void AgePolicyTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            Submission s1 = new Submission("Johan", "johan@hotmail.com", 16, 97504435);
            Submission s2 = new Submission("Markus", "markus@hotmail.com", 24, 14070055);

            string under18 = subRepo.ValidateSubmission(s1);
            string over18 = subRepo.ValidateSubmission(s2);

            Assert.AreEqual("No entry", under18);
            Assert.AreEqual("Thank you for entering the contest, you will receive an email when the winner is drawn", over18);
        }

        [TestMethod]
        public void ValidSerialNumberTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            using (_db)
            {
                Submission s1 = new Submission("Johan", "johan@hotmail.com", 16, 97504435);
                Submission s2 = new Submission("Markus", "markus@hotmail.com", 24, 14070055);


            }
        }
    }
}
