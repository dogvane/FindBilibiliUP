using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BilibiliSpider.Entity.Spider;
using Newtonsoft.Json;

namespace BilibiliSpider.Spider
{
    /// <summary>
    /// Up主数据采集
    /// </summary>
    public class UpSpider
    {
        public UPInfo.Data GetUpInfo(int mid)
        {
            // https://api.bilibili.com/x/space/acc/info?mid=85834123

            var url = $"https://api.bilibili.com/x/space/acc/info?mid={mid}";

            Console.WriteLine(url);

            var retData = new WebClient().DownloadString(url);

            // Console.WriteLine(retData);

            var ret = JsonConvert.DeserializeObject<UPInfo.Root>(retData);

            if (ret.code == 0)
            {
                return ret.data;
            }

            return null;
        }

        public UpVideo.Data GetUpVideo(int mid)
        {
            // https://space.bilibili.com/ajax/member/getSubmitVideos?mid=85834123&page=1&pagesize=25

            var url = $"https://space.bilibili.com/ajax/member/getSubmitVideos?mid={mid}&page=1&pagesize=100";

            Console.WriteLine(url);

            var retData = new WebClient().DownloadString(url);

            // Console.WriteLine(retData);

            var ret = JsonConvert.DeserializeObject<UpVideo.Root>(retData);

            if (ret.status == "true")
            {
                return ret.data;
            }

            return null;
        }

        /// <summary>
        /// 获得相册数据
        /// </summary>
        public void GetAlbum()
        {
            // https://api.bilibili.com/x/space/album/index?mid=85834123&ps=10
        }

        /// <summary>
        /// 获得粉丝的数据
        /// </summary>
        public UPFace.Data GetFaceStat(int mid)
        {
            // https://api.bilibili.com/x/relation/stat?vmid=85834123


            var url = $"https://api.bilibili.com/x/relation/stat?vmid={mid}";

            Console.WriteLine(url);

            var retData = new WebClient().DownloadString(url);

            // Console.WriteLine(retData);

            var ret = JsonConvert.DeserializeObject<UPFace.Root>(retData);

            if (ret.code == 0)
            {
                return ret.data;
            }

            return null;
        }

        /// <summary>
        /// 获得更新数据
        /// 貌似就播放量有用
        /// </summary>
        public UPStat.Data GetUpStat(int mid)
        {
            // https://api.bilibili.com/x/space/upstat?mid=85834123&jsonp=jsonp&callback=__jp4

            var url = $"https://api.bilibili.com/x/space/upstat?mid={mid}";

            Console.WriteLine(url);

            var retData = new WebClient().DownloadString(url);
            
            var ret = JsonConvert.DeserializeObject<UPStat.Root>(retData);

            if (ret.code == 0)
            {
                return ret.data;
            }

            return null;
        }
    }
}
