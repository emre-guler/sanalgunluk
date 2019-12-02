using sanalgunlugum.com.Models.Manager;
using sanalgunlugum.com.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanalgunlugum.com.Controllers
{
    public class ReadDailyController : Controller
    {
        [HttpGet]
        public ActionResult Index(string id)
        {
            if (Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                int userId = Int16.Parse(Session["Id"].ToString());
                int routeValue = Convert.ToInt16(id);
                DatabaseContext db = new DatabaseContext();
                var control = db.Dailys.Where(x => x.UserId == userId && x.Id == routeValue).SingleOrDefault();
                if (control == null)
                {
                    return RedirectToAction("Index", "SignIn");
                }
                else
                {
                    var readDaily = db.Dailys.Where(x => x.UserId == userId && x.Id == routeValue).Select(x => new ReadDailyViewModel()
                    {
                        Id = x.Id,
                        DailyTitle = x.DailyTitle,
                        DailyText = x.DailyText,
                        DailyDate = x.DailyDate
                    });
                    return View(readDaily);
                }
            }
        }
    }
}