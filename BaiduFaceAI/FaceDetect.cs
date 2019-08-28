using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BaiduFaceAI.Entity;
using Newtonsoft.Json;

namespace BaiduFaceAI
{
    /// <summary>
    /// 人脸检测：检测图片中的人脸并标记出位置信息
    /// </summary>
    /// <remarks>
    /// https://cloud.baidu.com/doc/FACE/index.html
    /// https://cloud.baidu.com/doc/FACE/s/Pjwvxqpcz/
    /// </remarks>
    public class FaceDetect: AIBase
    {
        public DetectReturn DetectFromBytes(byte[] bytes)
        {
            var client = new Baidu.Aip.Face.Face2(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            var image = Convert.ToBase64String(bytes);

            var imageType = "BASE64";

            // 调用人脸检测，可能会抛出网络等异常，请使用try/catch捕获
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"face_field", "age,beauty,expression,face_shape,gender,glasses,landmark,landmark72,landmark150,race,quality,eye_status,emotion,face_type"},
                {"max_face_num", 10},
                {"face_type", "LIVE"},
            };

            try
            {
                // 带参数调用人脸检测
                var result = client.Detect(image, imageType, options);

                return JsonConvert.DeserializeObject<DetectReturn>(result.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public DetectReturn DetectFromUri(string url)
        {
            var client = new Baidu.Aip.Face.Face2(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            // var url = "http://i2.hdslb.com/bfs/archive/c7f927e65987ef87d29314a8f770653b29a44c0a.jpg";
            var imageType = "URL";

            var options = new Dictionary<string, object>{
                {"face_field", "age,beauty,expression,face_shape,gender,glasses,landmark,landmark72,landmark150,race,quality,eye_status,emotion,face_type"},
                {"max_face_num", 10},
                {"face_type", "LIVE"},
            };
            var result = client.Detect(url, imageType, options);

            return JsonConvert.DeserializeObject<DetectReturn>(result.ToString());
        }

        public DetectReturn DetectFromTOKEN(string token)
        {
            var client = new Baidu.Aip.Face.Face2(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            var imageType = "FACE_TOKEN";

            // 调用人脸检测，可能会抛出网络等异常，请使用try/catch捕获
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"face_field", "age,beauty,expression,face_shape,gender,glasses,landmark,landmark72,landmark150,race,quality,eye_status,emotion,face_type"},
                {"max_face_num", 10},
                {"face_type", "LIVE"},
            };
            // 带参数调用人脸检测
            var result = client.Detect(token, imageType, options);

            // Console.WriteLine(result);
            // File.WriteAllText("../../../2.txt", result.ToString());

            return JsonConvert.DeserializeObject<DetectReturn>(result.ToString());
        }
    }
}
