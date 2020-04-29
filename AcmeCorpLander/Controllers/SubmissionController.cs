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
        
        // GET: Submission
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var submissions = _context.Submission;

            int pageSize = 10;

            return View(await PaginatedList<Submission>.CreateAsync(submissions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        public IActionResult Validate(Submission submission)
        {
            _subRepo.ValidateSubmission(submission);
            return View();
        }

        // GET: Submission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Submission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Email,Age,SerialNum")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                string v = "noooo";
                v = _subRepo.ValidateSubmission(submission);
                _context.Add(submission);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Validate));
            }

            return View(submission);
        }


        // GET: Submission/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submission
                .FirstOrDefaultAsync(m => m.Email == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var submission = await _context.Submission.FindAsync(id);
            _context.Submission.Remove(submission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(string id)
        {
            return _context.Submission.Any(e => e.Email == id);
        }
    }
}
