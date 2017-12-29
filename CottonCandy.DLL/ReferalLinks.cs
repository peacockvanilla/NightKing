using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CottonCandy.DLL.Models;

namespace CottonCandy.DLL
{
    public static class ReferalLinks
    {
        //Demo 
        public static object GetReferal(string refCode)
        {
            StructDatabaseDevEntities2 db = new StructDatabaseDevEntities2();
                var firstLevel =  db.AspNetUsers.Where(x => x.ReferalCode == refCode).Select(y => new
                ReferalLinksViewModel()
                {
                    Email = y.Email,
                    Username = y.UserName
                   
                }).ToList();
            
            return firstLevel;
        }

        public static List<ReferalLinksViewModel> GetFirstLevelReferal(string refCode)
        {
            StructDatabaseDevEntities2 db = new StructDatabaseDevEntities2();
            List<ReferalLinksViewModel> refLevel = db.AspNetUsers.Where(x => x.ReferalCode == refCode).Select(y => new
                ReferalLinksViewModel()
            {
                Email = y.Email,
                Username = y.UserName

            }).ToList();

            return refLevel;
        }

        public static List<ReferalLinksViewModel> GetSecondLevelReferal(string refCode1, string refCode2)
        {
            StructDatabaseDevEntities2 db = new StructDatabaseDevEntities2();
            List<ReferalLinksViewModel> refLevel = db.AspNetUsers.Where(x => x.ReferalCode == refCode1 || x.ReferalCode == refCode2).Select(y => new
                ReferalLinksViewModel()
            {
                Email = y.Email,
                Username = y.UserName

            }).ToList();

            return refLevel;
        }

        public static List<ReferalLinksViewModel> GetThirdLevelReferal(string refCode1,string refCode2, string refCode3)
        {
            StructDatabaseDevEntities2 db = new StructDatabaseDevEntities2();
            List<ReferalLinksViewModel> refLevel = db.AspNetUsers.Where(x => x.ReferalCode == refCode1 || x.ReferalCode == refCode2 || x.ReferalCode == refCode3).Select(y => new
                ReferalLinksViewModel()
            {
                Email = y.Email,
                Username = y.UserName

            }).ToList();

            return refLevel;
        }

    }
}
