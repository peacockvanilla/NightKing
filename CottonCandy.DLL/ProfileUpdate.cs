using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.DLL
{
    public static class ProfileUpdate
    {
        public static void UpdateDatabase(string userId)
        {
            using(StructDatabaseDevEntities db = new StructDatabaseDevEntities())
            {
                var userAccount = (from user in db.AspNetUsers
                                   where user.Id == userId
                                   select user).FirstOrDefault();


            }
        }
        private static StructDatabaseDevEntities db = new StructDatabaseDevEntities();
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
    }
}
