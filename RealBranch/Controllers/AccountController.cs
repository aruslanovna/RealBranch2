using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Model;
using RealBranch.Infrastructure;
using RealBranch.Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

using System.Web.Mvc;

namespace RealBranch.Controllers
{
   
    [System.Web.Mvc.Authorize]
    public class AccountController : BaseController
    {

        EFAccountRepository repository = new EFAccountRepository();
        ARContext db = new ARContext();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (returnUrl != null)
            {
                ViewBag.returnUrl = returnUrl;
            }
            else
            {
                ViewBag.returnUrl = Url.Action("Index", "Home");
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]

        public async Task<ActionResult> Login(LoginViewModel details, string returnUrl)
        {
            AppUser user = await UserManager.FindAsync(details.Name, details.Password);

            if (user == null)
            {
                ModelState.AddModelError("", Resources.Content.Incorrect);
            }
            else
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                AuthManager.SignOut();
                AuthManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, ident);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);


                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(details);
        }
        [Authorize]
     
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }



        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        [Authorize]
        public ActionResult UserProfile()
        {
            AppUser user = CurrentUser;

            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.Photo = user.Photo;
            ViewBag.Status = user.Status;
            ViewBag.WorkPLace = user.WorkPlace;
            ViewBag.Position = user.Position;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.Experience = user.Experience;
            ViewBag.Info = user.Info;
            return View(repository.UserProfile(user));

        }
        [Authorize]

        public async Task<ActionResult> AnotherUserProfile(string error = "")
        {
            if (error != null)
            {
                ViewBag.Error = error;
            }
            string[] memberIDs = UserManager.Users.Select(x => x.Id).ToArray();

            IEnumerable<AppUser> members
                = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));

            return View(members);

        }

        [Authorize]

        public ActionResult AnotherOneUserProfile(string Id, string error)
        {
            AppUser user = CurrentUser;
            AppUser targetuser = UserManager.Users.FirstOrDefault(x => x.Id == Id);
            UserManager.UpdateAsync(targetuser);
            bool following = false;

            if (db.Followers.Any(x => x.FollowerId == user.Id && x.UserId == Id))
            {
                following = true;
            }
            ViewBag.Error = error;
            ViewBag.UserName = targetuser.UserName;
            ViewBag.Email = targetuser.Email;
            ViewBag.Photo = targetuser.Photo;
            ViewBag.Status = targetuser.Status;
            ViewBag.WorkPLace = targetuser.WorkPlace;
            ViewBag.Position = targetuser.Position;
            ViewBag.PhoneNumber = targetuser.PhoneNumber;
            ViewBag.Experience = targetuser.Experience;
            ViewBag.Info = targetuser.Info;
            ViewBag.Following = following;
            ViewBag.Id = targetuser.Id;

            return View(repository.AnotherOneUserProfile(targetuser));
        }
       

        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        [Authorize]
        public ActionResult Edit()
        {

            if (CurrentUser != null)
            {
                return View(CurrentUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(string UserName, string email, string password, string PhoneNumber, string position, string info, string status, string experience)
        {


            AppUser user = CurrentUser;
            if (CurrentUser != null)
            {
                user.UserName = UserName;
               
                user.Info = info;
                user.Status = status;

                user.Position = position;
                user.PhoneNumber = PhoneNumber;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                else
                {
                    user.Email = email;
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file != null)
                {

                    byte[] imageBytes = null;
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    imageBytes = reader.ReadBytes((int)file.ContentLength);

                    if (imageBytes.Length != 0)
                    {
                        user.Photo = imageBytes;
                    }
                }

                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserProfile");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "We can't find user");
            }
            return View(user);

        }
        public ActionResult AddUser(string FollowingId)
        {

            AppUser user = CurrentUser;
            if (user.Id != FollowingId)
            {
                repository.Save(user, FollowingId);
                return RedirectToAction("AnotherOneUserProfile", new { Id = FollowingId });
            }
            else
            {
                return RedirectToAction("AnotherOneUserProfile", new { Id = user.Id, error = "You can't follow yourself" }) ;
            }
        }

        public ActionResult Unfollow(string FollowingId)
        {
            AppUser user = CurrentUser;
            repository.Unfollow(user, FollowingId);
            return RedirectToAction("AnotherOneUserProfile", new { Id = FollowingId });

        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback",
                    new { returnUrl = returnUrl })
            };

            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            AppUser user = await UserManager.FindAsync(loginInfo.Login);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,

                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                else
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
            }

            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            ident.AddClaims(loginInfo.ExternalIdentity.Claims);

            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);

            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult FacebookLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookLoginCallback",
                    new { returnUrl = returnUrl })
            };

            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Facebook");
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public async Task<ActionResult> FacebookLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();

            AppUser user = await UserManager.FindAsync(loginInfo.Login);

            if (user == null)
            {
                user = new AppUser
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,

                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                else
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
            }

            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            ident.AddClaims(loginInfo.ExternalIdentity.Claims);

            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);

            return Redirect(returnUrl ?? "/");
        }

    }
}


