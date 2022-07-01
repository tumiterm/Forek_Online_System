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

namespace ForekRequisition.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string _pass { get; }

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

 
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        public IActionResult RegisterUser()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser([Bind("ConfirmPassword,Id,Name,LastName,Phone,Email,Password,IsEmailVerified,ActivationCode,ResetPasswordCode,LastLoginDate,Role,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Helper.GenerateGuid();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

   
        public async Task<IActionResult> ModifyUserDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            
            ViewData["password"] = user.Password;

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyUserDetails(Guid id, [Bind("ConfirmPassword,Id,Name,LastName,Phone,Email,Password,IsEmailVerified,ActivationCode,ResetPasswordCode,LastLoginDate,Role,IsActive,CreatedOn,UpdatedOn,CreatedBy,UpdatedBy")] User user)
        {
            if (id != user.Id)
            {
                //StatusCode = 404;
                
                return View("NotFoundView",id);  
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.LastLoginDate = DateTime.Now.ToString();
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
