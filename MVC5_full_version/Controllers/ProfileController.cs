using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVC5_full_version.Models;
using MVC5_full_version.Models.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static MVC5_full_version.Controllers.ManageController;
using CottonCandy.DLL;


namespace MVC5_full_version.Controllers
{
    
    public class ProfileController : Controller
    {
        public ProfileController() { }
        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                    HttpPostedFileBase filebase = new HttpPostedFileWrapper(pic);
                    var fileName = Path.GetFileName(filebase.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/UserUploadedAvatars/"), fileName);
                    filebase.SaveAs(path);
                    string userId = User.Identity.GetUserId();
                    ProfileUpdate.UploadImagePathToDB(userId,path);
                    return Json("File Saved Successfully.");
                }
                else { return Json("No File Saved."); }
            }
            catch (Exception ex) { return Json("Error While Saving." + ex.Message); }
        }



        //Save Details of New Password using Identity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
            //return PartialView("/Views/Profile/_EditPersonal")
        }


        private string GetUserInfo()
        {
            return User.Identity.GetUserId();
        }

        [HttpPost]
        public void UpdateUserInfo(EditPersonalDetailsViewModel model)
        {
           if(model != null)
            {
                string userId = GetUserInfo();
                ProfileUpdate.UpdateDatabase(userId,model.FirstName,model.LastName);
                
            }
        }
        [HttpPost]
        public JsonResult GetUserDetails()
        {
            string userId = GetUserInfo();
            var result =  ProfileUpdate.GetUserDetailsToEdit(userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }

}