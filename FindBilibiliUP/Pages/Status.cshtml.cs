using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilibiliSpider.DB;
using BilibiliSpider.Entity.Database;
using ServiceStack.OrmLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FindBilibiliUP.Pages
{
    public class StatusModel : PageModel
    {
        public void OnGet()
        {
            using (var db = DBSet.GetCon(DBSet.SqliteDBName.Bilibili))
            {
                VModel.AVCount = db.Count<AV>().ToString();
                VModel.UPCount = db.Count<UP>().ToString();
                VModel.ImageDetectCount =  db.Count<ImageDetect>().ToString();
                VModel.DbFaceCount = db.Count<ImageDetect>(o => o.AddToFaceDB).ToString();
            }
        }

        public ViewModel VModel = new ViewModel { };

        public class ViewModel
        {
            public string AVCount { get; set; }
            public string UPCount { get; internal set; }

            public string ImageDetectCount { get; set; }

            public string DbFaceCount { get; set; }
        }
    }
}