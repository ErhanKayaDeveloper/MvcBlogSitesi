namespace IskurGunlugu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Blogs", "UserId");
            CreateIndex("dbo.Blogs", "CategoryId");
            CreateIndex("dbo.BlogComments", "UserId");
            CreateIndex("dbo.BlogComments", "BlogId");
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs", "Id");
            AddForeignKey("dbo.Blogs", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.BlogComments", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
            AddForeignKey("dbo.Blogs", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.BlogComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Blogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropIndex("dbo.BlogComments", new[] { "UserId" });
            DropIndex("dbo.Blogs", new[] { "CategoryId" });
            DropIndex("dbo.Blogs", new[] { "UserId" });
        }
    }
}
