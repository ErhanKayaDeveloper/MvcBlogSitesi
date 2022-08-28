using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IskurGunlugu.Models
{
    public class DataContext:DbContext
    {
        public DataContext():base("DbConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();//çoka çok diagram ilişkileri
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//bire çok diagram ilişkileri
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogComment> BlogComment { get; set; }
    }
}