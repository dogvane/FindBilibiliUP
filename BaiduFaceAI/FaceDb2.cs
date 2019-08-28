using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BaiduFaceAI
{
    /// <summary>
    /// 脸数据库
    /// </summary>
    public class FaceDb2:AIBase
    {
        private string groupId = "";

        public FaceDb2(string groupId)
        {
            this.groupId = groupId;
        }

        Baidu.Aip.Face.Face2 client = new Baidu.Aip.Face.Face2(API_KEY, SECRET_KEY);


        public JObject GetGroup()
        {
            var groups = client.GroupGetlist();

            return groups;
        }

        public JObject GetUserList(string groupid)
        {
            if (string.IsNullOrEmpty(groupid))
                groupid = groupId;

            var groups = client.GroupGetusers(groupid);

            return groups;
        }

        /// <summary>
        /// 通过token增加
        /// 能不用就不用吧，因为token有时限的
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JObject AddUserByToken(string userId, string token)
        {
            var imageType = "FACE_TOKEN";

            var ret = client.UserAdd(token, imageType, groupId, userId);

            return ret;
        }

        public JObject AddUserByImage(string userId, byte[] bytes)
        {
            var image = Convert.ToBase64String(bytes);

            var imageType = "BASE64";

            var ret = client.UserAdd(image, imageType, groupId, userId);

            return ret;
        }

        /// <summary>
        /// URL地址是本地服务器自己去拿
        /// 百度ai接口拿图片是有点问题的
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public JObject AddUserByURL(string userId, string url)
        {
            var webclient = new WebClient();
            var bytes = webclient.DownloadData(url);

            return AddUserByImage(userId, bytes);
        }

        public JObject GetUserById(string userId)
        {
            var ret = client.UserGet(userId, groupId);

            return ret;
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public JObject SereachUserByImage(byte[] bytes)
        {
            var image = Convert.ToBase64String(bytes);

            var imageType = "BASE64";

            var options = new Dictionary<string, object>{
                {"face_field", "age,beauty,expression,face_shape,gender,glasses,landmark,landmark72,landmark150,race,quality,eye_status,emotion,face_type"},
                {"max_user_num", 3},
            };

            var ret = client.Search(image, imageType, groupId, options);

            return ret;
        }


        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public JObject SereachUserByUrl(string url)
        {
            try
            {
                var webclient = new WebClient();
                var bytes = webclient.DownloadData(url);

                return SereachUserByImage(bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                var ret = new JObject();
                ret["error_code"] = -1;
                ret["error_msg"] = e.Message;
                return ret;
            }
            //var client = new Baidu.Aip.Face.Face2(API_KEY, SECRET_KEY);
            //client.Timeout = 60000; // 修改超时时间

            //var imageType = "URL";

            //var ret = client.Search(url, imageType, groupid);

            //return ret;
        }
    }
}
