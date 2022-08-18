using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Offers_MVC.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public virtual Jobs job { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}