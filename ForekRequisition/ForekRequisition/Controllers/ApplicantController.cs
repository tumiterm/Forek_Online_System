#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApprenticeApplications.Models;
using ForekRequisition.Data;
using ApprenticeApplications.Models.Enums;
using ForekRequisition.Models.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using ForekRequisition.Models.ViewModel;

namespace ForekRequisition.Controllers
{

    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Guid ApplicantId;

        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notfy { get; }

        public ApplicantController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notfy)
        {
            _context = context;

            this._hostEnvironment = hostEnvironment;

            _notfy = notfy;
        }

        public async Task<IActionResult> OnGetApplicants()
        {
            var getApplicant = await _context.Applicants.ToListAsync();

            return View(getApplicant);
        }

        public async Task<IActionResult> Index()
        {
            var applicants = new ApplicantsViewModel
            {
                QualifiedApplicants = await _context.AppResults.

                Where(m => m.IsApproved == eApproval.Approved).

                ToArrayAsync(),

                NotQualified = await _context.AppResults.

                Where(m => m.IsApproved == eApproval.NotApproved).

                ToArrayAsync(),
            };

            return View(applicants);
        }

        [Authorize]
        public async Task<IActionResult> SearchApplicant(string userId)
        {
            var getApplicant = await _context.Applicants.Where(m => m.IDNumber == userId).FirstOrDefaultAsync();

            if (getApplicant == null)
            {

                return RedirectToAction("UserNotFound", "Applicant");

            }
            else
            {
                if (getApplicant.HasAcceptedOffer)
                {
                    getApplicant.HasAcceptedOffer = true;
                }

                ViewData["Name"] = getApplicant.Name;

                ViewData["LName"] = getApplicant.LastName;

                ViewData["Ref"] = getApplicant.ReferenceNumber;

                ViewData["RSAID"] = getApplicant.IDNumber;

                ViewData["Course"] = getApplicant.Trade;

                ViewData["UserId"] = getApplicant.Id;

            }
            return View(getApplicant);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                         .FirstOrDefaultAsync(m => m.Id == id);

            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        public IActionResult OnSetApplicantResponse()
        {
            
            return RedirectToAction("");
        }



        public IActionResult ApplicantRegister()
        {

            _notfy.Information("Applications Closes on 24 May 2022", 3);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplicantRegister(Applicant applicant)
        {

            if (applicant.DisabilityAtt != null)
            {
                DisabilityFileHelper(applicant);
            }

            applicant.ReferenceNumber = $"FIT" + DateTime.Now.Year + Helper.RandomStringGenerator(4);

            if (!ApplicantExists(applicant.IDNumber))
            {
                if (!DoesEmailExists(applicant.Email))
                {

                    if (ModelState.IsValid)
                    {
                        applicant.IsActive = true;

                        applicant.Id = Helper.GenerateGuid();

                        ViewData["Name"] = applicant.Name;

                        ViewData["LName"] = applicant.LastName;

                        ViewData["Id"] = applicant.Id;

                        ViewBag.Namez = applicant.Name;

                        applicant.CreatedBy = Helper.loggedInUser;

                        applicant.CreatedOn = DateTime.Now;

                        applicant.IsActive = true;

                        await _context.AddAsync(applicant);

                        await _context.SaveChangesAsync();

                        _notfy.Success("Information Successfully Saved");

                        _notfy.Information("Next step is to Add Education Details");

                        return RedirectToAction("AddEducationDetails", "Education", new { @ApplicantId = applicant.Id });
                    }
                    else
                    {
                        _notfy.Error($"Error: Oops, Something went wrong!");

                    }
                }
                else
                {
                    _notfy.Error($"Error: Email already exists!");

                }

            }
            else
            {
                _notfy.Error($"Error: Applicant already exists!");
            }

            return View(applicant);
        }

        [Authorize(Roles = "SystemAdministrator")]
        public async Task<IActionResult> UpdateApplicantDetails(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateApplicantDetails(Guid id, [Bind("Id,ProgrammeType,Trade,Name,LastName,Province,Initials,IDNumber,Race,Gender,Disability,DisabilityAttachment,Municipality,AddressIsSameAsPostal,AddressLine1,City,Code,HomeTel,Cellphone,ReferenceNumber,Email,AlternativeContacPers,AlternativeNum,ProspectiveEmployer,IsCurrentlyEmployed,HasSignedTerms,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            var userEduDetails = await _context.Education.Where(x => x.ApplicantId == id).FirstOrDefaultAsync();


            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(applicant);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.IDNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (userEduDetails != null)
                {
                    return RedirectToAction("ModifyUserData", "Education", new { @ApplicantId = applicant.Id });

                }
                else
                {
                    return RedirectToAction("AddEducationDetails", "Education", new { @ApplicantId = applicant.Id });

                }
            }
            return View(applicant);
        }


        public async Task<IActionResult> ModifyApplicant(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);

            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyApplicant(Guid id, [Bind("Id,Name,ReferenceNumber,LastName,Province,Initials,IDNumber,Race,Gender,Disability,DisabilityAttachment,Municipality,AddressIsSameAsPostal,AddressLine1,City,Code,HomeTel,Cellphone,Email,AlternativeContacPers,AlternativeNum,ProspectiveEmployer,IsCurrentlyEmployed,HasSignedTerms,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            var userEduDetails = await _context.Education.Where(x => x.ApplicantId == id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    applicant.IsActive = true;

                    applicant.UpdatedOn = DateTime.Now;

                    applicant.UpdatedBy = Helper.loggedInUser;

                    _context.Update(applicant);

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.IDNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (userEduDetails != null)
                {
                    return RedirectToAction("ModifyUserData", "Education", new { @ApplicantId = applicant.Id });

                }
                return RedirectToAction("AddEducationDetails", "Education", new { @ApplicantId = applicant.Id });
            }
            return View(applicant);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.Id == id);

            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicant = await _context.Applicants.FindAsync(id);

            _context.Applicants.Remove(applicant);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Attachments\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }

        public async void DisabilityFileHelper(Applicant applicant)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(applicant.DisabilityAtt.FileName);

            string extension = Path.GetExtension(applicant.DisabilityAtt.FileName);

            applicant.DisabilityAttachment = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            string path = Path.Combine(wwwRootPath + "/Attachments/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await applicant.DisabilityAtt.CopyToAsync(fileStream);
            }
        }

        [NonAction]
        private async Task OnSendSMSAsync(SMSViewModel viewModel)
        {
            viewModel = new SMSViewModel
            {
                message = "Another Test from System",
                mobileNumber = ""
            };

            await Helper.OnSendSMSNotification(viewModel);
        }

        private bool ApplicantExists(string Id)
        {
            return _context.Applicants.Any(e => e.IDNumber.Equals(Id));
        }

        private bool DoesEmailExists(string email)
        {
            return _context.Applicants.Any(e => e.Email.Equals(email));
        }





    }
}
