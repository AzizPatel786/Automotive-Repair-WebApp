using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Automotive_Repair_WebApp.ViewModels
{
    public class QuoteToEmailViewModel
    {
        [Required, Display(Name = "Your name")]
        public string Name { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }
        //public HttpPostedFileBase Upload { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }
        [Required]
        public string Subject { get; set; }
        public string CarModel { get; set; }


    }
}
