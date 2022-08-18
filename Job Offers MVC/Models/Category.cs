using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Job_Offers_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name ="Category Description")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Jobs> Job { get; set; }
    }
}