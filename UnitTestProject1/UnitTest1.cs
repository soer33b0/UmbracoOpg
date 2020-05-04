using AcmeCorpLander.Controllers;
using AcmeCorpLander.Data;
using AcmeCorpLander.Models;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void AgeCheckTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            Submission s1 = new Submission("Johan", "johan@hotmail.com", 16, 97504435);
            Submission s2 = new Submission("Markus", "markus@hotmail.com", 24, 14070055);

            bool under18 = subRepo.Over18(s1.Age);
            bool over18 = subRepo.Over18(s2.Age);

            Assert.IsFalse(under18);
            Assert.IsTrue(over18);
        }

        [TestMethod]
        public void ValidSerialNumberTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            Submission s1 = new Submission("Edvard", "edvard@hotmail.com", 91, 88888888);
            Submission s2 = new Submission("Martin", "martin@hotmail.com", 36, 23328632);

            bool invalidSerial = subRepo.ValidateSerial(s1.SerialNum);
            bool validSerial = subRepo.ValidateSerial(s2.SerialNum);

            Assert.IsFalse(invalidSerial);
            Assert.IsTrue(validSerial);
        }

        [TestMethod]
        public void EntryLimitReachedTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);
            var controller = new SubmissionController(_db, subRepo);

            Submission submission = new Submission("Egon", "egon@hotmail.com", 44, 63307008);

            _db.Add(submission);
            _db.SaveChangesAsync();

            _db.Add(submission);
            _db.SaveChangesAsync();

            int subCount = subRepo.GetSubmissions().Count();

            controller.Create(submission);
            string v = subRepo.ValidateSubmission(submission);
            
            Assert.AreEqual(2, subCount);
            Assert.AreEqual("Too many entries", v);
        }
    }
}
