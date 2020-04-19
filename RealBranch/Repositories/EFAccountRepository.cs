using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using Model;
using System.Web.ModelBinding;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Model.LINQmodels;
using Repository;
using System.Security.Principal;
using System.Web;
using System.Security.Policy;

namespace RealBranch.Repositories
{
    public class EFAccountRepository
    {
        ARContext db = new ARContext();

        internal IEnumerable<UserProfileModel> UserProfile(AppUser user)
        {
            IEnumerable<UserProfileModel> stores =
         from x in db.Articles
         where x.UserId == user.Id
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


         };
            return stores;
        }

        public IEnumerable<UserProfileModel> AnotherOneUserProfile(AppUser targetuser)
        {
            IEnumerable<UserProfileModel> model =
       from x in db.Articles
       where x.UserId == targetuser.Id
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
       };
            return model;
        }

        internal void Save(AppUser user, string FollowingId)
        {
            Following following = new Following();
            following.FollowingId = FollowingId;
            following.UserId = user.Id;
            Follower follower = new Follower();
            follower.UserId = FollowingId;
            follower.FollowerId = user.Id;
            db.Followers.Add(follower);
            db.Followings.Add(following);
            db.SaveChanges();
        }

        internal void Unfollow(AppUser user, string FollowingId)
        {
            var following = db.Followings.Single(x => x.FollowingId == FollowingId && x.UserId == user.Id);
            db.Followings.Remove(following);
            var followers = db.Followers.Single(x => x.UserId == FollowingId && x.FollowerId == user.Id);
            db.Followers.Remove(followers);
            db.SaveChanges();
        }
    }
}