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

        [HttpGet]
        public IActionResult Getaquote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Getaquote(QuoteToEmailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(vm.FromEmail);//Email which you are getting 
                                                         //from contact us page 
                    msz.To.Add("m.azizp23@gmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("m.azizp23@gmail.com", "MohammeddO4567");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

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
