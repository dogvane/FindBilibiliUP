using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaiduFaceAI;
using BilibiliSpider.DB;
using BilibiliSpider.Entity.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.OrmLite;

namespace FindBilibiliUP.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            this.ViewData["error"] = -1;
            return null;
        }

        FaceDb faceDb = new FaceDb("zaiwu");

        public void OnPost(List<IFormFile> files, string imageUrl)
        {
            long size = 0;
            this.ViewData["error"] = -1;

            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var stream = file.OpenReadStream();
                size = stream.Length;
                var bytes = new byte[size];
                stream.Read(bytes, 0, (int) size);

                var ret = faceDb.SereachUserByImage(bytes);

                if ((int) ret["error_code"] != 0)
                {
                    this.ViewData["error"] = -1;
                    // ErrorMessage = JsonConvert.SerializeObject(ret, Formatting.Indented);
                    ErrorMessage = ret["error_msg"].ToString();
                    return;
                }

                // 能找到对应的人脸
                ErrorMessage = JsonConvert.SerializeObject(ret, Formatting.Indented);
            }
            else if (!string.IsNullOrEmpty(imageUrl))
            {
                var ret = faceDb.SereachUserByUrl(imageUrl);

                if ((int) ret["error_code"] != 0)
                {
                    this.ViewData["error"] = -1;
                    // ErrorMessage = JsonConvert.SerializeObject(ret, Formatting.Indented);
                    ErrorMessage = ret["error_msg"].ToString();
                    return;
                }

                // ErrorMessage = JsonConvert.SerializeObject(ret, Formatting.Indented);

                List<UPModel> ups = new List<UPModel>();

                var arr = ret["result"]["user_list"] as JArray;

                if (arr != null && arr.Count > 0)
                {
                    using (var db = DBSet.GetCon(DBSet.SqliteDBName.Bilibili))
                    {
                        foreach (var item in arr)
                        {
                            var id = int.Parse(item["user_id"].ToString());
                            var dbItem = db.SingleById<UP>(id);

                            if (dbItem != null)
                            {
                                ups.Add(new UPModel
                                {
                                    UPId = id.ToString(),
                                    Name = dbItem.name,
                                    FaceUrl = dbItem.face,
                                    Rate = ((double) item["score"]).ToString("f2")
                                });
                            }
                        }
                    }
                }

                FindUP = ups.ToArray();
            }
            else
            {
                ErrorMessage = "上传图片或者url，你总得设置一个吧";
            }
        }

        public string ErrorMessage { get; set; }

        public class UPModel
        {
            public string UPId { get; set; }

            public string Name { get; set; }

            public string Rate { get; set; }

            public string FaceUrl { get; set; }
        }

        public UPModel[] FindUP { get; set; }

        [HttpPost]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            return RedirectToPage("./Privacy");
        }
    }
}
