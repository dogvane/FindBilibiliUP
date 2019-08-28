using System;
using System.Collections.Generic;
using System.Text;
using BilibiliSpider.Entity.Spider;
using ServiceStack.DataAnnotations;

namespace BilibiliSpider.Entity.Database
{
    /// <summary>
    /// 字面意思
    /// </summary>
    public class AV
    {
        /// <summary>
        /// av id 应该都是数字的
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// up主的id，
        /// </summary>
        public int UpId { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// av下有多少个视频
        /// </summary>
        public int videos { get; set; }

        /// <summary>
        /// 创建时间（应该是首次上传的时间吧）
        /// </summary>
        public int ctime { get; set; }

        /// <summary>
        /// 创建时间（应该是首次上传的时间吧）
        /// </summary>
        public string ctime2 { get; set; }

        /// <summary>
        /// 骗流量的封面照
        /// </summary>
        public string pic { get; set; }

        /// <summary>
        /// 版权类型
        /// </summary>
        public int copyright { get; set; }

        /// <summary>
        /// 视频状态
        /// </summary>
        public Stat stat { get; set; }

        /// <summary>
        /// 将观影人数列出来
        /// </summary>
        public int view { get; set; }

        /// <summary>
        /// 板块id
        /// 宅舞 20
        /// 三次元 154,
        /// </summary>
        public int rid { get; set; }
    }
}
