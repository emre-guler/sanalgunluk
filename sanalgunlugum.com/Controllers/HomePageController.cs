using sanalgunlugum.com.Models.Manager;
using sanalgunlugum.com.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanalgunlugum.com.Controllers
{
    public class HomePageController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                int userId =  Int16.Parse(Session["Id"].ToString());
                DatabaseContext db = new DatabaseContext();
                var books = db.Dailys.Where(x => x.UserId == userId).Select(x => new BookViewModel()
                {
                    Id = x.Id,
                    DailyTitle = x.DailyTitle,
                    Mood = x.Mood,
                    BookColor = x.BookColor
                });
                ViewBag.Count = db.Dailys.Where(x => x.UserId == userId).Count();
                return View(books);
            }
        }
        [HttpPost]
        public ActionResult Index(DateTime dateSearch)
        {
            if (Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                if(dateSearch != null)
                {
                    int userId = Int16.Parse(Session["Id"].ToString());
                    DatabaseContext db = new DatabaseContext();
                    var books = db.Dailys.Where(x => x.UserId == userId && DbFunctions.TruncateTime(x.DailyDate) == DbFunctions.TruncateTime(dateSearch)).Select(x => new BookViewModel()
                    {
                        Id = x.Id,
                        DailyTitle = x.DailyTitle,
                        Mood = x.Mood,
                        BookColor = x.BookColor
                    });
                    ViewBag.Count = db.Dailys.Where(x => x.UserId == userId).Count();
                    return View(books);
                }
                else
                {
                    return Json(new { task = "Bu alan boş bırakılamaz"});
                }
            }
        }
        [HttpPost]
        public ActionResult Exit(string Crypt)
        {
            if (Crypt == "logout")
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                return View();
            }
        }
    }
}