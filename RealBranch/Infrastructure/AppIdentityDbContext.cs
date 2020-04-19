﻿
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealBranch.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("name=ARContext") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    //public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    //{
    //    protected override void Seed(AppIdentityDbContext context)
    //    {
    //        PerformInitialSetup(context);
    //        base.Seed(context);
    //    }
        //public void PerformInitialSetup(AppIdentityDbContext context)
        //{
        //    AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
        //    AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));

        //    string roleName = "Administrators";
        //    string userName = "Admin";
        //    string password = "mypassword";
        //    string email = "admin@professorweb.ru";

        //    if (!roleMgr.RoleExists(roleName))
        //    {
        //        roleMgr.Create(new AppRole(roleName));
        //    }

        //    AppUser user = userMgr.FindByName(userName);
        //    if (user == null)
        //    {
        //        userMgr.Create(new AppUser { UserName = userName, Email = email },
        //            password);
        //        user = userMgr.FindByName(userName);
        //    }

        //    if (!userMgr.IsInRole(user.Id, roleName))
        //    {
        //        userMgr.AddToRole(user.Id, roleName);
        //    }
        //}
        public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
        { }
    }