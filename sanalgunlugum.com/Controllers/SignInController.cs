using sanalgunlugum.com.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace sanalgunlugum.com.Controllers
{
    public class SignInController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if(Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "HomePage");
            }
        }
        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            if (email != "" && email != " " && password != "" && password != " ")
            {
                DatabaseContext db = new DatabaseContext();
                SHA1 sha = new SHA1CryptoServiceProvider();
                string pass = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                var dbRequest = db.Users.Where(x => x.EmailAddress == email && x.Password == pass).SingleOrDefault();
                if(dbRequest != null)
                {
                    Session["Id"] = dbRequest.Id;
                    Session["Fullname"] = dbRequest.FullName;
                    Session["Email"] = dbRequest.EmailAddress;
                    Session.Timeout = 40;
                    return RedirectToAction("Index", "Homepage");
                }
                else
                {
                    ViewBag.First = "Err";
                    return View();
                }
            }
            else
            {
                ViewBag.First = "Err";
                return View();
            }

        }
    }
}