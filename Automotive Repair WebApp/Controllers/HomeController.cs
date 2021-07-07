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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using Automotive_Repair_WebApp.Helpers;

namespace Automotive_Repair_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IStaffRepository _staffRepository;
        private readonly AppDbContext db;
        private IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, AppDbContext context,
                                 IWebHostEnvironment hostEnvironment, IConfiguration _configuration)
        {
            configuration = _configuration;
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

        [HttpGet]
        public IActionResult Getaquote()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Getaquote(QuoteToEmailViewModel quotetoemail, IFormFile[] attachments)
        {
            if (ModelState.IsValid) { 
                    var body = "Name: " + quotetoemail.Name + "<br>Email: " + quotetoemail.Email + "<br>Car Model: " + quotetoemail.CarModel + "<br>Phone: " + quotetoemail.MobilePhone + "<br>Message: " + quotetoemail.Content + "<br>";
            var mailHelper = new MailHelper(configuration);
            List<string> fileNames = null;
            if (attachments != null && attachments.Length > 0)
            {
                fileNames = new List<string>();
                foreach (IFormFile attachment in attachments)
                {
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "uploads", attachment.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        attachment.CopyToAsync(stream);
                    }
                    fileNames.Add(path);
                }
            }
            if (mailHelper.Send(quotetoemail.Email, configuration["Gmail:Username"], quotetoemail.Subject, body, fileNames))
            {
                ViewBag.msg = "Sent Mail Successfully";
            }
            else
            {
                ViewBag.msg = "Failed";
            }
            }
            return View(quotetoemail);
           

        }
        public ActionResult Sent()
        {
            return View();
        }
        //public IActionResult Error()
        //{
        //    return View();
        //}






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
