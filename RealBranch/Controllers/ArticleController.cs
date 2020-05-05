using Azure.Storage.Blobs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model;
using Model.LINQmodels;
using RealBranch.Infrastructure;
using RealBranch.Repositories;
using Repository;
using Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealBranch.Controllers
{
    public class ArticleController : BaseController
    {


        EFArticleRepository repository = new EFArticleRepository();
        ARContext db = new ARContext();

        // GET: Article
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<ActionResult> Create(Article model)
        {
            AppUser user = CurrentUser;
            Article c = new Article();


            if (ModelState.IsValid)
            {

                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file.InputStream != null)
                {

                    byte[] imageBytes = null;
                    var reader = file.InputStream;

                    c.Photo = imageBytes;

                    string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
                   
                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                    //Create a unique name for the container
                    string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

                    // Create the container and return a container client object
                    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
                    // Create a local file in the ./data/ directory for uploading and downloading
                    string localPath = "./data/";
                    string fileName = "quickstart" + Guid.NewGuid().ToString() + ".jpg";
                    string localFilePath = Path.Combine(localPath, fileName);

                    containerClient.UploadBlob(localFilePath, reader);
                    // Get a reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);

              

                    // Open the file and upload its data
                  //  FileStream uploadFileStream = System.IO.File.OpenRead(localFilePath);
                   // await blobClient.UploadAsync(uploadFileStream, true);
                  //  uploadFileStream.Close();

                }

                c.UserId = user.Id;
                c.Name = model.Name;
                if (String.IsNullOrEmpty(c.Name))
                    ModelState.AddModelError("Name", Resources.Content.TitleRequired);
                c.Briefly = model.Briefly;

                c.Description = model.Description;

                db.Articles.Add(c);
                db.SaveChanges();
                ViewBag.Ok = Resources.Content.ArticleSaved;
                return View(c);
            }
            return View();
            // return RedirectToAction("UserProfile", "Account");

        }


        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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

        public ActionResult GetAllArticles()
        {


            var courses = db.Articles.AsNoTracking();
            return View(courses.ToList());
        }

        public ActionResult GetArticle(int id)
        {

            AppUser user = CurrentUser;
            if (user != null && user.Photo != null)
            {
                ViewBag.Photo = user.Photo;
            }
            if (user != null)
            {
                ViewBag.Name = user.UserName;
            }
            IEnumerable<UserProfileModel> stores =
    from x in db.Articles
    where x.ArticleId == id
    select new UserProfileModel
    {
        Photo = x.Photo,
        Briefly = x.Briefly,
        DatePost = x.PostDate,

        ArticleId = x.ArticleId,
        ArticleName = x.Name,
        Description = x.Description,
        Topic = x.Topics.Name,
        Comment = db.Comments
                  .Where(y => y.ArticleId == x.ArticleId)
                  .Select(y => y.Content).ToList(),
        CommentDate = db.Comments
                  .Where(y => y.ArticleId == x.ArticleId)
                  .Select(y => y.PostDate).ToList(),
        CommentedUserId = db.Comments
                  .Where(y => y.ArticleId == x.ArticleId)
                  .Select(y => y.UserId).ToList(),



    };
            Article article = db.Articles.FirstOrDefault(x => x.ArticleId == id);
            return View(stores);
        }
        [Authorize]
        public ActionResult Subscription()
        {
            AppUser user = CurrentUser;



            // IEnumerable<Article> articles = db.Articles.Where(x => x.ArticleId in db.SelectedNews.Where(y=>y.UserId==user.Id).Select(y=>y.ArticleId).FirstOrDefault()).ToList();
            string query = "SELECT * FROM Articles where ArticleId IN (SELECT ArticleId FROM SelectedNews where UserId=" + "'" + user.Id + "'" + ")";
            IEnumerable<Article> result = db.Articles.SqlQuery(query).ToList();
            return View(result);

        }

        [Authorize]
        public ActionResult AddComment(Comment model, int articleId)
        {
            AppUser user = CurrentUser;
            if (ModelState.IsValid)
            {
                Comment c = new Comment();

                DateTime d = DateTime.Now;


                c.ArticleId = articleId;
                c.Content = model.Content;
                c.PostDate = d;
                c.UserId = user.Id;
                db.Comments.Add(c);
                db.SaveChanges();
                return RedirectToAction("GetArticle", new { id = articleId });
            }
            return RedirectToAction("GetArticle", new { id = articleId });

        }
        [Authorize]
        public ActionResult AddArticle(SelectedNews model, int articleId)
        {
            AppUser user = CurrentUser;
            if (ModelState.IsValid)
            {
                SelectedNews c = new SelectedNews();

                c.ArticleId = articleId;
                c.UserId = user.Id;

                db.SelectedNews.Add(c);
                db.SaveChanges();
                return RedirectToAction("GetAllArticles");
            }
            return RedirectToAction("GetAllArticles");

        }
        public ActionResult GetFollowingArticles()
        {
            AppUser user = CurrentUser;

            string query = "SELECT * FROM Articles where UserId IN (SELECT FollowingId FROM Followings where UserId=" + "'" + user.Id + "'" + ")";
            IEnumerable<Article> result = db.Articles.SqlQuery(query).ToList();
            return View(result);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article != null)
            {
                return View(article);
            }
            else
            {
                return RedirectToAction("UserProfile", "Account");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(string name, string briefly, string description, int id)
        {

            AppUser user = CurrentUser;
            Article article = db.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article != null)
            {

                article.UserId = user.Id;
                article.Briefly = briefly;
                article.Name = name;
                article.Description = description;




                HttpPostedFileBase file = Request.Files["ImageData"];
                if (file != null)
                {

                    byte[] imageBytes = null;
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    imageBytes = reader.ReadBytes((int)file.ContentLength);

                    if (imageBytes.Length != 0)
                    {
                        article.Photo = imageBytes;
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "We can't find user");
            }



            return RedirectToAction("GetArticle", new { id = id });

        }


    }
}