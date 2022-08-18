using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Offers_MVC.Models
{
    public class JobApplicationsViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<ApplyForJob> items { get; set; }
    }
}