#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForekRequisition.Data;
using ForekRequisition.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using ForekRequisition.Models.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace ForekRequisition.Controllers
{
    public class ApprenticeResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notfy { get; }

        public ApprenticeResultsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notfy)
        {
            _context = context;

            this._hostEnvironment = hostEnvironment;

            _notfy = notfy;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppResults.ToListAsync());
        }

        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apprenticeResult = await _context.AppResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (apprenticeResult == null)
            {
                return NotFound();
            }

            return View(apprenticeResult);
        }

     
        [Authorize]
        public IActionResult SetApprenticeResults()
        {
            ViewData["Gents"] = OnGetMaleApplicants();

            ViewData["Ladies"] = OnGetFemaleApplicants();

            ViewData["ElecMale"] = OnGetMaleElecApplicants();

            ViewData["ElecFem"] = OnGetFemElecApplicants();

            ViewData["PlumMale"] = OnGetMalePlumbApplicants();

            ViewData["PlumFem"] = OnGetFemalePlumApplicants();

            ViewData["WeldMale"] = OnGetMaleWeldApplicants();

            ViewData["WeldFem"] = OnGetFemWeldApplicants();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetApprenticeResults([Bind("Id,Name,LastName,MunicipalDistrict,IdNumber,IsApproved,Gender,Cellphone,DoesApplicantQualify,Programme,ProgrammeType,Recommendation,PercentageObtained,IsActive,District,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApprenticeResult apprentice)
        {
            if (ModelState.IsValid)
            {
                if (!ApprenticeExists(apprentice.IdNumber))
                {
                    try
                    {
                        apprentice.Id = Helper.GenerateGuid();

                        apprentice.IsActive = true;

                        apprentice.CreatedOn = DateTime.Now;

                        apprentice.CreatedBy = Helper.loggedInUser;

                        _context.Add(apprentice);

                        await _context.SaveChangesAsync();

                        _notfy.Success("Apprentice Saved Successfully");

                        return RedirectToAction(nameof(SetApprenticeResults));
                    }
                    catch (Exception e)
                    {
                        _notfy.Error("Error: " + e.Message);
                    }

                }
                else
                {
                    _notfy.Error("Error: Apprentice already exists!");

                }


            }
            return View(apprentice);
        }

        public async Task<IActionResult> ApprenticeEdit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var list = await _context.AppResults.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();

            if (list != null)
            {
                ViewData["Name"] = list.Name;

                ViewData["LastName"] = list.LastName;

            }


            var apprenticeResult = await _context.AppResults.FindAsync(id);

            if (apprenticeResult == null)
            {
                return NotFound();
            }
            return View(apprenticeResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprenticeEdit(Guid id, [Bind("Id,Name,Gender,IsApproved, District,MunicipalDistrict, LastName,IdNumber,Cellphone,DoesApplicantQualify,Programme,ProgrammeType,Recommendation,PercentageObtained,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] ApprenticeResult apprenticeResult)
        {
            if (id != apprenticeResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    apprenticeResult.UpdatedOn = DateTime.Now;

                    apprenticeResult.UpdatedBy = Helper.loggedInUser;

                    _context.Update(apprenticeResult);

                    await _context.SaveChangesAsync();

                    _notfy.Success("User updated successfully");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprenticeExists(apprenticeResult.IdNumber))
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
            else
            {
                _notfy.Error("Error: User not updated");
            }
            return View(nameof(SetApprenticeResults));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apprenticeResult = await _context.AppResults
                .FirstOrDefaultAsync(m => m.Id == id);

            if (apprenticeResult == null)
            {
                return NotFound();
            }

            return View(apprenticeResult);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var apprenticeResult = await _context.AppResults.FindAsync(id);

            _context.AppResults.Remove(apprenticeResult);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ResultsSearch(string userId)
        {
            var getApplicant = await _context.AppResults.Where(m => m.IdNumber == userId).FirstOrDefaultAsync();

            if (getApplicant == null)
            {

                return View(nameof(OnNotComplete));

            }
            else
            {
                ViewData["Name"] = getApplicant.Name;

                ViewData["LName"] = getApplicant.LastName;

                ViewData["Per"] = getApplicant.PercentageObtained;

                ViewData["Programme"] = getApplicant.Programme;

                ViewData["Id"] = getApplicant.IdNumber;

                ViewData["Reco"] = getApplicant.Recommendation;

                ViewData["UserId"] = getApplicant.Id;


            }
            return View(getApplicant);
        }

        private bool ApprenticeExists(string id)
        {
            return _context.AppResults.Any(e => e.IdNumber == id);
        }

        [NonAction]
        public int OnGetFemaleApplicants() => _context.AppResults.
                                                  Where(x => x.Gender == ApprenticeApplications.
                                                  Models.Enums.eGender.Female).
                                                  Count();

        [NonAction]
        public int OnGetMaleApplicants() => _context.AppResults.
                                                 Where(x => x.Gender == ApprenticeApplications.
                                                 Models.Enums.eGender.Male).
                                                 Count();

        public int OnGetMaleElecApplicants() => _context.AppResults.
                                               Where(x => x.Gender == ApprenticeApplications.
                                               Models.Enums.eGender.Male
                                               && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Electrician))
                                               .Count();
        public int OnGetFemElecApplicants() => _context.AppResults.
                                               Where(x => x.Gender == ApprenticeApplications.
                                               Models.Enums.eGender.Female
                                               && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Electrician))
                                               .Count();

        public int OnGetFemWeldApplicants() => _context.AppResults.
                                       Where(x => x.Gender == ApprenticeApplications.
                                       Models.Enums.eGender.Female
                                       && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Welder))
                                       .Count();

        public int OnGetMaleWeldApplicants() => _context.AppResults.
                                  Where(x => x.Gender == ApprenticeApplications.
                                  Models.Enums.eGender.Male
                                  && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Welder))
                                  .Count();

        public int OnGetMalePlumbApplicants() => _context.AppResults.
                             Where(x => x.Gender == ApprenticeApplications.
                             Models.Enums.eGender.Male
                             && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Plumber))
                             .Count();

        public int OnGetFemalePlumApplicants() => _context.AppResults.
                             Where(x => x.Gender == ApprenticeApplications.
                             Models.Enums.eGender.Female
                             && x.Programme.Equals(ApprenticeApplications.Models.Enums.eProgramme.Plumber)).
                             Count();

        public IActionResult OnNotComplete()
        {
            return View();  
        }

       


    }
}
