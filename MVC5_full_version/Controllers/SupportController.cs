using MVC5_full_version.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void ContactUs(ContactUsModel contactUsModel)
        {
            if(contactUsModel != null)
            {

            }
        }
    }
}