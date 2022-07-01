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
using ApprenticeApplications.Models;
using ForekRequisition.Models.ViewModel;
using ForekRequisition.Models.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace ForekRequisition.Controllers
{
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notfy { get; }
        public Guid _applicantId { get; set; }
        private string _usermail { get; set; }

        public EducationController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notfy)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            _notfy = notfy;
        }
        public async Task<IActionResult> Index(int pg=1)
        {

            var getEd = await _context.Education.ToListAsync();

            const int pageSize = 3;

            if(pg<1)
                pg = 1;
            
            int recsCount = getEd.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = getEd.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);

            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }
        public async Task<IActionResult> ApplicantDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
                //_notfy.Error("Error: Applicant NOT found!");

            }

            var applicant = await _context.Education

                .FirstOrDefaultAsync(m => m.ApplicantId == id);

            if (applicant == null)
            {
                _notfy.Error("Error: Applicant did NOT complete school leaving details!");
 
                return RedirectToAction("IncompleteData", "Education", id);

            }

            return View(applicant);
        }
        public async Task<IActionResult> AddEducationDetails(Guid ApplicantId)
        {
            await Initializer(ApplicantId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducationDetails(Education model,Guid ApplicantId)
        {
            if (model.RSAIDDoc != null && model.Gr12 != null)  
            {
                RSAIDFileHelper(model);
                ResidenceFileHelper(model);
                BankDocsFileHelper(model);
                MatricFileHelper(model);
                CVFileHelper(model);
                MedicalFormFileHelper(model);
                //AffidavitFileHelper(model);
                //HostFileHelper(model);
            }

           //bool isDocsLoaded = IsDocNotNull(model.RSAIDDoc,model.Gr12,
           //                                 model.BankDocs,model.CVDocs,
           //                                 model.MedCertificate,model.ResidenceDoc
           //                              

                var user = _context.Applicants.Where(n => n.Id == ApplicantId).FirstOrDefault();

                if (ModelState.IsValid)
                {

                model.ApplicantId = ApplicantId;

                model.Id = Helper.GenerateGuid();

                model.IsActive = true;  
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = Helper.loggedInUser;

                _context.Add(model);

                await _context.SaveChangesAsync();

                if (user != null)
                {
                    Helper.SendEmailNotification(user.Email, "Confirmation of Application",
                            Helper.AcknowledgementMessage(user.Name,
                            user.Trade.ToString(),
                            user.ReferenceNumber));

                }

                _notfy.Success("Application Successful");

                return RedirectToAction("Details", "Applicant", new { @id = model.ApplicantId });

                }
                else
                {
                    _notfy.Error("Ooops: Something went wrong!");
                    _notfy.Error("Error: Documents required!");

                }

            return RedirectToAction("Details", "Applicant", new { @id = model.ApplicantId });

        }

        public ActionResult Success()
        {
            return View();
        }
        public async Task<IActionResult> ModifyUserData(Guid? ApplicantId)
        {
            if (ApplicantId == null)
            {
                return NotFound();
            }

            Education applicant = await _context.Education.Where(x => x.ApplicantId == ApplicantId).FirstOrDefaultAsync();

            if (applicant == null)
            {
                _notfy.Error("Error: User details not found");
                return NotFound();
            }
            return View(applicant);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyUserData(Education education,Guid ApplicantId)
        {

            if (education.RSAIDDoc != null && education.Gr12 != null)
            {
                RSAIDFileHelper(education);
                ResidenceFileHelper(education);
                BankDocsFileHelper(education);
                MatricFileHelper(education);
                CVFileHelper(education);
                MedicalFormFileHelper(education);
                //AffidavitFileHelper(model);
                //HostFileHelper(model);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    education.IsActive = true;
                    education.ApplicantId = ApplicantId;
                    education.UpdatedOn = DateTime.Now;
                    education.UpdatedBy = Helper.loggedInUser;
                   
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Applicant", new { @id = education.ApplicantId });
            }
            return View(education);
        }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var education = await _context.Education.FindAsync(id);
            _context.Education.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EducationExists(Guid id)
        {
            return _context.Education.Any(e => e.Id == id);
        }
        public async Task<Applicant> OnGetApplicant(Guid ApplicantId)
        {
            var user = await _context.Applicants.Where(x => x.Id == ApplicantId).FirstOrDefaultAsync();

            if (user != null)
            {
                ViewData["UserName"] = user.Name;
                ViewData["UserLastName"] = user.LastName;
                ViewData["Id"] = user.Id;

            }

            return user;
        }
        public async Task<Applicant> Initializer(Guid ApplicantId)
        {
            var user = await OnGetApplicant(ApplicantId);

            if (user != null)
            {
                ApplicantId = _applicantId;

                ViewData["Id"] = ApplicantId;

                ViewData["GetUserName"] = user.Name;

                ViewData["GetUserLastName"] = user.LastName;
            }

            return user;
        }
        public async void ResidenceFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.ResidenceDoc.FileName);

            string extension = Path.GetExtension(education.ResidenceDoc.FileName);

            education.ProofRes = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/Residence/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.ResidenceDoc.CopyToAsync(fileStream);
            }
        }
        public async void BankDocsFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.BankDocs.FileName);

            string extension = Path.GetExtension(education.BankDocs.FileName);

            education.BankingDocs = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/BankingDetails/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.BankDocs.CopyToAsync(fileStream);
            }
        }
        public async void MatricFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.Gr12.FileName);

            string extension = Path.GetExtension(education.Gr12.FileName);

            education.MatricResults = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/Matric/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.Gr12.CopyToAsync(fileStream);
            }
        }
        public async void MedicalFormFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.MedCertificate.FileName);

            string extension = Path.GetExtension(education.MedCertificate.FileName);

            education.MedicalCert = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/MedicalForm/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.MedCertificate.CopyToAsync(fileStream);
            } 
        }
        public async void RSAIDFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.RSAIDDoc.FileName);

            string extension = Path.GetExtension(education.RSAIDDoc.FileName);

            education.IdentityPDF = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/ID/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.RSAIDDoc.CopyToAsync(fileStream);
            }
        }
        public async void CVFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.CVDocs.FileName);

            string extension = Path.GetExtension(education.CVDocs.FileName);

            education.ApprenticeCV = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/CV/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.CVDocs.CopyToAsync(fileStream);
            }
        }
        public async void HostFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.HostFile.FileName);

            string extension = Path.GetExtension(education.HostFile.FileName);

            education.HostEmpNot = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/Host/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.HostFile.CopyToAsync(fileStream);
            }
        }
        public async void AffidavitFileHelper(Education education)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(education.AffidavitFile.FileName);

            string extension = Path.GetExtension(education.AffidavitFile.FileName);

            education.AffidavitInSupport = fileName = fileName + Helper.GenerateGuid() + extension;

            string path = Path.Combine(wwwRootPath + "/Affidavit/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await education.AffidavitFile.CopyToAsync(fileStream);
            }
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
        public async Task<IActionResult> BankFileDownload(string bankDoc)
        {
            if (bankDoc == null)
                return Content("Banking Details NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\BankingDetails\" + bankDoc;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> CVDownload(string CVDoc)
        {
            if (CVDoc == null)
                return Content("CV NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\CV\" + CVDoc;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> IDDownload(string IDDoc)
        {
            if (IDDoc == null)
                return Content("RSA ID NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\ID\" + IDDoc;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> MatricDownload(string filename)
        {
            if (filename == null)
                return Content("Matric Results NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Matric\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> ResidenceDownload(string residencDoc)
        {
            if (residencDoc == null)
                return Content("Proof of Residence NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Residence\" + residencDoc;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> MedicalFormDownload(string medcert)
        {
            if (medcert == null)
                return Content("Medical Form NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\MedicalForm\" + medcert;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> HostFormDownload(string host)
        {
            if (host == null)
                return Content("Host Letter not uploaded");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Host\" + host;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> AffidavitFormDownload(string affidavit)
        {
            if (affidavit == null)
                return Content("Affidavit not uploaded");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Affidavit\" + affidavit;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public IActionResult IncompleteData()
        {
            return View();  
        }

        [NonAction]
        public bool IsDocNotNull(params IFormFile[] docs)
        {
            bool result = false;

            foreach (var doc in docs)
            {
                if (!string.IsNullOrEmpty(doc.FileName))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
