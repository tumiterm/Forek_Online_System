using ApprenticeApplications.Models.Enums;
using AspNetCoreHero.ToastNotification.Abstractions;
using CMS.Models.ViewModels;
using ForekRequisition.Data;
using ForekRequisition.Models;
using ForekRequisition.Models.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForekRequisition.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public INotyfService _notfy { get; }
        public AuthController(ApplicationDbContext context, INotyfService notfy)
        {
            _context = context;

            _notfy = notfy;
        }


        [HttpGet]
        public IActionResult OnSignIn()
        {
            //UserLogin login = new UserLogin();

            //login.ReturnUrl = ReturnUrl;

            // return View(login);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnSignIn(UserLogin user, string? ReturnUrl = "")
        {
            string message = "";

            if (!String.IsNullOrEmpty(user.EmailID))
            {
                if (!String.IsNullOrEmpty(user.Password))
                {
                    if (ModelState.IsValid)
                    { 

                        var userInfo = await _context.Users.Where(m => m.Email == user.EmailID 

                        && m.Password == Helper.EncryptInput(user.Password)).

                        FirstOrDefaultAsync();

                        if (userInfo != null)
                        {
                            var claims = new List<Claim>()
                            {
                               new Claim(ClaimTypes.NameIdentifier,userInfo.Id.ToString()),
                               new Claim(ClaimTypes.Name, userInfo.Name),
                               new Claim(ClaimTypes.Surname, userInfo.LastName),
                               new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                               new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
                               new Claim("ApprenticeProjCookie","Code")
                            };

                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var principal = new ClaimsPrincipal(identity);
                            

                           var currentUserName = identity.FindFirst(ClaimTypes.Name);

                           var currentUserSurname = identity.FindFirst(ClaimTypes.Surname);

                           var currentUserId = identity.FindFirst(ClaimTypes.NameIdentifier);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                            {
                                IsPersistent = user.RememberMe

                            });

                            if (!userInfo.IsEmailVerified)
                            {
                                ViewBag.Message = "Please verify your email first";

                                _notfy.Error("email not verified");

                                return View();

                            }


                            if (string.Compare(Helper.EncryptInput(user.Password), userInfo.Password) == 0)
                            {
                                int timeout = user.RememberMe ? 525600 : 20;

                                if (Url.IsLocalUrl(ReturnUrl))
                                {
                                    return Redirect(ReturnUrl);
                                }
                                else
                                {

                                    Helper.loggedInUser = $"{userInfo.Name} {userInfo.LastName}";

                                    userInfo.LastLoginDate = DateTime.Now.ToString();

                                    if(userInfo.Role == eRole.SystemAdministrator)
                                    {
                                        return RedirectToAction("Index", "Home");

                                    }
                                    else
                                    {
                                        return RedirectToAction("ApplicantRegister", "Applicant");

                                    }

                                }

                            }
                            else
                            {
                                message = "Invalid credentials provided";

                                _notfy.Error("Invalid credentials provided");
                            }
                        }
                        else
                        {
                            message = "Error: An error occured!";

                            _notfy.Error("Error: An error occured!");

                        }
                    }
                }
                else
                {
                    message = "Error: Invalid or Empty Password!";

                    _notfy.Error("Error: Invalid or Empty Password!");
                }
            }
            else
            {
                message = "Error: Invalid or Empty Email Provided!";

                _notfy.Error("Error: Invalid or Empty Email Provided!");
            }

            ViewBag.Message = message;

            return View();
        }

        public IActionResult UserSignUp()
        {
            return View();
        }

        public IActionResult ModifyUserInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ModifyUserInfo(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoesUserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));

        }



        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UserSignUp(User user)
            {

                bool Status = false;

                string message = "";

                #region Generate Activation Code 

                string time = DateTime.Now.ToString("hh:mm tt");

                string date = DateTime.Now.ToString("dddd, dd MMMM yyyy") + " " + time;

                user.ActivationCode = Helper.GenerateGuid();

                user.LastLoginDate = date;

                user.ResetPasswordCode = "";

                user.Id = Helper.GenerateGuid();

                #endregion

                #region  Password Hashing 
                user.Password = Helper.EncryptInput(user.Password);
                user.ConfirmPassword = Helper.EncryptInput(user.ConfirmPassword);
                #endregion

                //Set to false before production
                user.IsEmailVerified = true;

                user.IsActive = true;
                user.CreatedOn = DateTime.Now;
                user.ResetPasswordCode = "";
                user.Role = eRole.Applicant;

                if (ModelState.IsValid)
                {


                    #region Save to Database

                    try
                    {

                        await _context.Users.AddAsync(user);

                        await _context.SaveChangesAsync();

                        ViewData["successMessage"] = $"User Successfully Registered";

                        //SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                        //message = " Registration successfully done. Account activation link " +
                        //    " has been sent to your email: " + user.Email + "\nKindly click on the link to activate your account.\n" +
                        //    " Yours In-Community";
                        Status = true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                }
                else
                {
                    message = "Registration Successful";
                }

                ViewBag.Message = message;
                ViewBag.Status = Status;

                return View(user);
            }
        


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Helper.loggedInUser = String.Empty;

            return RedirectToAction("OnSignIn");
        }

        private bool DoesUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


    }
}
