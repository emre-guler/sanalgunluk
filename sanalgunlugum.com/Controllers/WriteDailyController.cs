using sanalgunlugum.com.Models;
using sanalgunlugum.com.Models.Manager;
using sanalgunlugum.com.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sanalgunlugum.com.Controllers
{
    public class WriteDailyController : Controller
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
                DatabaseContext db = new DatabaseContext();
                int userId = Int16.Parse(Session["Id"].ToString());
                var Friends = db.Friends.Where(x => x.UserId == userId).Select(x => new FriendTagViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.FullName,
                    PhotoWay = x.PhotoWay
                });
                ViewBag.Count = db.Friends.Where(x => x.UserId == userId).Count();
                return View(Friends);
            }
        }
        [HttpPost]
        public JsonResult Index(string title, string daily, string status, DateTime datepick, string bookColor, string friendTag)
        {
            string text;
            if (title != "" && title !=  " " && daily != "" && daily != " " && status != "" && status != " " && datepick != null && bookColor != "" && bookColor != " " && friendTag != null)
            {
                string moodselect = "/Content/uplodas/" + status + ".png";
                DatabaseContext db = new DatabaseContext();
                int uId= Convert.ToInt16(Session["Id"].ToString());
                Daily daytext = new Daily
                {
                    UserId = uId,
                    DailyTitle = title,
                    DailyText = daily,
                    Mood = moodselect,
                    DailyDeleteStatus = false,
                    DailyDate = datepick,
                    BookColor = bookColor,
                    FriendTag = friendTag,
                    CreatedAt = DateTime.Now
                };
                db.Dailys.Add(daytext);
                int save = db.SaveChanges();
                if(save == 1)
                {
                    text = "Success";
                }
                else
                {
                    text = "ServerErr";;
                }
            }
            else
            {
                text = "Err";
            }
            return Json(new { res = text});
        }
        [HttpGet]
        public ActionResult FileUpload()
        {
            if (Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase inputFile)
        {
            int id = Convert.ToInt16(Session["Id"].ToString());
            if (id != 0)
            {
                if (inputFile != null && inputFile.ContentLength > 0 && inputFile.ContentLength < 10485760)
                {
                    if (inputFile.ContentType == "image/jpeg" || inputFile.ContentType == "image/x-png" || inputFile.ContentType == "video/mpeg" || inputFile.ContentType == "video/x-msvideo") {
                        if (Directory.Exists(Server.MapPath("~/UserContent/" + id)) == false)
                        {
                            Directory.CreateDirectory(Server.MapPath("~/UserContent/" + id));
                        }
                        inputFile.SaveAs(Path.Combine(Server.MapPath("~/UserContent/" + id), inputFile.FileName));
                        DatabaseContext db = new DatabaseContext();
                        var dId = db.Dailys.Where(x => x.UserId == id).Select(x => x.Id).ToArray().Last();
                        Models.Files newFile = new Models.Files
                        {
                            DailyId = dId, 
                            UserId = id,
                            FileType = inputFile.ContentType,
                            FileName = inputFile.FileName,
                            FileWay = ("/UserContent/" + id + "/" + inputFile.FileName),
                            FileSize = (inputFile.ContentLength).ToString(),
                            UploadedAt = DateTime.Now
                        };
                        db.Files.Add(newFile);
                        int okSave = db.SaveChanges();
                        if (okSave >= 1)
                        {
                            ViewBag.First = "OkImg";
                        }
                        else
                        {
                            ViewBag.First = "NoImg";
                        }
                    }
                    else
                    {
                        ViewBag.First = "NoImgType";
                    }
                }
                else
                {
                    ViewBag.First = "NoImg";
                }
            }
            else
            {
                ViewBag.First = "Connect";
            }
            return View();
        }
        [HttpPost]
        public JsonResult AddFriend(string text, string fullname)
        {
            int id = Convert.ToInt16(Session["Id"].ToString());
            DatabaseContext db = new DatabaseContext();
            Friend person = new Friend
            {
                UserId = id,
                FullName = fullname,
                FriendText = text,
                PhotoWay = "/Content/Uplodas/defaultphoto.png",
                CreatedAt = DateTime.Now
            };
            db.Friends.Add(person);
            int control = db.SaveChanges();
            if(control == 1)
            {
                return Json(new { res = "Success"});
            }
            else
            {
                return Json(new { res = "Danger"});
            }
        }
    }
}