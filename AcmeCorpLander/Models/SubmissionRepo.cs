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
            bool over18 = AgeCheck(submission.Age);
            bool repeatEntry = RepeatEntryCheck(submission.SerialNum);
            bool serialValid = ValidateSerial(submission.SerialNum);
            bool winner = IsWinner(submission.SerialNum);
            string message;

            if (over18 == true && repeatEntry == true && serialValid == true && winner == false)
            {
                submission.Entries++;
                message = "You win... Nothing";
            }
            else if (over18 == true && repeatEntry == true && serialValid == true && winner == true)
            {
                submission.Entries++;
                submission.Wins++;
                message = "Congratulations! You won the draw!";
            }
            else
            {
                message = "Sorry, closed for entry";
            }
            _db.SaveChanges();

            return message;
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

        public bool AgeCheck(int age)
        {
            if (age > 18)
            {
                return true;
            }
            return false;
        }

        public bool RepeatEntryCheck(int serial)
        {
            foreach (Submission s in _db.Submission.ToArray())
            {
                if (serial == s.SerialNum && s.Entries >= 2)
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

        public List<int> GetSerials()
        {
            List<int> validSerials = new List<int>();

            if (File.Exists(path))
            {  
                using (StreamReader file = new StreamReader(path))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        validSerials.Add(Convert.ToInt32(line));
                    }

                    file.Close();
                }
            }

            return validSerials;
        }
    }
}
