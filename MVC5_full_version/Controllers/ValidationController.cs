using CottonCandy.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_full_version.Controllers
{
    public class ValidationController : Controller
    {
        //To check only EmpName   
        [AllowAnonymous]
        [HttpPost]
        public JsonResult IsEmailExists(string Email)
     {
            try
            {
               bool isExist =  ProfileUpdate.CheckForEmail(Email);
                return Json(!isExist, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        //To check only Username   
        [AllowAnonymous]
        [HttpPost]
        public JsonResult IsUsernameExists(string username)
        {
            try
            {
                bool isExist = ProfileUpdate.CheckForUsername(username);
                return Json(!isExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}