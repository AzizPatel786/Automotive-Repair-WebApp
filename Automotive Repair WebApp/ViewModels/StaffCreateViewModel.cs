using Microsoft.AspNetCore.Http;
using Automotive_Repair_WebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Automotive_Repair_WebApp.ViewModels
{
    public class StaffCreateViewModel
    {
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public String Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Invalid email format")]
        [Display(Name = "School Email")]
        public String Email { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public String Department { get; set; }


        [Required]
        public String Occupation { get; set; }
        public IFormFile Photo { get; set; }
    }
}
