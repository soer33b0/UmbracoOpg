using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcmeCorpLander.Data;
using ClassLibrary;

namespace AcmeCorpLander.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly AcmeDbContext _context;

        public SubmissionsController(AcmeDbContext context)
        {
            _context = context;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Submission.ToListAsync());
        }


        // GET: Submissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,SerialNum")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(submission);
        }

        // GET: Submissions/Delete/5
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

        // POST: Submissions/Delete/5
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
