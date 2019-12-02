using sanalgunlugum.com.Models;
using sanalgunlugum.com.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
// using MVCEmail.Models;
// using System.Net;
// using System.Net.Mail;

namespace sanalgunlugum.com.Controllers
{
    public class SignUpController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Id"] == null || Session["Fullname"] == null || Session["Email"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "HomePage");
            }
        }
        [HttpPost]
        public ActionResult Index(string FullName, string EMailAddress, string Password, string PasswordAgain)
        {
            if(FullName != " " && EMailAddress != " " && Password != " " && PasswordAgain != " " &&  FullName != "" && EMailAddress != "" && Password != "" && PasswordAgain != "")
            {
                if(Password == PasswordAgain)
                {
                    DatabaseContext db = new DatabaseContext();
                    var EmailControl = db.Users.Where(x => x.EmailAddress == EMailAddress).SingleOrDefault();
                    if(EmailControl == null)
                    {
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        string pass = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(Password)));
                        User usr = new User
                        {
                            FullName = FullName,
                            EmailAddress = EMailAddress,
                            Password = pass,
                            CreatedAt = DateTime.Now
                        };
                        db.Users.Add(usr);
                        int save = db.SaveChanges();
                        if (save == 1)
                        {
                            /*
                            var mailBody = "<p>" + FullName +" günlük ailemize hoşgeldin. https://www.sanalgunlugum.com/signin adresinden  hesabına giriş yapıp, günlüklerini yazabilir ve anılarını kaydedebilrisin. <br> Sanalgunlugum.com</p>";
                            var message = new MailMessage();
                            message.To.Add(new MailAddress(EMailAddress));
                            message.From = new MailAddress("info@sanalgunlugum.com");
                            message.Subject = "Sanalgunlugum.com - Üyelik Oluşturma";
                            message.IsBodyHtml = true;
                            using(var smtp = new SmtpClient())
                            {
                                var credential = new NetworkCredential
                                {
                                    UserName = "info@sanalgunlugum.com",
                                    Password = "sifre123"
                                };
                                smtp.Credentials = credential;
                                smtp.Host = "smtp.sanalgunlugum.com";
                                smtp.Port = 587;
                                smtp.EnableSsl = true;
                                smtp.SendMailAsync(message);
                            }
                            */
                            ViewBag.First = "Success";
                            return View();
                        }
                        else
                        {
                            ViewBag.First = "ServerError" ;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.First = "UsedMail";
                        return View();
                    }
                }
                else{
                    ViewBag.First = "PassErr";
                    return View();
                }
            }
            else
            {
                ViewBag.First = "Req";
                return View();
            }
        }
    }
}   