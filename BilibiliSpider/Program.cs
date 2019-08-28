using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading;
using BaiduFaceAI;
using BaiduFaceAI.Entity;
using BilibiliSpider.Common;
using BilibiliSpider.Config;
using BilibiliSpider.DB;
using BilibiliSpider.Entity.Database;
using BilibiliSpider.Entity.Spider;
using BilibiliSpider.Spider;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceStack.OrmLite;

namespace BilibiliSpider
{
    class Program
    {

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", true);

            var config = builder.Build();
            AIBase.APP_ID = config["BaiduAI:APP_ID"];
            AIBase.API_KEY = config["BaiduAI:API_KEY"];
            AIBase.SECRET_KEY = config["BaiduAI:SECRET_KEY"];

            Console.WriteLine("Hello World!");

            DBSet.Init();

            SpiderRun.DownZaiwu(0, 3, 154);
            // SpiderRun.Do();
            return;

            SpiderRun.DetectFace(50);
            SpiderRun.AddUPToFaceDb(20);
        }
    }

    public class SpiderRun
    {
        public static void Do()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            int start = 10;
            DateTime next = DateTime.Now.AddMinutes(10);
            while (!isExit)
            {
                DownZaiwu(0, 3, 20);
                DownZaiwu(0, 3, 154);
                DetectFace(50);
                if (isExit)
                    break;

                // DownZaiwu(start, start + 10);
                // start += 10;
                AddUPToFaceDb(20);

                if (!isExit && next > DateTime.Now)
                {
                    var sec = (next - DateTime.Now).TotalSeconds;
                    Console.WriteLine($"sleep {sec} sec");
                    Thread.Sleep((int)(sec * 1000));
                    next = DateTime.Now.AddMinutes(10);
                }
            }
        }

        private static bool isExit = false;

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            isExit = true;
        }

        public static void DoAllList()
        {
            DownZaiwu(0, 5780, 154);
        }

        public static void AddUPToFaceDb(int count = 100)
        {
            try
            {
                AddUPToFaceDb2(count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// 将upo主添加到人脸数据库里
        /// </summary>
        public static void AddUPToFaceDb2(int count = 100)
        {
            var faceDb = new FaceDb2("zhaiwu");

            using (var db = DBSet.GetCon(DBSet.SqliteDBName.Bilibili))
            {
                var images = db.Select<ImageDetect>(o => o.face_num == 1 && o.max_quality > 0.5 && o.AddToFaceDB == false);

                foreach (var item in images)
                {
                    if (isExit)
                        break;

                    //var imagePath = SpiderConfig.GetPath($"imgs/{item.UpId}/{item.AVId}");

                    //var imageBytes = File.ReadAllBytes(Path.Combine(imagePath, item.LocalFile));

                    var ret = faceDb.AddUserByURL(item.UpId.ToString(), item.Url);
                    var errorCode = ret["error_code"].ToString();
                    if (errorCode == "223105" || errorCode == "222210")
                    {
                        item.AddToFaceDB = true;
                        db.Update(item);
                    }

                    if (ret["error_code"].ToString() == "0")
                    {
                        item.AddToFaceDB = true;
                        db.Update(item);
                        Console.WriteLine($"Add:{item.UpId}");
                    }

                    Console.WriteLine(JsonConvert.SerializeObject(ret, Formatting.Indented));
                    Thread.Sleep(500);

                    if (count-- < 0)
                        return;
                }
            }
        }

        public static bool FromWeb = true;

        public static void DetectFace(int maxGetCount = 60 * 2 * 30) // 30分钟的数据)
        {
            try
            {
                DetectFace2(maxGetCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 将视频封面照，拿去百度检查
        /// </summary>
        private static void DetectFace2(int maxGetCount = 60 * 2 * 30) // 30分钟的数据)
        {
            var baiduai = new FaceDetect();
            int i = 0;
            DateTime nextCallTime = DateTime.Now;

            using (var db = DBSet.GetCon(DBSet.SqliteDBName.Bilibili))
            {
                foreach (var up in db.Select<UP>(o => o.follower > 3000).OrderByDescending(o => o.follower).ToArray())
                {
                    foreach (var av in db.Select<AV>(o => o.UpId == up.Id))
                    {
                        if (isExit)
                            break;

                        var pic = new Uri(av.pic).AbsolutePath.Replace("/", "_");

                        // 只按照本地文件名做验证
                        var detect = db.Single<ImageDetect>(o => o.LocalFile == pic);

                        if (detect == null)
                        {
                            byte[] bytes = null;

                            if (FromWeb)
                            {
                                try
                                {
                                    bytes = new WebClient().DownloadData(av.pic);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                            else
                            {
                                // 封面照落地，根据目前采集到数据，如果将宅舞区的封面照落地的话，估计要100多G
                                // 再加上三次元区，估计服务器硬盘干不动
                                var imagePath = SpiderConfig.GetPath($"imgs/{av.UpId}/{av.Id}");

                                var imageFile = Path.Combine(imagePath, pic);
                                if (!File.Exists(imageFile))
                                    continue;

                                bytes = File.ReadAllBytes(imageFile);
                            }

                            if (bytes == null)
                                continue;

                            var wait = (int)(nextCallTime - DateTime.Now).TotalMilliseconds + 1;

                            if (wait > 0)
                            {
                                Console.WriteLine($"wait {wait}");
                                Thread.Sleep(wait);
                            }

                            var start = DateTime.Now;
                            var ret = baiduai.DetectFromBytes(bytes);
                            Console.Write($"useTime:{ (DateTime.Now - start).TotalMilliseconds} ms ");
                            nextCallTime = DateTime.Now.AddMilliseconds(500);

                            if (ret != null)
                            {
                                var dbItem = new ImageDetect
                                {
                                    AVId = av.Id,
                                    UpId = av.UpId,
                                    LocalFile = pic,                                    
                                    Url = av.pic,
                                    Detect = ret.result,
                                };

                                if (ret.error_code == 0)
                                {
                                    dbItem.face_num = ret.result.face_num;
                                    if (ret.result.face_num > 0)
                                    {
                                        dbItem.max_face_probability = ret.result.face_list.Max(o => o.face_probability);
                                        dbItem.max_quality = ret.result.face_list.Max(o => GetQuality(o));
                                    }
                                }

                                db.Insert(dbItem);

                                Console.WriteLine(av.title);
                                if (maxGetCount-- < 0)
                                    return;

                                // Thread.Sleep(500); // 百度的免费接口只有 2 qps，所以在这里做一下延迟。
                            }
                        }
                        else
                        {
                            // Console.WriteLine("忽略 " + av.title);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得人脸的置信度
        /// 从0到1
        /// 0表示完全不可信
        /// </summary>
        /// <returns></returns>
        static double GetQuality(Face_listItem item)
        {
            // 规则见 https://cloud.baidu.com/doc/FACE/s/Rjwvxqjp9#%E4%BA%BA%E8%84%B8%E6%B3%A8%E5%86%8C

            var faceQuality = item.quality;
            Angle angle = item.angle;

            if (faceQuality.occlusion.chin_contour > 0.6)
                return 0;

            if (faceQuality.occlusion.right_cheek > 0.8)
                return 0;
            if (faceQuality.occlusion.left_cheek > 0.8)
                return 0;
            if (faceQuality.occlusion.mouth > 0.7)
                return 0;
            if (faceQuality.occlusion.nose > 0.7)
                return 0;
            if (faceQuality.occlusion.right_eye > 0.6)
                return 0;
            if (faceQuality.occlusion.left_eye > 0.6)
                return 0;

            if (faceQuality.blur > 0.7)
                return 0;

            if (faceQuality.illumination < 40)
                return 0;

            if (faceQuality.completeness == 0)
                return 0;

            if (angle.pitch > 20)
                return 0;
            if (angle.roll > 20)
                return 0;
            if (angle.yaw > 20)
                return 0;

            if (item.location.width < 100)
                return 0;

            if (item.location.height < 100)
                return 0;

            return 1 - faceQuality.blur;
        }

        public static void DownZaiwu(int start = 0, int maxPage = 10, int rid=20)
        {
            for (var i = start; i < maxPage; i++)
            {
                try
                {
                    var ret = GetZhaiWuNewList(i, rid);
                    if (isExit)
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(1000 * 5);
                    i--;
                }
            }
        }

        public static bool GetZhaiWuNewList(int page = 0, int rid=20)
        {
            var ret = new Newlist().Download(page, 50, rid);
            bool hasNew = false;

            using (var historyCon = DBSet.GetCon(DBSet.SqliteDBName.History))
            {
                using (var con = DBSet.GetCon(DBSet.SqliteDBName.Bilibili))
                {
                    foreach (var item in ret.archives)
                    {
                        var dbItem = con.SingleById<AV>(item.aid);

                        if (dbItem == null)
                        {
                            dbItem = new AV()
                            {
                                Id = item.aid,
                                UpId = item.owner.mid,
                                rid = rid,
                            };

                            Console.WriteLine(item.title);
                            UpdateAVData(dbItem, item);

                            con.Insert(dbItem);
                            // 更新封面照片
                            //var imagePath = SpiderConfig.GetPath($"imgs/{dbItem.UpId}/{dbItem.Id}");

                            //var pic = new Uri(item.pic).AbsolutePath.Replace("/","_");
                            //var imageFile = Path.Combine(imagePath, pic);

                            //if (!File.Exists(imageFile))
                            //{
                            //    Console.WriteLine(imageFile);
                            //    var bytes = new WebClient().DownloadData(item.pic);
                            //    File.WriteAllBytes(imageFile, bytes);
                            //}

                            hasNew = true;
                        }
                        else
                        {
                            UpdateAVData(dbItem, item);
                            con.Update(dbItem);
                        }

                        var upItem = con.SingleById<UP>(item.owner.mid);

                        if (upItem == null)
                        {
                            var upspider = new UpSpider();
                            int mid = item.owner.mid;
                            Console.WriteLine($"get up {mid}");

                            var info = upspider.GetUpInfo(mid);
                            var face = upspider.GetFaceStat(mid);
                            var upstat = upspider.GetUpStat(mid);

                            upItem = new UP
                            {
                                Id = mid,
                                follower = face.follower,
                                following = face.following,
                                face = info.face,
                                jointime = info.jointime,
                                level = info.jointime,
                                name = info.name,
                                rank = info.rank,
                                sex = info.sex,
                                sign = info.sign,
                                views = upstat.archive.view,
                            };

                            con.Insert(upItem);
                        }

                        var dbHistoryItem = historyCon.SingleById<AVHistory>(item.aid);

                        if (dbHistoryItem == null)
                        {
                            dbHistoryItem = new AVHistory
                            {
                                Id = item.aid,
                                History = new List<ArchivesItem> { item }
                            };
                            historyCon.Insert(dbHistoryItem);
                        }
                        else
                        {
                            dbHistoryItem.History.Add(item);
                            historyCon.Update(dbHistoryItem);
                        }
                    }
                }
            }

            return hasNew;
        }

        private static void UpdateAVData(AV dbItem, ArchivesItem item)
        {
            dbItem.title = item.title;
            dbItem.videos = item.videos;
            dbItem.ctime = item.ctime;
            dbItem.ctime2 = item.ctime.UnixToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            dbItem.pic = item.pic;
            dbItem.stat = item.stat;
            dbItem.copyright = item.copyright;
            dbItem.view = item.stat.view;
        }
    }
}
