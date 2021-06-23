using Automotive_Repair_WebApp.Data;
using Automotive_Repair_WebApp.Models;
using Automotive_Repair_WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Automotive_Repair_WebApp.Controllers
{
    public class StaffsController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        public StaffsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "Staff,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await db.Staffs.ToListAsync());
        }
        [Authorize(Roles = "Staff,Admin")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await db.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);

            var staffViewModel = new StaffViewModel()
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Experience = staff.Experience,
                Department = staff.Department,
                Occupation = staff.Occupation,
                ExistingImage = staff.ProfilePicture
            };

            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Staff staff = new Staff
                {
                    
                    Name = model.Name,
                    Email = model.Email,
                    Experience = model.Experience,
                    Department = model.Department,
                    Occupation = model.Occupation,
                    ProfilePicture = uniqueFileName
                };

                db.Add(staff);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await db.Staffs.FindAsync(id);
            var staffViewModel = new StaffViewModel()
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Experience = staff.Experience,
                Department = staff.Department,
                Occupation = staff.Occupation,
                ExistingImage = staff.ProfilePicture
            };

            if (staff == null)
            {
                return NotFound();
            }
            return View(staffViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffViewModel model)
        {
            if (ModelState.IsValid)
            {
                var staff = await db.Staffs.FindAsync(model.Id);
                staff.Name = model.Name;
                staff.Email = model.Email;
                staff.Experience = model.Experience;
                staff.Department = model.Department;
                staff.Occupation = model.Occupation;

                if (model.StaffPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    staff.ProfilePicture = ProcessUploadedFile(model);
                }
                db.Update(staff);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await db.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);

            var staffViewModel = new StaffViewModel()
            {
                Id = staff.Id,
                Name = staff.Name,
                Email = staff.Email,
                Experience = staff.Experience,
                Department = staff.Department,
                Occupation = staff.Occupation,
                ExistingImage = staff.ProfilePicture
            };
            if (staff == null)
            {
                return NotFound();
            }

            return View(staffViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await db.Staffs.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", staff.ProfilePicture);
            db.Staffs.Remove(staff);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(StaffViewModel model)
        {
            string uniqueFileName = null;

            if (model.StaffPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StaffPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.StaffPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}