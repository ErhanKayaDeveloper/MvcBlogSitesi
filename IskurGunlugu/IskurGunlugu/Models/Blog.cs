using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models
{
    public class Blog:TableStaticColumns
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public List<BlogComment> BlogComment { get; set; }
    }
}