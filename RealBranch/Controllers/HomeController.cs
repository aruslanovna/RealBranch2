using Azure.Storage.Blobs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model;
using RealBranch.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealBranch.Controllers
{
    public class HomeController : BaseController
    {


        public async Task<ActionResult> Index()
        {
            //string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            //// Create a BlobServiceClient object which will be used to create a container client
            //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            ////Create a unique name for the container
            //string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            //// Create the container and return a container client object
            //BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            //// Create a local file in the ./data/ directory for uploading and downloading
            //string localPath = "./data/";
            //string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
            //string localFilePath = Path.Combine(localPath, fileName);

            //// Write text to the file
            // System.IO.File.WriteAllText(localFilePath, "Hello, World!");

            //// Get a reference to a blob
            //BlobClient blobClient = containerClient.GetBlobClient(fileName);

            //Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

            //// Open the file and upload its data
            //FileStream uploadFileStream = System.IO.File.OpenRead(localFilePath);
            //await blobClient.UploadAsync(uploadFileStream, true);
            //uploadFileStream.Close();
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