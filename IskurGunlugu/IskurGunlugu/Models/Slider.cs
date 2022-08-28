using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IskurGunlugu.Models
{
    public class Slider:TableStaticColumns
    {
        public string Title { get; set; }
        public string TitleColor { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Button { get; set; }
        public string Image { get; set; }
    }
}