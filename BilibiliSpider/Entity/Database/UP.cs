using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.DataAnnotations;

namespace BilibiliSpider.Entity.Database
{
    /// <summary>
    /// Up主
    /// </summary>
    public class UP
    {
        /// <summary>
        /// Up 主的id ，应该都是数字的
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// 粉丝数量
        /// </summary>
        public int following { get; set; }

        /// <summary>
        /// 关注的人数
        /// </summary>
        public int follower { get; set; }

        /// <summary>
        /// 视频播放数量
        /// </summary>
        public int views { get; set; }

        /// <summary>
        /// 视频数量
        /// </summary>
        public int video { get; set; }

        /// <summary>
        ///     名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     性别
        /// 男 女 or 中间吗？
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        ///     头像图片
        ///     如果有的话
        /// </summary>
        public string face { get; set; }

        /// <summary>
        ///     签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        public int rank { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 入站时间
        /// </summary>
        public int jointime { get; set; }

    }
}
