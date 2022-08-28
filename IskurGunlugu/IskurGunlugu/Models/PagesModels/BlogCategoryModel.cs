using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IskurGunlugu.Models;

namespace IskurGunlugu.Models.PagesModels
{
    public class BlogCategoryModel
    {
        public List<Blog> Blog { get; set; }
        public List<Category> Category { get; set; }
        public Blog SingleBlog { get; set; }

    }
}