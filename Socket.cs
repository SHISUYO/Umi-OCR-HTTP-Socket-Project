/*-------------------------------------------------------------------------
# 本脚本由作者个人兴趣开发，与任何单位或工作项目无关。
# 脚本基于海康 VisionMaster 平台提供的公开脚本接口开发。
#
# 本脚本遵循 Apache License 2.0 开源许可发布：
# 
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Copyright 2025 SHISUYO
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
-------------------------------------------------------------------------*/

using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Script.Methods;
using Newtonsoft.Json;

public class OcrDataItem
{
    public string text { get; set; }
}

public class OcrResult
{
    public List<OcrDataItem> data { get; set; }
}

public partial class UserScript : ScriptMethods, IProcessMethods
{
    int processCount;

    public void Init()
    {
        processCount = 0;
    }

    public bool Process()
    {
        try
        {
            ImageData imageing = in1;
            int width = imageing.Width;
            int height = imageing.Height;
            string base64Image = "";
            PixelFormat format = PixelFormat.Format8bppIndexed;
            byte[] pixelData = imageing.Buffer;

            using (Bitmap bitmap = new Bitmap(width, height, format))
            {
                ColorPalette palette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = palette;

                BitmapData bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    format
                );

                int stride = bmpData.Stride;

                for (int y = 0; y < height; y++)
                {
                    IntPtr destPtr = IntPtr.Add(bmpData.Scan0, y * stride);
                    Marshal.Copy(pixelData, y * width, destPtr, width);
                }

                bitmap.UnlockBits(bmpData);

                // 保存为 PNG 或 JPEG
                
    			string filePath = @"C:\Users\SHISUYO\Desktop\ZIP\DLL\aaa.jpg";	//设置存图路径以及文件名字
    			bitmap.Save(filePath, ImageFormat.Jpeg);	//你要存JPG就改成ImageFormat.Jpeg，PNG就改成Png,BMP就改成Bmp，补全都有的
                
               /*
               		如果你就单单存图，那么代码到这里就可以了，下面的你可以直接多行注释注释掉了(仅可存黑白图像)
               		下面的都是将图片转换成Base64编码并且输出给服务器进行OCR识别的代码了
               		（题外话：这里的路径必须是真实存在的，否则会报错"GDI+中发生一般性错误"，不过我已经在上面引用了System.IO
					Directory.CreateDirectory方法我相信你肯定会用的吧？）
               */
				
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    base64Image = Convert.ToBase64String(ms.ToArray());
                    out1 = base64Image;
                }
            }

            // 设置OCR识别参数
            var postData = new
            {
                base64 = base64Image,
                options = new
                {
                    ocr_language = "models/config_chinese.txt",		//选择你要使用的模型
                    /*
						模型可选	"models/config_chinese.txt"、 		中文
								"models/config_en.txt"、 			English
								"models/config_chinese_cht(v2).txt"、中文二版 
								"models/config_japan.txt"、 			日本語
								"models/config_korean.txt"、 		한국어
								"models/config_cyrillic.txt"		Ελληνικό αλφάβητο
                     */
                    ocr_cls = false,
                    /*
                    	是否纠正文本方向
                    */
                    ocr_limit_side_len = 960,
                    /*
                    	限制图像边长，顾名思义，限制长宽的长
                    */
                    tbpu_parser = "multi_para",
                    /*
                    	禁止修改，除非你认为你知道这个是什么意思
                    */
                    data_format = "dict"
                    /*
                    	禁止修改，除非你认为你知道这个是什么意思
                    */	
                }
            };

            string url = "http://nas.shisuyo.top:41235/api/ocr";	//测试服务器,此服务器需要IPV6才可连接,本地就填127.0.0.1:1224/api/ocr
            string jsonData = JsonConvert.SerializeObject(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";

            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);
            request.ContentLength = byteData.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteData, 0, byteData.Length);
            }

            string responseContent;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseContent = reader.ReadToEnd();
            }

            /* 显示返回内容
            MessageBox.Show("返回内容：\n" + responseContent);
			*/
			
			/*
			   由于众所周知的原因，脚本的Console.Writeline是不可使用的，所以这里使用MessageBox来显示文本框信息
			   默认注释，如果感觉OCR给出的内容不太对劲，可以取消注释看看
			*/
			
            var result = JsonConvert.DeserializeObject<OcrResult>(responseContent);
            var texts = new StringBuilder();

            foreach (var item in result.data)
            {
                texts.AppendLine(item.text);
            }

            MessageBox.Show("识别到的文本内容：\n" + texts.ToString());

            out1 = texts.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show("出错：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return true;
    }
}
