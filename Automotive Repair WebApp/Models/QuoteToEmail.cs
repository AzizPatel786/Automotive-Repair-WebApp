using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Automotive_Repair_WebApp.Models
{
    public class QuoteToEmail
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        //public HttpPostedFileBase Upload { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }
        [Required]
        public string Subject { get; set; }
        public IFormFile Upload { get; set; }
    }
}
