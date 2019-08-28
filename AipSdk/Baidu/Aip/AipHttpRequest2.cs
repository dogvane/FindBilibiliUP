/*
* Copyright 2017 Baidu, Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
* the License. You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
* an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
* specific language governing permissions and limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Baidu.Aip
{
    /// <summary>
    ///     Http请求包装
    /// </summary>
    public class AipHttpRequest2
    {
        public enum BodyFormat
        {
            Formed,
            Json,
            JsonRaw // 只取Body["RAW"]的内容
        }

        public const string BodyFormatJsonRawKey = "RAw";

        public Dictionary<string, object> Bodys;

        public BodyFormat BodyType;
        public Encoding ContentEncoding;

        public Dictionary<string, string> Headers;

        public string Method;
        public Dictionary<string, string> Querys;

        /// <summary>
        ///     不带query
        /// </summary>
        public Uri Uri;

        public string UriWithQuery
        {
            get
            {
                var query = Utils.ParseQueryString(Querys);
                return Uri + "?" + query;
            }
        }


        private AipHttpRequest2()
        {
            Headers = new Dictionary<string, string>();
            // 所有Url中附带aipSdk=CSharp参数
            Querys = new Dictionary<string, string> {{"aipSdk", "CSharp"}};
            Bodys = new Dictionary<string, object>();
            Method = "GET";
            BodyType = BodyFormat.Formed;
            ContentEncoding = Encoding.UTF8;
            System.Net.ServicePointManager.Expect100Continue = false;
        }


        public AipHttpRequest2(string uri) : this()
        {
            Uri = new Uri(uri);
        }

        public string ContentType
        {
            get
            {
                switch (BodyType)
                {
                    case BodyFormat.Formed:
                    {
                        return "application/x-www-form-urlencoded";
                    }
                    case BodyFormat.Json:
                    {
                        return "application/json";
                    }
                    case BodyFormat.JsonRaw:
                    {
                        return "application/json";
                    }
                }

                return string.Empty;
            }
        }

        public new byte[] ProcessHttpRequest(WebClient webRequest)
        {
            foreach (var header in Headers)
                webRequest.Headers.Add(header.Key, header.Value);

            switch (BodyType)
            {
                case BodyFormat.Formed:
                {
                    var body = Bodys.Select(pair => pair.Key + "=" + Utils.UriEncode(pair.Value.ToString()))
                        .DefaultIfEmpty("")
                        .Aggregate((a, b) => a + "&" + b);

                    webRequest.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    return ContentEncoding.GetBytes(body);
                }
                case BodyFormat.Json:
                {
                    var body = JsonConvert.SerializeObject(Bodys);
                    webRequest.Headers.Add("Content-Type", "application/json");
                    return ContentEncoding.GetBytes(body);
                }
                case BodyFormat.JsonRaw:
                {
                    var body = JsonConvert.SerializeObject(Bodys[BodyFormatJsonRawKey]);
                    webRequest.Headers.Add("Content-Type", "application/json");
                    return ContentEncoding.GetBytes(body);
                }
            }
            return null;
        }

        /// <summary>
        ///     生成AI的Web请求
        /// </summary>
        /// <param name="token"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public byte[] GenerateDevWebRequest(string token, int timeout = 0)
        {
            Querys.Add("access_token", token);
            var client = new WebClient();
            
            var body = ProcessHttpRequest(client);
            return client.UploadData(UriWithQuery, body);
        }
    }
}