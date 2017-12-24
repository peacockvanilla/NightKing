using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5_full_version.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC5_full_version.Models.Profile
{
    public class EditPersonalDetailsViewModel 
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

    }
}