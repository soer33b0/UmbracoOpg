using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeCorpLander.Data;
using ClassLibrary;
using AcmeCorpLander.Models;

namespace AcmeCorpLander.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly AcmeDbContext _context;
        private readonly SubmissionRepo _subRepo;

        public SubmissionController(AcmeDbContext context, SubmissionRepo sr)
        {
            _context = context;
            _subRepo = sr;
        }

        public IActionResult Start()
        {
            return View();
        }

        public async Task<IActionResult> Index(
            string sortOrder, 
            string currentFilter,
            string searchString, 
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SerialSortParm"] = sortOrder == "SerialNum" ? "serial_desc" : "SerialNum";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var submissions = from s in _context.Submission
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                submissions = submissions.Where(s => s.FullName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    submissions = submissions.OrderByDescending(s => s.FullName);
                    break;
                case "SerialNum":
                    submissions = submissions.OrderBy(s => s.SerialNum);
                    break;
                case "serial_desc":
                    submissions = submissions.OrderByDescending(s => s.SerialNum);
                    break;
                default:
                    submissions = submissions.OrderBy(s => s.FullName);
                    break;
            }
            int pageSize = 8;

            return View(await PaginatedList<Submission>.CreateAsync(submissions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Email,Age,SerialNum,Entries,Wins")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                string v = _subRepo.ValidateSubmission(submission);
                if (v == "No entry")
                {
                    return RedirectToAction(nameof(Error));
                }

                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(submission);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult DrawWinner()
        {
            Submission submission = _subRepo.DrawWinner();
            ViewBag.wName = submission.FullName;
            ViewBag.wEmail = submission.Email;
            ViewBag.wSerial = submission.SerialNum;

            return View(submission);
        }
    }
}
