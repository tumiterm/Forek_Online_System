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
using AspNetCoreHero.ToastNotification.Abstractions;
using System.Text.Encodings.Web;

namespace ForekRequisition.Controllers
{
    public class ApplicantOfferController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IWebHostEnvironment _webHostEnvironment;
        public INotyfService _notfy { get; }
        public ApplicantOfferController(ApplicationDbContext context,INotyfService notfy, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;

            _notfy = notfy;

            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
              return _context.ApplicantOffer != null ? 

                          View(await _context.ApplicantOffer.ToListAsync()) :

                          Problem("Entity set 'ApplicationDbContext.ApplicantOffer'  is null.");
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ApplicantOffer == null)
            {
                return NotFound();
            }

            var applicantOffer = await _context.ApplicantOffer
                .FirstOrDefaultAsync(m => m.Id == id);

            if (applicantOffer == null)
            {
                return NotFound();
            }

            return View(applicantOffer);
        }
        public IActionResult OnSignOffer (Guid applicantId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnSignOffer([Bind("Id,ApplicantId,HasAcceptedPlumbingOffer,HasAcceptedWeldingOffer,HasSignedOffer,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApplicantOffer applicantOffer, Guid applicantId)
        {

            var getApplicantInfo = await _context.AppResults.Where(m => m.Id == applicantId).FirstOrDefaultAsync();
            var status = await _context.ApplicantOffer.Where(x => x.ApplicantId == applicantId).FirstOrDefaultAsync();

            if (applicantOffer.HasAcceptedPlumbingOffer && applicantOffer.HasAcceptedWeldingOffer)
            {
                //_notfy.Error("Error: You cannot choose both programmes");
                _notfy.Error("Error: You cannot make both choices");
            }

             string applicantchoice = "";

                if (getApplicantInfo == null)
                {
                    return NotFound();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        applicantOffer.Id = Helper.GenerateGuid();

                        applicantOffer.IsActive = true;

                        applicantOffer.CreatedOn = DateTime.Now;

                        applicantOffer.CreatedBy = $"{getApplicantInfo.Name} {getApplicantInfo.LastName}";

                        applicantOffer.ApplicantId = applicantId;


                        if (applicantOffer.HasAcceptedPlumbingOffer)
                        {
                            applicantOffer.HasSignedOffer = true;

                            applicantchoice = "Available";

                            _notfy.Success("Offer Placed as: Available");

                        }
                        else
                        {
                            if (applicantOffer.HasAcceptedWeldingOffer)
                            {
                                applicantOffer.HasSignedOffer = true;

                                // applicantchoice = "Welder";
                                applicantchoice = "Unavailabe";

                                _notfy.Success("Offer as NOT available");

                            }
                        }

                        if (applicantOffer.HasSignedOffer)
                        {
                          //  _context.Add(applicantOffer);

                           // await _context.SaveChangesAsync();

                            Helper.OnSendOfferMail(applicantchoice, "lmhlongo@forekinstitute.co.za", "Availability Status",
                            Helper.OfferMessage(getApplicantInfo.Name, getApplicantInfo.LastName, getApplicantInfo.IdNumber, applicantchoice));
                            return RedirectToAction(nameof(Notification));
                        }
                        else
                        {
                            _notfy.Warning("Warning: You have not signed the offer");
                        }

                    }
                }
                return View(applicantOffer);
         
       
           
        }

        public async Task<IActionResult> OnModifyOffer(Guid? id)
        {
            if (id == null || _context.ApplicantOffer == null)
            {
                return NotFound();
            }

            var applicantOffer = await _context.ApplicantOffer.FindAsync(id);
            if (applicantOffer == null)
            {
                return NotFound();
            }
            return View(applicantOffer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnModifyOffer(Guid id, [Bind("Id,ApplicantId,HasAcceptedPlumbingOffer,HasAcceptedWeldingOffer,HasSignedOffer,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApplicantOffer applicantOffer)
        {
            if (id != applicantOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantOfferExists(applicantOffer.Id))
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
            return View(applicantOffer);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ApplicantOffer == null)
            {
                return NotFound();
            }

            var applicantOffer = await _context.ApplicantOffer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantOffer == null)
            {
                return NotFound();
            }

            return View(applicantOffer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ApplicantOffer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplicantOffer'  is null.");
            }
            var applicantOffer = await _context.ApplicantOffer.FindAsync(id);
            if (applicantOffer != null)
            {
                _context.ApplicantOffer.Remove(applicantOffer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Notification()
        {
            return View();
        }

        private bool ApplicantOfferExists(Guid id)
        {
          return (_context.ApplicantOffer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
