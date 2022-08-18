using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job_Offers_MVC.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Job_Offers_MVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(x => x.JobId == JobId && x.UserId == UserId).ToList();

            if (check.Count<1)
            {
                var JobApplication = new ApplyForJob();
                JobApplication.JobId = JobId;
                JobApplication.UserId = UserId;
                JobApplication.Message = Message;
                JobApplication.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(JobApplication);
                db.SaveChanges();

                ViewBag.Result = " Successfully Added To Your Cart";
            }
            else
            {
                ViewBag.Result = "You Have Already Added  this Product";
            }

            return View();

        }

        [Authorize]
        public ActionResult GetJobByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(x => x.UserId == UserId);
            return View(jobs.ToList());
        }

        public ActionResult UserJobsDetails(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [Authorize]
        public ActionResult GetJobsByEmployer()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = from tbl in db.ApplyForJobs
                       join job in db.Jobs on tbl.JobId equals 
                       job.Id where job.User.Id == UserId select tbl;


            var grouped = from j in Jobs
                          group j
           by j.job.JobTitle into
           gr
                          select new JobApplicationsViewModel
                          {
                              JobTitle = gr.Key,
                              items = gr

                          };

            return View(grouped.ToList());

        }

        public ActionResult EditUserJobs(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult EditUserJobs(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
            }

            return View(job);
        }


        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            var Myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(Myjob);
            db.SaveChanges();

            return RedirectToAction("GetJobByUser");

        }

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(x => x.JobTitle.Contains(searchName)
              || x.JobContent.Contains(searchName) || x.category.CategoryName.Contains(searchName)
              || x.category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel Contact)
        {
            var mail = new MailMessage();
            var logininfo = new NetworkCredential("basmaalinaserallah146@gmail.com", "12345678@");
            mail.From = new MailAddress(Contact.Email);
            mail.To.Add(new MailAddress("basmaalinaserallah146@gmail.com"));
            mail.Subject = Contact.Subject;
            mail.IsBodyHtml = true;
            string Body = "Sender Name: " + Contact.Name + "<br>" +
                "Email: " + Contact.Email + "<br>" + "Topic: " + Contact.Subject + "<br>" +
                "Message: " + Contact.Message;
            mail.Body = Body;

            var smtpclient = new SmtpClient("smtp.gmail.com", 587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = logininfo;
            smtpclient.Send(mail);
            return RedirectToAction("Index");
        }

        public ActionResult PublicSector()
        {
            var Public = db.Categories.Where(x => x.CategoryName.Contains("Mac OS Laptops")).ToList();
            return View(Public);
        }

        public ActionResult PrivateSector()
        {
            var Private = db.Categories.Where(x => x.CategoryName.Contains("Windows Os Laptops")).ToList();
            return View(Private);
        }

        public ActionResult PrivateAbroad()
        {
            var Privateabroad = db.Categories.Where(x => x.CategoryName.Contains("IPads")).ToList();
            return View(Privateabroad);
        }

        public ActionResult Miscellaneous()
        {
            var miscellaneous = db.Jobs.ToList();
            return View(miscellaneous);
        }
    }
}