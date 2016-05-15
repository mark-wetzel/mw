using mw.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;

namespace mw.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(ConfigurationSettings.AppSettings["EmailTo"]));  // replace with valid value 
                message.From = new MailAddress(ConfigurationSettings.AppSettings["EmailFrom"]);  // replace with valid value
                message.Subject = ConfigurationSettings.AppSettings["EmailSubject"];
                message.Body = string.Format(body, model.Name, model.Email, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = ConfigurationSettings.AppSettings["EmailUsername"],
                        Password = ConfigurationSettings.AppSettings["EmailPassword"]
                    };

                    smtp.Credentials = credential;
                    smtp.Host = ConfigurationSettings.AppSettings["Host"];
                    smtp.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"]);
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableSSL"]);
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }

            return View("Index", model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}