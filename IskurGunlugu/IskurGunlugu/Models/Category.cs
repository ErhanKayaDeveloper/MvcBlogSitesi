using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models
{
    public class Category:TableStaticColumns
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<Blog> Blog { get; set; }
    }
}