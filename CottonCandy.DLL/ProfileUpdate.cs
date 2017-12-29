using CottonCandy.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.DLL
{
    public static class ProfileUpdate
    {
        private static StructDatabaseDevEntities2 db = new StructDatabaseDevEntities2();
        //Updates FirstName, LastName
        public static void UpdateDatabase(string userId,string Fn, string Ln )
        {
          
                //var userAccount = (from user in db.AspNetUsers
                //                   where user.Id == userId
                //                   select user).FirstOrDefault();

            AspNetUser user = db.AspNetUsers.SingleOrDefault(x => x.Id == userId);
            user.FirstName = Fn;
            user.LastName = Ln;
            db.SaveChanges();


            
        }
        
        public static bool CheckForEmail(string email)
        {
            // bool isExist = db.AspNetUsers.Where(x => x.Email.ToLowerInvariant().Equals(email.ToLowerInvariant())) != null;
            var abc = (from users in db.AspNetUsers
                      where users.Email.ToLower() == email.ToLower()
                      select users.Id).FirstOrDefault();

            return abc != null ? true : false;
           // return isExist;
        }

        public static bool CheckForUsername(string username)
        {
            var abc = (from users in db.AspNetUsers
                       where users.UserName.ToLower() == username.ToLower()
                       select users.Id).FirstOrDefault();

            return abc != null ? true : false;
        }

        public static object GetUserDetailsToEdit(string userName)
        {
            //var abc = (from users in db.AspNetUsers
            //           where users.Id == userName
            //           select users).FirstOrDefault();

            var abc = db.AspNetUsers.Where(x => x.Id == userName).Select(x => new { x.FirstName, x.LastName, x.Email, x.UserName });

            return abc;
        }

        public static void UploadImagePathToDB(string userId,string imagePath)
        {
            AspNetUser user = db.AspNetUsers.SingleOrDefault(x => x.Id == userId);
            user.UserImagePath = imagePath;
            db.SaveChanges();
        }
    }
}
