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
        
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var submissions = _context.Submission;

            int pageSize = 10;

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
            string v = _subRepo.ValidateSubmission(submission);
            if (v == null)
            {
                return RedirectToAction(nameof(Error));
            }
            
            if (ModelState.IsValid)
            {
                

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
    }
}
