using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
namespace WinAppDemo.tools
{
    class ChangeAmrToMp3
    {
        private static int equal_prefix_index(string s1, string s2)
        {
            int len1, len2;
            len1 = s1.Length;
            len2 = s2.Length;
            int i = 0, j = -1;
            while (i < len1 && i < len2)
            {
                if (s1[i] != s2[i]) return j;
                j++;
                i++;
            }
            return j;
        }

        private static string path(string di_path)
        {
            int index = equal_prefix_index(Environment.CurrentDirectory, di_path);
            string post1 = "", post2;

            //Console.WriteLine((index + 1).ToString());
            //Console.WriteLine(Environment.CurrentDirectory.Length);
            if (index + 1 == Environment.CurrentDirectory.Length)
            {
                post1 = "";
                post2 = di_path.Substring(index + 1);
            }
            else
            {
                if (Environment.CurrentDirectory[index] != '\\')
                {
                    while (di_path[index] != '\\') { index--; }
                }
                post1 = Environment.CurrentDirectory.Substring(index + 1);
                post2 = di_path.Substring(index + 1);
            }
            //Console.WriteLine(post1);
            //Console.WriteLine(post2);
            if (post1 == "")
            {
                post2 = string.Format(@".{0}", post2);
            }
            else
            {
                string[] sArray = post1.Split('\\');
                int len = sArray.Length;
                string temp_s = @".\";
                for (int i = 0; i < len; i++)
                {
                    temp_s = string.Format(@"{0}..\", temp_s);
                }
                post2 = string.Format("{0}{1}", temp_s, post2);
            }
            string ss2 = "";

            int le2 = post2.Length;
            for (int i = 0; i < le2; i++)
            {
                if (post2[i] == '\\')
                {
                    ss2 += '/';
                }
                else
                {
                    ss2 += post2[i];
                }
            }
            //Console.WriteLine(post1);
            //Console.WriteLine(post2);
            //Console.WriteLine(ss2);
            return ss2;
        }

        private static string changeAmrToPcm(string di_amr)
        {
            int index = di_amr.LastIndexOf('.');
            return string.Format("{0}.{1}", di_amr.Substring(0, index), "pcm");
        }

        private static string changePcmToMp3(string di_pcm)
        {
            int index = di_pcm.LastIndexOf('.');
            return string.Format("{0}.{1}", di_pcm.Substring(0, index), "mp3");
        }

        private static void change(string str)
        {
            string currentWorkDir = Environment.CurrentDirectory;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            p.StandardInput.WriteLine(str + "&exit");
            p.StandardInput.AutoFlush = true;
            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            //MessageBox.Show(output);
            //Console.WriteLine(output);
        }

        public static void ChangeToMp3(string amr_path)//本类执行的入口
        {
            //string di_pcm = "";
            //string di_mp3 = "";
            string cmd_command1 = "";
            string cmd_command2 = "";
            //amr_path = path(amr_path);//变成相对路径，反斜杠变为斜杠
            //di_pcm = changeAmrToPcm(amr_path);
            //di_mp3 = changePcmToMp3(di_pcm);
            //cmd_command1 = string.Format("{0} {1} {2}", "silk_v3_decoder.exe", amr_path, di_pcm);//产生cmd可执行的命令
            //cmd_command2 = string.Format("{0} {1} {2}", "lame.exe -r -s 24000 --preset voice", di_pcm, di_mp3);

            int index = amr_path.LastIndexOf(".");
            string prefix = amr_path.Substring(0, index);
            string pcm_path = string.Format("{0}.{1}", prefix, "pcm");
            string mp3_path = string.Format("{0}.{1}", prefix, "mp3");
            //Console.WriteLine(amr_path+"\n"+pcm_path+"\n"+mp3_path);

            cmd_command1 = string.Format("{0} {1} {2}", "silk_v3_decoder.exe", amr_path, pcm_path);//产生cmd可执行的命令
            cmd_command2 = string.Format("{0} {1} {2}", "lame.exe -r -s 24000 --preset voice", pcm_path, mp3_path);

            Task t1 = new Task(() =>
            {
                change(cmd_command1);
            });
            Task t2 = new Task(() =>
            {
                Thread.Sleep(50);
                change(cmd_command2);
            });
            t1.Start();
            t2.Start();
        }
    }
}
