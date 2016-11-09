using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;

namespace EmailAndFileProject.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _env;
        public HomeController(IHostingEnvironment env)
        {
            this._env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        #region Student

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Models.Student S)
        {
            string RootPathViaDI = this._env.ContentRootPath;
            string CVFinalPath = RootPathViaDI + "/UploadedData/CVs";
            string PathToSave = "";
            foreach (var file in Request.Form.Files)
            {

                string FileName = file.FileName;
                string FileExt = System.IO.Path.GetExtension(file.FileName);
                string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                PathToSave = CVFinalPath + DateTime.Now.ToString("ddMMyyyyhhmmss") + FileExt;
                System.IO.FileStream fs = new System.IO.FileStream(PathToSave, System.IO.FileMode.Create);
                file.CopyTo(fs);
                fs.Close();


            }

            var message = new MailMessage();
            message.From = new MailAddress("saad.ahmad920@gmail.com", "Saad Ahmad");
            message.To.Add(new MailAddress(S.Email));
            message.To.Add(new MailAddress("chocolatesaad7@gmail.com"));
            message.Subject = "This is Subject";
            message.Body = "<h1>Hi, " + S.FullName + "</h1><br><br>This is email body.<br><br>-----<br>ABC Website.";
            message.IsBodyHtml = true;

            message.Attachments.Add(new Attachment(PathToSave));


            SmtpClient SC = new SmtpClient();
            SC.Credentials = new System.Net.NetworkCredential("XXXXXXXXXXXX", "XXXXXXXXXXX");
            SC.Host = "smtp.gmail.com";
            SC.Port = 587;
            SC.EnableSsl = true;

            try
            {
                SC.Send(message);
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        #endregion
    }
}
