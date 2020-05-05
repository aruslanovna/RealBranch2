using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model;
using RealBranch.Infrastructure;
using Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealBranch.Controllers
{
    public class DesignController : BaseController
    {
        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Create");
        }

        ARContext db = new ARContext();
        // GET: Design
        public ActionResult Ekibana()
        {

            return View();
        }
        public ActionResult Design()
        {
            AppUser user = CurrentUser;
            IEnumerable<Note> note = db.Notes.Where(x => x.UserId == user.Id);
            return View(note);
        }

     
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }
        [Authorize]
        public ActionResult Screen()
        {

            AppUser user = CurrentUser;
            if (ModelState.IsValid)
            {
                Note note = new Note();
                HttpPostedFileBase file = Request.Files["ImageData"];
                Rectangle rect = new Rectangle(640, 350, 700, 600);
                Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
                DateTime d = DateTime.Now;
                string dd = d.Day + "-" + d.Month + "-" + d.Hour + "-" + d.Minute;
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(bmp, typeof(byte[]));
                note.Photo = xByte;
                note.UserId = user.Id;
                note.Name = user.UserName + dd + ".jpg";
                db.Notes.Add(note);
                db.SaveChanges();
            }
            return View("Ekibana");

        }

        public ActionResult DesignAnotherUser(string id)
        {
            AppUser user = CurrentUser;
            IEnumerable<Note> note = db.Notes.Where(x => x.UserId == id);
            return View(note);
        }

    }
}