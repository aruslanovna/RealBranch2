using Microsoft.AspNet.Identity.EntityFramework;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ARContext : DbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public ARContext() : base("ARContext")
        { }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Directory> Directorys { get; set; }
        public DbSet<SelectedNews> SelectedNews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Article> Articles { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            
        }
    }
}
