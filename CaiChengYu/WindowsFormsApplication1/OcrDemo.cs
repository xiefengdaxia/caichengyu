using System;
using System.Collections.Generic;
using System.IO;
using Baidu.Aip.Ocr;
using Newtonsoft.Json.Linq;

namespace Baidu.Aip.Demo
{
    internal class OcrDemo
    {
        private const string api = "lGQWAZSAfKBPIAMo3thpPu6h";
        private const string key = "u0uUGtiqtM70HTGqNd3DS2ztqyGCvKSq";
        public static string GeneralBasic(string path)
        {
            string resultStr = "";
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 通用文字识别
            var result = client.GeneralBasic(image);
            //var result = client.Accurate(image);
            //var result = client.General(image);
            if (result.Property("words_result") == null || result.Property("words_result").ToString() == "")
            {
                Console.WriteLine("键值key不存在！");
                return result.ToString();
            }
            else
            {
                var words = result["words_result"];
                foreach (var item in words)
                {
                    resultStr += item["words"];
                }
            }
            return resultStr;
            // 图片url
            //result = client.GeneralBasic("https://www.baidu.com/img/bd_logo1.png");
        }

        public static void GeneralEnhanced(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 带生僻字版
            var result = client.GeneralEnhanced(image);
        }

        public static void GeneralWithLocatin(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 带位置版本
            //var result = client.GeneralWithLocatin(image, null);
        }

        public static void WebImage(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 网图识别
            var result = client.WebImage(image, null);
        }

        public static void Accurate(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 高精度识别
            var result = client.Accurate(image);
        }

        public static void AccurateWithLocation(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 高精度识别(带位置信息)
            //var result = client.AccurateWithLocation(image);
        }


        public static void BankCard(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            // 银行卡识别
            //var result = client.BankCard(image);
        }

        public static void Idcard(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);

            var options = new Dictionary<string, object>
            {
                {"detect_direction", "true"} // 检测方向
            };
            // 身份证正面识别
            //var result = client.IdCardFront(image, options);
            // 身份证背面识别
            //result = client.IdCardBack(image);
        }

        public static void DrivingLicense(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);
            var result = client.DrivingLicense(image);
        }

        public static void VehicleLicense(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);
            var result = client.VehicleLicense(image);
        }

        public static void PlateLicense(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);
            //var result = client.PlateLicense(image);
        }

        public static void Receipt(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);
            var options = new Dictionary<string, object>
            {
                {"recognize_granularity", "small"} // 定位单字符位置
            };
            var result = client.Receipt(image, options);
        }


        public static void BusinessLicense(string path)
        {
            var client = new Ocr.Ocr(api, key);
            var image = File.ReadAllBytes(path);
            var result = client.BusinessLicense(image);
        }

        //public static void FormBegin(string path)
        //{
        //    var form = new Form(api, key);
        //    var image = File.ReadAllBytes(path);
        //    form.DebugLog = false; // 是否开启调试日志

        //    var result = form.BeginRecognition(image);
        //    Console.Write(result);
        //}

        //public static void FormGetResult(string path)
        //{
        //    var form = new Form(api, key);
        //    var options = new Dictionary<string, object>
        //    {
        //        {"result_type", "json"} // 或者为excel
        //    };
        //    var result = form.GetRecognitionResult("123344", options);
        //    Console.Write(result);
        //}

        //public static void FormToJson(string path)
        //{
        //    var form = new Form(api, key);
        //    var image = File.ReadAllBytes(path);
        //    form.DebugLog = false; // 是否开启调试日志

        //    // 识别为Json
        //    var result = form.RecognizeToJson(image);
        //    Console.Write(result);
        //}

        //public static void FormToExcel(string path)
        //{
        //    var form = new Form(api, key);
        //    var image = File.ReadAllBytes(path);
        //    form.DebugLog = false; // 是否开启调试日志

        //    // 识别为Excel
        //    var result = form.RecognizeToExcel(image);
        //    Console.Write(result);
        //}
    }
}