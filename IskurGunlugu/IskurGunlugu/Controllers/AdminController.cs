using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IskurGunlugu.Models;
using IskurGunlugu.Models.PagesModels;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace IskurGunlugu.Controllers
{
    [Authorize(Roles = "Admin,User,Author")]//Yetkisi admin olan sadece bu controllere girebilir.
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,User,Author")]
        public ActionResult Slider()
        {
            var slider = db.Slider.Where(x => x.IsDelete == false && x.Status == true).ToList();

            return View(slider);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult SliderCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SliderCreate(Slider slider, HttpPostedFileBase Image)
        {
            Slider addSlider = new Slider();

            addSlider = slider;
            if (Image != null && Image.ContentLength > 0)
            {
                string ImagePath = "";
                string ImageName = "";

                ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/images/slider"), ImageName);
                Image.SaveAs(ImagePath);
                addSlider.Image = ImageName;
            }
            else
            {
                addSlider.Image = null;
            }


            db.Slider.Add(addSlider);
            db.SaveChanges();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SliderDelete(int id)
        {
            var slider = db.Slider.Find(id);
            if (slider != null)
            {
                slider.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("Slider");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult SliderEdit(int id)
        {
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return RedirectToAction("Slider");
            }

            return View(slider);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SliderEdit(Slider slider, HttpPostedFileBase Image)
        {
            Slider editSlider = db.Slider.Find(slider.Id);

            if (Image != null && Image.ContentLength > 0)
            {
                string ImagePath = "";
                string ImageName = "";

                ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/images/slider"), ImageName);
                Image.SaveAs(ImagePath);
                editSlider.Image = ImageName;
            }
            editSlider.IsDelete = slider.IsDelete;
            editSlider.Status = slider.Status;
            editSlider.Title = slider.Title;
            editSlider.TitleColor = slider.TitleColor;
            editSlider.Link = slider.Link;
            editSlider.Description = slider.Description;
            editSlider.Button = slider.Button;

            db.SaveChanges();

            return View(editSlider);
        }
        [Authorize(Roles = "Admin,User")]
        public ActionResult Category()
        {
            var category = db.Category.Where(x => x.IsDelete == false && x.Status == true).ToList();

            return View(category);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CategoryCreate(Category category, HttpPostedFileBase Image)
        {
            Category addCategory = new Category();

            addCategory = category;
            if (Image != null && Image.ContentLength > 0)
            {
                string ImagePath = "";
                string ImageName = "";

                ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/images/category"), ImageName);
                Image.SaveAs(ImagePath);
                addCategory.Image = ImageName;
            }
            else
            {
                addCategory.Image = null;
            }


            db.Category.Add(addCategory);
            db.SaveChanges();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CategoryDelete(int id)
        {
            var category = db.Category.Find(id);
            if (category != null)
            {
                category.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("Category");
        }
        [Authorize(Roles = "Admin,User")]
        public ActionResult Blog()
        {
            BlogCategoryModel model = new BlogCategoryModel();

            model.Blog = db.Blog.Where(x => x.IsDelete == false).ToList();
            model.Category = db.Category.ToList();
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult BlogCreate()
        {
            BlogCategoryModel model = new BlogCategoryModel();
            model.Category = db.Category.Where(x => x.IsDelete == false && x.Status == true).ToList();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult BlogCreate(Blog blog, HttpPostedFileBase Image)
        {


            BlogCategoryModel model = new BlogCategoryModel();
            model.Category = db.Category.Where(x => x.IsDelete == false && x.Status == true).ToList();

            string ImagePath = "";
            string ImageName = "";
            Blog addblog = new Blog();
            if (Image != null && Image.ContentLength > 0)
            {
                ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/images/blog"), ImageName);
                Image.SaveAs(ImagePath);
                addblog.Image = ImageName;
            }
            addblog.Title = blog.Title;
            addblog.Description = blog.Description;
            addblog.Status = blog.Status;
            addblog.UserId = 1;
            addblog.CategoryId = blog.CategoryId;
            db.Blog.Add(addblog);
            db.SaveChanges();

            var id = db.Blog.ToList().LastOrDefault().Id;
            return Redirect("~/Admin/BlogEdit/" + id);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult BlogEdit(int id)
        {
            var blog = db.Blog.Find(id);
            BlogCategoryModel model = new BlogCategoryModel();
            if (blog != null)
            {
                model.SingleBlog = blog;
                model.Category = db.Category.Where(x => x.IsDelete == false && x.Status == true).ToList();
                return View(model);
            }
            return RedirectToAction("Blog");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult BlogEdit(Blog blog, HttpPostedFileBase Image)
        {
            var editblog = db.Blog.Find(blog.Id);
            BlogCategoryModel model = new BlogCategoryModel();
            if (editblog != null)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    string ImagePath = "";
                    string ImageName = "";

                    ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/images/blog"), ImageName);
                    Image.SaveAs(ImagePath);

                    editblog.Image = ImageName;
                }
                editblog.Title = blog.Title;
                editblog.Description = blog.Description;
                editblog.Status = blog.Status;
                editblog.CategoryId = blog.CategoryId;

                db.SaveChanges();

                model.SingleBlog = editblog;
                model.Category = db.Category.Where(x => x.IsDelete == false && x.Status == true).ToList();
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult BlogDelete(int id)
        {
            var blog = db.Blog.Find(id);
            if (blog != null)
            {
                blog.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("Blog");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult BlogComment(int id)
        {
            var blog = db.Blog.Find(id);
            if (blog != null)
            {
                ViewBag.BlogTitle = blog.Title;
                var blogcomment = db.BlogComment.Where(x => x.BlogId == id && x.IsDelete == false).ToList();
                return View(blogcomment);
            }
            return RedirectToAction("Blog");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CommentView(int id)
        {
            var comment = db.BlogComment.Find(id);
            if (comment != null)
            {
                BlogDetailModel model = new BlogDetailModel()
                {
                    SingleBlogComment = comment,
                    User = db.User.ToList(),
                    Blog = db.Blog.ToList()
                };
                return View(model);
            }
            return Redirect("~/Admin/BlogComment/" + comment.BlogId);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CommentView(BlogComment comment)
        {
            var editcomment = db.BlogComment.Find(comment.Id);
            if (editcomment != null)
            {
                editcomment.Status = comment.Status;
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
                //return Redirect(Request.Url.AbsoluteUri);
            }
            return RedirectToAction("Blog");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CommentDelete(int id)
        {
            var comment = db.BlogComment.Find(id);
            if (comment != null)
            {
                comment.IsDelete = true;
                db.SaveChanges();
            }
            return Redirect("~/Admin/BlogComment/" + comment.BlogId);
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult Users()
        {
            UserRoleModel model = new UserRoleModel()
            {
                UserList = db.User.ToList(),
                RoleList = db.Role.ToList()

            };
            return View(model);

        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin,User")]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "Admin,User")]
        public ActionResult Login(string UserName, string Password, bool? Remember)
        {
            var userControl = db.User.FirstOrDefault(x => x.UserName == UserName && x.Password == Password);
            if (userControl != null)
            {
                if (userControl.IsDelete == false)
                {
                    bool benihatirla = Remember == null ? false : true;
                    FormsAuthentication.SetAuthCookie(userControl.UserName, benihatirla);
                    ViewBag.Mesaj = "Merhaba" + " " + UserName;
                    return RedirectToAction("index");
                }
                else
                {
                    ViewBag.Mesaj = "Bu kullanıcı hesabı silinmiştir.";
                }

            }

            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Şifre Hatalı";

            }
            return View();
        }
        [Authorize(Roles = "Admin,User")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult UserCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(User user, HttpPostedFileBase Image)
        {
            Regex PasswordControl = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (PasswordControl.IsMatch(user.Password))//true false
            {
                ViewBag.Mesaj = "Kayıt İşlemi Başarılı";
            }
            else
            {
                ViewBag.Mesaj = "Şifrede büyük harf, küçük harf, rakam ve özel karakter olmalıdır.";
            }
            UserRoleModel model = new UserRoleModel();
            model.UserList = db.User.Where(x => x.IsDelete == false && x.Status == true).ToList();

            string ImagePath = "";
            string ImageName = "";
            User adduser = new User();
            if (Image != null && Image.ContentLength > 0)
            {
                ImageName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(Image.FileName);
                ImagePath = Path.Combine(Server.MapPath("~/Content/images/user"), ImageName);
                Image.SaveAs(ImagePath);
                adduser.Image = ImageName;
            }
            adduser.Status = user.Status;
            adduser.IsDelete = user.IsDelete;
            adduser.UserName = user.UserName;
            adduser.Password = user.Password;
            adduser.Email = user.Email;
            adduser.UserName = user.Name;
            adduser.Password = user.Surname;
            db.User.Add(adduser);
            db.SaveChanges();

            var id = db.User.ToList().LastOrDefault().Id;
            return Redirect("~/Admin/UserEdit/" + id);
            

        }
    
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UserEdit(int id)
        
            {
                var user = db.User.Find(id);
                UserRoleModel model = new UserRoleModel();
                if (user != null)
                {
                    model.SingleUser = user;
                    model.UserList = db.User.Where(x => x.IsDelete == false && x.Status == true).ToList();
                    return View(model);
                }
                return RedirectToAction("Users");
            }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UserEdit(User user)
        {
            var edituser = db.User.Find(user.Id);
            UserRoleModel model = new UserRoleModel();

            edituser.UserName = user.UserName;
            edituser.Password = user.Password;
            edituser.Email = user.Email;
            edituser.Name = user.Name;
            edituser.Surname = user.Surname;
            edituser.RegisterDate = user.RegisterDate;
            edituser.RoleId = user.RoleId;

            db.SaveChanges();

            model.SingleUser = edituser;
            model.UserList = db.User.Where(x => x.IsDelete == false && x.Status == true).ToList();
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UserDelete(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                user.IsDelete = true;
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult UserComment(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                
            }
            return RedirectToAction("Blog");
        }

    }
}
