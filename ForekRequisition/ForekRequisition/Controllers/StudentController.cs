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

namespace ForekRequisition.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public INotyfService _notfy { get; }

        public StudentController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, INotyfService notfy)
        {
            _context = context;
            _hostEnvironment = hostEnvironment; 
            _notfy = notfy;

        }
        public async Task<IActionResult> OnGetRegisteredStudents()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult OnGetResponse()
        {
            return View();
        }

        public IActionResult OnRegisterStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnRegisterStudent(Student student)
        {

            if(student.RSAID != null)
            {
                if(student.GuardianID != null)
                {
                    if (student.HighestCert != null)
                    {
                        if (student.AttachProofRes != null)
                        {
                            if (ModelState.IsValid)
                            {
                                RSAIDFileUploaderHelper(student);

                                HighestQualFileUploaderHelper(student);

                                ResidenceFileUploaderHelper(student);

                                GuardianIDFileUploaderHelper(student);

                                student.Id = Helper.GenerateGuid();

                                _context.Add(student);

                                var response = await _context.SaveChangesAsync();

                                if (response > 0)
                                {
                                    _notfy.Success("Student successfully registered");
                                    Helper.OnSendOfferMail("", "ifoliphant@forekinstitute.co.za", $"{student.FullNames} {student.Surname} Application",
                                    Helper.MessageRequest(student.FullNames,student.Surname,student.Course.ToString()));

                                }
                                else
                                {
                                    _notfy.Error("Entity NOT saved");

                                }

                                return RedirectToAction(nameof(OnGetResponse));
                            }
                            else
                            {
                                _notfy.Error("Error: something went wrong");
                            }
                        }
                        else
                        {
                            _notfy.Error("Error: Proof of residence not attached");
                            
                        }
                    }
                    else
                    {
                        _notfy.Error("Error: Certificate not attached");
                    }
                }
                else
                {
                    _notfy.Error("Error: Guardian ID not attached");

                }
            }
            return View(student);
        }

        public async Task<IActionResult> OnModifyStudent(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnModifyStudent(Guid id, [Bind("Id,Surname,FullNames,IDNumber,AddressType,Address,Code,Telephone,Cellphone,GradePassed,HighestQual,GuardianName,GuardianLastName,GuardianCell,GuardianTel,Relationship,Course,CourseDescription,HasFunding,FunderNameSurname,FunderContact,RSAIDAttach,IsEmployed")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(OnGetRegisteredStudents));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OnGetRegisteredStudents));
        }

        private bool StudentExists(Guid id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async void RSAIDFileUploaderHelper(Student student)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(student.RSAID.FileName);

            string extension = Path.GetExtension(student.RSAID.FileName);

            if(!string.IsNullOrEmpty(fileName))
            {
                if (!string.IsNullOrEmpty(extension))
                {
                    student.RSAIDAttach = fileName = fileName + Helper.GenerateGuid() + extension;

                    string path = Path.Combine(wwwRootPath + "/ID/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await student.RSAID.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    _notfy.Error("Error: ID/Passport NOT attached");
                    return;
                }
            }
            else
            {
                _notfy.Error("Error: Upload your RSA ID");
                return;

            }


        }
        public async void HighestQualFileUploaderHelper(Student student)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(student.HighestCert.FileName);


            if(!string.IsNullOrEmpty(fileName))
            {
                string extension = Path.GetExtension(student.HighestCert.FileName);

                student.HighestQualCertAttach = fileName = fileName + Helper.GenerateGuid() + extension;

                string path = Path.Combine(wwwRootPath + "/Qualification/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await student.HighestCert.CopyToAsync(fileStream);
                }
            }
            else
            {
                _notfy.Warning("Qualification NOT uploaded");
            }

            
        }
        public async void ResidenceFileUploaderHelper(Student student)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(student.AttachProofRes.FileName);

            if (!string.IsNullOrEmpty(fileName))
            {
                string extension = Path.GetExtension(student.AttachProofRes.FileName);

                student.ProofOfRes = fileName = fileName + Helper.GenerateGuid() + extension;

                string path = Path.Combine(wwwRootPath + "/Residence/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await student.AttachProofRes.CopyToAsync(fileStream);
                }
            }
            else
            {
                _notfy.Error("Upload Proof of Residence");
                return;
            }

            
        }
        public async void GuardianIDFileUploaderHelper(Student student)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            string fileName = Path.GetFileNameWithoutExtension(student.GuardianID.FileName);

            if (!string.IsNullOrEmpty(fileName))
            {
                string extension = Path.GetExtension(student.GuardianID.FileName);

                student.GuadianId = fileName = fileName + Helper.GenerateGuid() + extension;

                string path = Path.Combine(wwwRootPath + "/Guardian/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await student.GuardianID.CopyToAsync(fileStream);
                }
            }
            else
            {
                _notfy.Warning("Guardian ID NOT upoaded");
            }

           
        }


        public async Task<IActionResult> RSAIDFileDownload(string filename)
        {
            if (filename == null)
                return Content("RSA/Passport NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\ID\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> HighestQualFileDownload(string filename)
        {
            if (filename == null)
                return Content("Qualification NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Qualification\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> ResidenceFileDownload(string filename)
        {
            if (filename == null)
                return Content("Residence NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Residence\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }
        public async Task<IActionResult> GuardianIdFileDownload(string filename)
        {
            if (filename == null)
                return Content("Guardian ID NOT uploaded!");


            var path1 = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot");

            string folder = path1 + @"\Guardian\" + filename;


            var memory = new MemoryStream();

            using (var stream = new FileStream(folder, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, Helper.GetContentType(folder), Path.GetFileName(folder));
        }


    }
}
