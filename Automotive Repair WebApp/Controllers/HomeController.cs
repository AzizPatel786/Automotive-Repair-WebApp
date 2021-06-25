using Automotive_Repair_WebApp.Models;
using Automotive_Repair_WebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Automotive_Repair_WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Automotive_Repair_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IStaffRepository _staffRepository;
        private readonly AppDbContext db;

        private readonly IWebHostEnvironment webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, AppDbContext context,
                                 IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            db = context;
            //_staffRepository = staffRepository;
            webHostEnvironment = hostEnvironment;


        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Aboutus()
        {
            //var model = _staffRepository.GetAllStaff();
            //return View(model); //Returns all the staff
            return View(await db.Staffs.ToListAsync());
        }

        public IActionResult Tyres()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Getaquote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Getaquote(QuoteToEmail model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("ac98844@avcol.school.nz"));  // replace with valid value 
                message.From = new MailAddress("ac98844@avcol.school.nz");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "ac98844@avcol.school.nz",  // replace with valid value
                        Password = "MohammeddO4567"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.office365.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }


        public ActionResult Sent()
        {
            return View();
        }






        public IActionResult Wof()
        {
            return View();
        }
        public IActionResult LoginRegister()
        {
            return View();
        }
        public IActionResult chk()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
