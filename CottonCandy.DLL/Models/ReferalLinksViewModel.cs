using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonCandy.DLL.Models
{
    public class ReferalLinksViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }

    public class NetworkViewModel
    {
        public List<ReferalLinksViewModel> firstLevelReferalModel { get; set; }
        public List<ReferalLinksViewModel> secondLevelReferalModel { get; set; }
        public List<ReferalLinksViewModel> thirdLevelReferalModel { get; set; }
       // public List<ReferalLinksViewModel> fourthLevelReferalModel { get; set; }
    }
}
