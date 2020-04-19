using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model;
using RealBranch.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealBranch.Controllers
{
    public class HomeController : BaseController
    {


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Localization()
        {
            return PartialView();
        }
        public ActionResult Footer()
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult UserProps()
        {
            return View(CurrentUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UserProps(string Name)
        {
            AppUser user = CurrentUser;
            user.UserName = Name;
            await UserManager.UpdateAsync(user);
            return View(user);
        }

        private AppUser CurrentUser
        {
            get
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        // Вспомогательный метод, загружающий название элемента перечисления
        // из атрибута Display
        [NonAction]
        public static string GetCityName<TEnum>(TEnum item)
            where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("Тип TEnum должен быть перечислением");
            }
            else
                return item.GetType()
                    .GetMember(item.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .Name;
        }
    }
}