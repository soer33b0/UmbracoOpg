using AcmeCorpLander.Data;
using AcmeCorpLander.Models;
using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create([Bind("FullName,Email,Age,SerialNum,Wins")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                string v = _subRepo.ValidateSubmission(submission);
                if (v == null || v == "No entry" || v == "Invalid serial number" || v == "Too many entries")
                {
                    return RedirectToAction(nameof(Error));
                }

                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(submission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Age,SerialNum,Entries,Wins")] Submission submission)
        {
            if (id != submission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
        private bool SubmissionExists(int id)
        {
            return _context.Submission.Any(e => e.Id == id);
        }
    }
}
