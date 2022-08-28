using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models
{
    public class BlogComment:TableStaticColumns
    {
        public string Comment { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
       public Blog Blog { get; set; }
    }
}