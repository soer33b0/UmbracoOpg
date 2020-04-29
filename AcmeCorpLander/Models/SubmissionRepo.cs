using AcmeCorpLander.Data;
using ClassLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
            bool serialValid = ValidateSerial(submission.SerialNum);
            bool winner = IsWinner(submission.SerialNum);
            submission.Entries = UpdateEntries(submission);
            string message;

            if (submission.Entries < 2 && serialValid == true && winner == false)
            {
                submission.Entries++;
                message = "You win... Nothing";
            }
            else if (submission.Entries < 2 && serialValid == true && winner == true)
            {
                submission.Entries++;
                submission.Wins++;
                message = "Congratulations! You've won a splinterny Puch Maxi!";
            }
            else
            {
                message = "Sorry, closed for entry";
            }
            return message;
        }

        public int UpdateEntries(Submission submission)
        {
            List<Submission> allSubmissions = GetSubmissions();

            int entryCount = submission.Entries;

            foreach (Submission i in allSubmissions)
            {
                if (submission.SerialNum == i.SerialNum)
                {
                    entryCount += i.Entries;
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

        public bool IsWinner(int serial)
        {
            if (serial == 69029910)
            {
                return true;
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
    }
}
