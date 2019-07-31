using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;



namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_Lj : UserControl
    {
        public UcZjtq_SJ_Lj()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if(AppConfig.getAppConfig().caseId_selected_working == -1)
            {
                MessageBox.Show("请首先选择案件,然后单击添加案件的按钮！");
                AppConfig.getAppConfig().caseId_selected_row = -1;
                Program.m_mainform.AddNewGjalAj();
                return;
            }
            #region
            //查看手机是否连接
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("adb devices");
            process.StandardInput.WriteLine("exit");
            process.StandardInput.AutoFlush = true;
            string Conoutput = process.StandardOutput.ReadToEnd();
            process.WaitForExit();//等待程序执行完退出进程
            process.Close();
            Regex rege = new Regex(" daemon not running. starting it now on port 5037");
            Match mat = rege.Match(Conoutput);

            if (mat.Success)

            {
                MessageBox.Show("请查看手机是否为USB调试模式", "提示");
                return;
            }
            else
            {
                //获取手机厂商
                System.Diagnostics.Process reprocess = new System.Diagnostics.Process();
                reprocess.StartInfo.FileName = "cmd.exe";
                reprocess.StartInfo.UseShellExecute = false;
                reprocess.StartInfo.RedirectStandardError = true;
                reprocess.StartInfo.RedirectStandardInput = true;
                reprocess.StartInfo.RedirectStandardOutput = true;
                reprocess.StartInfo.CreateNoWindow = true;
                reprocess.Start();
                reprocess.StandardInput.WriteLine("adb shell getprop ro.product.brand");
                reprocess.StandardInput.WriteLine("exit");
                reprocess.StandardInput.AutoFlush = true;
                string output = reprocess.StandardOutput.ReadToEnd();

                reprocess.WaitForExit();//等待程序执行完退出进程
                reprocess.Close();
                string[] split = new string[] { Environment.NewLine.ToString() };
                string[] Devicebranarrs = output.Split(split, StringSplitOptions.RemoveEmptyEntries);
                Program.m_mainform.DeviceBrand = Devicebranarrs[3];

                //获取手机型号

                System.Diagnostics.Process process1 = new System.Diagnostics.Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.RedirectStandardError = true;
                process1.StartInfo.RedirectStandardInput = true;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                process1.StandardInput.WriteLine("adb shell getprop ro.product.model");
                process1.StandardInput.WriteLine("exit");
                process1.StandardInput.AutoFlush = true;
                string output1 = process1.StandardOutput.ReadToEnd();

                process1.WaitForExit();//等待程序执行完退出进程
                process1.Close();
                string[] Devicemodelarrs = output1.Split(split, StringSplitOptions.RemoveEmptyEntries);
                Program.m_mainform.DeviceModel = Devicemodelarrs[3];
                Program.m_mainform.g_zjName = Program.m_mainform.DeviceModel + DateTime.Now.ToString("yyyyMMddHHmmss");

                //获取手机操作系统
                System.Diagnostics.Process pro = new System.Diagnostics.Process();
                pro.StartInfo.FileName = "cmd.exe";
                pro.StartInfo.UseShellExecute = false;
                pro.StartInfo.RedirectStandardError = true;
                pro.StartInfo.RedirectStandardInput = true;
                pro.StartInfo.RedirectStandardOutput = true;
                pro.StartInfo.CreateNoWindow = true;
                pro.Start();
                pro.StandardInput.WriteLine("adb shell getprop ro.build.version.release");
                pro.StandardInput.WriteLine("exit");
                pro.StandardInput.AutoFlush = true;
                string output2 = pro.StandardOutput.ReadToEnd();

                pro.WaitForExit();//等待程序执行完退出进程
                pro.Close();
                string[] arrs = output2.Split(split, StringSplitOptions.RemoveEmptyEntries);
                Program.m_mainform.Devicesystem = arrs[3];

                //查看手机ROOT权限
                System.Diagnostics.Process pre = new System.Diagnostics.Process();
                pre.StartInfo.FileName = "cmd.exe";
                pre.StartInfo.UseShellExecute = false;
                pre.StartInfo.RedirectStandardError = true;
                pre.StartInfo.RedirectStandardInput = true;
                pre.StartInfo.RedirectStandardOutput = true;
                pre.StartInfo.CreateNoWindow = true;
                pre.Start();
                pre.StandardInput.WriteLine("adb root");
                pre.StandardInput.WriteLine("exit");
                pre.StandardInput.AutoFlush = true;
                string Rootout = pre.StandardOutput.ReadToEnd();
                pre.WaitForExit();//等待程序执行完退出进程
                pre.Close();
                Regex reg = new Regex("adbd cannot run as root in production builds");
                Match m = reg.Match(Rootout);

                if (m.Success)

                {
                    Program.m_mainform.DeviceState = "未ROOT";
                }
                else
                    Program.m_mainform.DeviceState = "ROOT";
            }
            #endregion
            #region
            //跳转提取数据界面
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();         
            AppContext.GetInstance().m_ucZjtq_sj_ljcg.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_ljcg);
            #endregion
           

            #region
            ////获取手机型号用于创建证据目录
            //Process PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = Application.StartupPath + " 0";
            //Console.WriteLine(PreProcess.StartInfo.Arguments);
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\socketPbi.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();

            //string DeviceStateFilename = Application.StartupPath + "\\getState.txt";
            //if (File.Exists(DeviceStateFilename))
            //{
            //    string state = File.ReadAllText(DeviceStateFilename, Encoding.Default);
            //    string[] states = state.Split('"');
            //    Console.WriteLine(states[3]);
            //    Console.WriteLine(states[7]);
            //    Program.m_mainform.DeviceBrand = states[3];
            //    Program.m_mainform.DeviceModel = states[7];
            //    UcZjtq_SJ_Ljcg ljcg = new UcZjtq_SJ_Ljcg();
            //    ljcg.label2.Text = Program.m_mainform.DeviceBrand + "--" + Program.m_mainform.DeviceModel;
            //    Console.WriteLine(ljcg.label2.Text);
            //}

            //string filename = Application.StartupPath + "\\phoneModel.txt";
            ////判断目标文件是否存在
            //bool flag = File.Exists(filename);
            //if (flag)
            //{
            //    string Str = File.ReadAllText(filename, Encoding.Default);
            //    string[] sArray = Str.Split('/');

            //    Program.m_mainform.g_zjName = sArray[1].Replace("-", "") + DateTime.Now.ToString("yyyyMMddHHmmss");
            //    Program.m_mainform.g_workPath += "\\" + Program.m_mainform.g_ajName + "\\" + Program.m_mainform.g_zjName;

            //    File.Delete(filename);
                Program.m_mainform.g_workPath += "\\" + Program.m_mainform.g_ajName + "\\" + Program.m_mainform.g_zjName;
                //创建案件目录结构
                Directory.CreateDirectory(Program.m_mainform.g_workPath);
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppBackup");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppData");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppExtract");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\PhoneData");
          //  }

            #endregion

        }

   
    }
}
