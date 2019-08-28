using System;
using System.IO;
using System.Net;
using BaiduFaceAI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BaiduFaceAITest
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

            var fdb = new FaceDb2("zaiwu");
            
            var pic = "https://i0.hdslb.com/bfs/album/f03be0b71c1521ec3495b74d861abe0591900bc1.jpg";
            //var ret = fdb.GetUserById("12589063");
            var client = new WebClient();
            // var bytes2 = client.DownloadData("https://i0.hdslb.com/bfs/album/008a16762c44577eb7e8bc05086d08c6ec402311.jpg");

            var ret = fdb.SereachUserByUrl(pic);

            // 
            Console.WriteLine(JsonConvert.SerializeObject(ret, Formatting.Indented));

            return;


            //var bytes = File.ReadAllBytes(@"..\..\..\2.jpg");
            var bytes = File.ReadAllBytes(@"..\..\..\1.webp");

            //var ai = new BaiduFaceAI.FaceDetect();
            //var ret = ai.DetectFromBytes(bytes);
            //// Console.WriteLine(JsonConvert.SerializeObject(ret, Formatting.Indented));

            //Console.WriteLine(ret.result.face_list[0].face_token);
            var fd3 = new BaiduFaceAI.FaceDetect();
            var ret4 = fd3.DetectFromTOKEN("cdd3be0426908371475157bbcf46ed2a");
            Console.WriteLine(JsonConvert.SerializeObject(ret4, Formatting.Indented));

            return;



            return;

            //var ai = new BaiduFaceAI.FaceDb();
            //var ret = ai.GetGroup();
            //Console.WriteLine(JsonConvert.SerializeObject(ret, Formatting.Indented));

            //ret = ai.GetUserList("abc");
            //Console.WriteLine(JsonConvert.SerializeObject(ret, Formatting.Indented));
        }

        private static void TestGetFromURL()
        {

            var fd = new BaiduFaceAI.FaceDetect();
            var ret2 = fd.DetectFromUri("https://i2.hdslb.com/bfs/archive/c7f927e65987ef87d29314a8f770653b29a44c0a.jpg");
            Console.WriteLine(JsonConvert.SerializeObject(ret2, Formatting.Indented));
        }
    }
}
