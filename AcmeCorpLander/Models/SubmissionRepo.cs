using AcmeCorpLander.Data;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AcmeCorpLander.Models
{
    public class SubmissionRepo
    {
        private readonly AcmeDbContext _db;

        public SubmissionRepo(AcmeDbContext db)
        {
            _db = db;
        }

        readonly string path = @".\serialNumbers.txt";

        public string ValidateSubmission(Submission submission)
        {
            if (submission.FullName != null && submission.Email != null)
            {
                bool over18 = Over18(submission.Age);
                bool serialValid = ValidateSerial(submission.SerialNum);
                int entries = EntryCheck(submission.SerialNum);

                if (over18 == false)
                {
                    return "Must be over 18";
                }

                else if (serialValid == false)
                {
                    return "Invalid serial number";
                }

                else if (entries >= 2)
                {
                    return "Too many entries";
                }

                else if (over18 == true && entries < 2 && serialValid == true)
                {
                    return "Thank you for entering the contest, you will receive an email when the winner is drawn";
                }
            }

            return null;
        }

        public bool Over18(int age)
        {
            if (age >= 18)
            {
                return true;
            }

            return false;
        }

        public int EntryCheck(int serial)
        {
            List<Submission> allSubmissions = GetSubmissions();

            int entryCount = 0;

            foreach (Submission s in allSubmissions)
            {
                if (serial == s.SerialNum)
                {
                    entryCount++;
                }
            }

            return entryCount;
        }

        public bool ValidateSerial(int serial)
        {
            List<int> validSerials = GetSerials();

            foreach (int s in validSerials)
            {
                if (serial == s)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Submission> GetSubmissions()
        {
            return _db.Submission.ToList();
        }

        public List<int> GetSerials()
        {


            List<int> validSerials = new List<int>();

            if (File.Exists(path))
            {
                using StreamReader file = new StreamReader(path);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    validSerials.Add(Convert.ToInt32(line));
                }

                file.Close();
            }

            return validSerials;
        }

        public Submission DrawWinner()
        {
            var random = new Random();
            List<Submission> allSubmissions = GetSubmissions();
            int index = random.Next(allSubmissions.Count);
            Submission sub = allSubmissions[index];

            sub.Wins++;
            _db.Update(sub);
            _db.SaveChangesAsync();


            return sub;
        }
    }
}
