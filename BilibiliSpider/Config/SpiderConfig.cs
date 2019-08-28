using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BilibiliSpider.Config
{
    /// <summary>
    /// 爬虫的配置
    /// </summary>
    public class SpiderConfig
    {
        /// <summary>
        /// 默认路径
        /// </summary>
        public static string BasePath
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory; }
        }

        public static string GetPath(string folder)
        {
            var path= Path.Combine(BasePath, folder);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
