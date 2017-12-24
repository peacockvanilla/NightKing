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
            using(DashBoardClientBetaEntities db = new DashBoardClientBetaEntities())
            {
                var userAccount = (from user in db.AspNetUsers
                                   where user.Id == userId
                                   select user).FirstOrDefault();


            }
        }
    }
}
