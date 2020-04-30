﻿using AcmeCorpLander.Data;
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
            int entries = EntryCheck(submission.SerialNum);

            if (entries < 2 && serialValid == true)
            {
                return "Thanks for trying";
            }

            return null;
        }

        public int EntryCheck(int serial)
        {
            List<Submission> allSubmissions = GetSubmissions();

            int entryCount = 0;

            foreach (Submission i in allSubmissions)
            {
                if (serial == i.SerialNum)
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

            return allSubmissions[index];
        }
    }
}
