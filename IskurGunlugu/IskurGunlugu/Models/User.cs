using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models
{
    public class User:TableStaticColumns
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string Image { get; set; }
        public int RoleId { get; set; } = 1;

        public Role Role { get; set; }
        public List<Blog> Blog { get; set; }
        public List<BlogComment> BlogComment { get; set; }
    }
}