using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BilibiliSpider.Entity.Spider;
using Newtonsoft.Json;

namespace BilibiliSpider.Spider
{
    /// <summary>
    /// 更新最新的视频列表
    /// </summary>
    public class Newlist
    {
        /// <summary>
        /// 下载最新视频列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页数量，b站限制，不能超过50</param>
        public AVDataReturn Download(int page, int pageSize = 50, int rid=20)
        {
            var baseUrl = $"https://api.Bilibili.com/x/web-interface/newlist?&rid={rid}&type=1&pn={page}&ps={pageSize}";
            Console.WriteLine(baseUrl);

            var retData = new WebClient().DownloadString(baseUrl);

            // Console.WriteLine(retData);

            var ret = JsonConvert.DeserializeObject<NewListReturn>(retData);

            if (ret.code == 0)
            {
                return ret.data;
            }

            return null;
        }
    }
}
