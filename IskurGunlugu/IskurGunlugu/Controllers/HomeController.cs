using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IskurGunlugu.Models;
using IskurGunlugu.Models.PagesModels;

namespace IskurGunlugu.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            IndexModel model = new IndexModel()
            {
                Slider = db.Slider.Where(x => x.Status == true && x.IsDelete == false).ToList(),
                Category=db.Category.Where(x => x.Status == true && x.IsDelete == false).ToList(),
                Blog=db.Blog.Where(x => x.Status == true && x.IsDelete == false).Take(6).ToList()
            };


            return View(model);
        }
        public ActionResult Blog()
        {
            IndexModel model = new IndexModel()
            {
                Category = db.Category.Where(x => x.Status == true && x.IsDelete == false).ToList(),
                Blog = db.Blog.Where(x => x.Status == true && x.IsDelete == false).ToList()
            };


            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CategoryDetail(int id)
        {
            var category = db.Category.Find(id);

            if (category != null)
            {
                return View(category);
            }
            else{
                return RedirectToAction("index");
            }
        }
        public ActionResult BlogDetail(int id)
        {
           

            var blog = db.Blog.Find(id);
            if (blog != null)
            {
                BlogDetailModel model = new BlogDetailModel()
                {
                    SingleBlog = blog,
                    Comments = db.BlogComment.Where(x => x.BlogId == id && x.Status == true && x.IsDelete == false).ToList(),
                    User = db.User.ToList()
                };
                return View(model);
            }
            return RedirectToAction("Blog");
        }

        [HttpPost]
        public ActionResult BlogComment(string Comment,int Id)
        {
            BlogComment blogcomment = new BlogComment();
            blogcomment.BlogId = Id;
            blogcomment.Comment = Comment;
            blogcomment.UserId = 1;

            db.BlogComment.Add(blogcomment);
            db.SaveChanges();
            return Redirect("~/Home/BlogDetail/" + Id);
        }
    }
}