using ApprenticeApplications.Models;
using ForekRequisition.Data;
using ForekRequisition.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ForekRequisition.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

            OnGetAllApplicants();
            OnGetElectricalApplicants();
            OnGetPlumbingApplicants();  
            OnGetWeldingApplicants();
            OnGetFemaleApplicants();
            OnGetMaleApplicants();
            OnGetMPApplicants();
            OnGetFMApplicants();
            OnGetForekApplicants();
        }

        public IActionResult Index()
        {

            ViewData["All"] = OnGetAllApplicants();
            ViewData["Electrical"] = OnGetElectricalApplicants();
            ViewData["Plumbing"] = OnGetPlumbingApplicants();
            ViewData["Welding"] = OnGetWeldingApplicants();

            ViewData["Female"] = OnGetFemaleApplicants();
            ViewData["Male"] = OnGetMaleApplicants();
            ViewData["Mpum"] = OnGetMPApplicants();

            ViewData["OtherProgr"] = OnGetAllApplicants() -
                                     OnGetElectricalApplicants() -
                                     OnGetPlumbingApplicants() -
                                     OnGetWeldingApplicants();

            ViewData["OtherProv"] = OnGetAllApplicants() - OnGetMPApplicants();

            ViewData["ForekMega"] = OnGetFMApplicants();
            ViewData["Forek"] = OnGetForekApplicants();


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public int OnGetAllApplicants() =>  _context.Applicants.Count();
        [NonAction]
        public int OnGetElectricalApplicants() => _context.Applicants.
                                                    Where(x => x.Trade == ApprenticeApplications.
                                                    Models.Enums.eProgramme.Electrician).
                                                    Count();

        [NonAction]
        public int OnGetWeldingApplicants() => _context.Applicants.
                                                   Where(x => x.Trade == ApprenticeApplications.
                                                   Models.Enums.eProgramme.Welder).
                                                   Count();

        [NonAction]
        public int OnGetPlumbingApplicants() => _context.Applicants.
                                                  Where(x => x.Trade == ApprenticeApplications.
                                                  Models.Enums.eProgramme.Plumber).
                                                  Count();

        [NonAction]
        public int OnGetFemaleApplicants() => _context.Applicants.
                                                  Where(x => x.Gender == ApprenticeApplications.
                                                  Models.Enums.eGender.Female).
                                                  Count();

        [NonAction]
        public int OnGetMaleApplicants() => _context.Applicants.
                                          Where(x => x.Gender == ApprenticeApplications.
                                          Models.Enums.eGender.Male).
                                          Count();

        [NonAction]
        public int OnGetMPApplicants() => _context.Applicants.
                                         Where(x => x.Province == ApprenticeApplications.
                                         Models.Enums.eProvince.Mpumalanga).
                                         Count();

        [NonAction]
        public int OnGetFMApplicants() => _context.Applicants.
                                 Where(x => x.ProgrammeType == ApprenticeApplications.
                                 Models.Enums.eProgrammeType.ForekMegaApprentice).
                                 Count();

        [NonAction]
        public int OnGetForekApplicants() => _context.Applicants.
                                Where(x => x.ProgrammeType == ApprenticeApplications.
                                Models.Enums.eProgrammeType.ForekArtisanDevelopment).
                                Count();

    }
}