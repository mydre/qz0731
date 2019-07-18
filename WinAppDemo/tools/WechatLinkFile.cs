using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
namespace WinAppDemo.tools
{
    class WechatLinkFile
    {
        public static string display_file(string path_filename)
        {
            if (!File.Exists(path_filename))//首要任务：判断文件是否存在
            {
                return "文件不存在！";
            }

            //string str = @"D:\Test - 副本\手机数据取证系统\a.b\c.d";
            string[] sArray = path_filename.Split('.');
            int len = sArray.Length;

            //Console.WriteLine(sArray[len-1]);
            string postfix = sArray[len - 1];
            if (postfix == "doc" || postfix == "docx")
            {
                try
                {
                    System.Diagnostics.Process.Start(path_filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("打开文件异常：" + ex.ToString());
                    return ex.ToString();
                }
            }
            else if (postfix == "amr")
            {//是微信语音文件，需要转换成mp3格式的文件，然后进行播放
                int index = path_filename.LastIndexOf('.');
                string s1;
                s1 = path_filename.Substring(0, index);
                //s2 = path_filename.Substring(index + 1);

                string path_mp3 = string.Format("{0}.{1}", s1, "mp3");
                if (!File.Exists(path_mp3))
                {
                    ChangeAmrToMp3.ChangeToMp3(path_filename);
                }
                int i = 0;
                while (!File.Exists(path_mp3))
                {
                    if (i++ > 300) break;
                    Thread.Sleep(50);
                }
                try
                {
                    System.Diagnostics.Process.Start(path_mp3);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("打开文件异常：" + ex.ToString());
                    return ex.ToString();
                }
            }
            else//其他格式的文件可以使用系统调用的方式进行播放
            {
                try
                {
                    System.Diagnostics.Process.Start(path_filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("打开文件异常：" + ex.ToString());
                    return ex.ToString();
                }
            }
            return "";
        }
        public static string display_url(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("打开url异常：" + ex.ToString());
                return ex.ToString();
            }
            return "";
        }
    }
}
