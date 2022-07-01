#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForekRequisition.Data;
using ForekRequisition.Models;
using ForekRequisition.Models.Helpers;
using System.Dynamic;

namespace ForekRequisition.Controllers
{
    public class ApplicantStatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Guid _applicantId;

        public ApplicantStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        public async Task<IActionResult> Index(Guid ApplicantId)
        {
            var applicationDbContext = _context.ApprenticeStatus.Include(a => a.Applicant).Where(x => x.ApplicantId == ApplicantId);
            return View(await applicationDbContext.ToListAsync());
        }

      
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStatus = await _context.ApprenticeStatus
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantStatus == null)
            {
                return NotFound();
            }

            return View(applicantStatus);
        }

        public async Task<IActionResult> ModifyApplicantStatus(Guid id)
        {
            //_applicantId = id;
            //ViewData["ApplicantId"] = id;

            var records = await _context.ApprenticeStatus.Where(m => m.ApplicantId == id).ToListAsync();

            dynamic statusModel = new ExpandoObject();

            statusModel.Status = records;

            return View(statusModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyApplicantStatus([Bind("Id,ApplicationStatus,Description,ApplicantId,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApplicantStatus applicantStatus,Guid id)
        {

            if (ModelState.IsValid)
            {
                applicantStatus.ApplicantId = id;
                applicantStatus.Id = Helper.GenerateGuid();

                applicantStatus.IsActive = true;
                applicantStatus.CreatedOn = DateTime.Now;
                applicantStatus.CreatedBy = Helper.loggedInUser;

                _context.Add(applicantStatus);
                await _context.SaveChangesAsync();

                return RedirectToAction("ModifyApplicantStatus", "ApplicantStatus", new { @id = id });

            }


            return RedirectToAction("Index", "ApplicantStatus", new { @ApplicantId = id });
        }


        public async Task<IActionResult> Edit(Guid? appId)
        {
            if (appId == null)
            {
                return NotFound();
            }

            var applicantStatus = await _context.ApprenticeStatus.Where(x => x.ApplicantId== appId).FirstOrDefaultAsync();  

            if (applicantStatus == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "Id", "Id", applicantStatus.ApplicantId);
            return View(applicantStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ApplicationStatus,Description,ApplicantId,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApplicantStatus applicantStatus)
        {
            if (id != applicantStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantStatusExists(applicantStatus.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.Applicants, "Id", "Id", applicantStatus.ApplicantId);
            return RedirectToAction("Index", "ApplicantStatus", new { @ApplicantId = id });
        }

        // GET: ApplicantStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantStatus = await _context.ApprenticeStatus
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantStatus == null)
            {
                return NotFound();
            }

            return View(applicantStatus);
        }

        // POST: ApplicantStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantStatus = await _context.ApprenticeStatus.FindAsync(id);
            _context.ApprenticeStatus.Remove(applicantStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantStatusExists(Guid id)
        {
            return _context.ApprenticeStatus.Any(e => e.Id == id);
        }

       
    }
}
