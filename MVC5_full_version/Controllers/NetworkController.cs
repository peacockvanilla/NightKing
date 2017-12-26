using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CottonCandy.DLL;
using CottonCandy.DLL.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace MVC5_full_version.Controllers
{
    public class NetworkController : Controller
    {
        // GET: Network
        public ActionResult Main()
        {
            return View();   
        }

        public ActionResult FirstLevelRef()
        {
            var user = User.Identity.GetUserName();
            List<ReferalLinksViewModel> res = ReferalLinks.GetFirstLevelReferal(user);

            return Json(res, JsonRequestBehavior.AllowGet);
           // return PartialView("/Views/Network/_ReferalLinksViewPartial.cshtml", res);
        }

       

    }
}