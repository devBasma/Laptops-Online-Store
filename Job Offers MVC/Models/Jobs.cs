using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job_Offers_MVC.Models
{
    public class Jobs
    {
        public int Id { get; set; }
        [DisplayName("Laptop Name")]
        public string JobTitle { get; set; }
        [DisplayName("Description")]
        [AllowHtml]
        public string JobContent { get; set; }
        [DisplayName("Image")]
        public string JobImage { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public string UserID { get; set; }
        public virtual Category category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}