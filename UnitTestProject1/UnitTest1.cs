using AcmeCorpLander.Controllers;
using AcmeCorpLander.Data;
using AcmeCorpLander.Models;
using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            string vUnder18 = subRepo.ValidateSubmission(s1);
            string vOver18 = subRepo.ValidateSubmission(s2);

            Assert.IsFalse(under18);
            Assert.IsTrue(over18);
            Assert.AreEqual("Thank you for entering the contest, you will receive an email when the winner is drawn", vOver18);
            Assert.AreEqual("Must be over 18", vUnder18);
        }

        [TestMethod]
        public void ValidSerialNumberTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            Submission s1 = new Submission("Edvard", "edvard@hotmail.com", 91,/*invalid*/ 88888888);
            Submission s2 = new Submission("Martin", "martin@hotmail.com", 36,/*valid*/ 23328632);

            bool invalidSerial = subRepo.ValidateSerial(s1.SerialNum);
            bool validSerial = subRepo.ValidateSerial(s2.SerialNum);
            string vInvalid = subRepo.ValidateSubmission(s1);
            string vValid = subRepo.ValidateSubmission(s2);

            Assert.IsFalse(invalidSerial);
            Assert.IsTrue(validSerial);
            Assert.AreEqual("Thank you for entering the contest, you will receive an email when the winner is drawn", vValid);
            Assert.AreEqual("Invalid serial number", vInvalid);
        }

        [TestMethod]
        public void EntryLimitReachedTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);
            var controller = new SubmissionController(_db, subRepo);

            Submission submission = new Submission("Egon", "egon@hotmail.com", 44, 63307008);
            Submission submission2 = new Submission("Egon", "egon@hotmail.com", 44, 63307008);

            controller.Create(submission);
            controller.Create(submission2);

            int subCount = subRepo.GetSubmissions().Count();

            controller.Create(submission);
            string v = subRepo.ValidateSubmission(submission);

            Assert.AreEqual(2, subCount);
            Assert.AreEqual("Too many entries", v);
        }

        [TestMethod]
        public void ValidateSubmissionTest()
        {
            var _db = GetDatabaseContext();
            var subRepo = new SubmissionRepo(_db);

            Submission s1 = new Submission("Gunner", "gunner@hotmail.com", 44, 77118090);
            Submission s2 = new Submission("Janus", null, 67, 80303768);
            Submission s3 = new Submission(null, "hej@123.dk", 39, 90657863);

            string validSubmission = subRepo.ValidateSubmission(s1);
            string emailNull = subRepo.ValidateSubmission(s2);
            string nameNull = subRepo.ValidateSubmission(s3);

            Assert.AreEqual("Thank you for entering the contest, you will receive an email when the winner is drawn", validSubmission);
            Assert.AreEqual(null, emailNull);
            Assert.AreEqual(null, nameNull);
        }
    }
}
