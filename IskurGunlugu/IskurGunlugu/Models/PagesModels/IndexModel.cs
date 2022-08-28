using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models.PagesModels
{
    public class IndexModel
    {
        public List<Slider> Slider { get; set; }
        public List<Category> Category { get; set; }
        public List<Blog> Blog { get; set; }
    }
}