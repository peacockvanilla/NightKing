using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_full_version.Models.Support
{
    public class ContactUsModel
    {
        public string Topic { get; set; }
        public string Description { get; set; }

        //public DateTime SendDate { get; set; }
        public RegisterViewModel userDetails { get; set; }

    }
}