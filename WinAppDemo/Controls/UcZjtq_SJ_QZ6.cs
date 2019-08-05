using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using WinAppDemo.Forms;
using System.Data.SQLite;
using System.IO;

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ6 : UserControl
    {        
        System.Timers.Timer timer;  //7.8
        private int m_Timer = 0;
        private int m_Hour = 0;//时
        private int m_Minute = 0;//分
        private int m_Second = 0;//秒

        public delegate void SetControlValue(string value);

        public UcZjtq_SJ_QZ6()
        {
            InitializeComponent();

            //设置定时间隔(毫秒为单位)  7.8
            int interval = 1000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerUp);

            timer.Start();
        }

        public void startGetData()    //7.8
        {
   
            MessageBox.Show("数据提取中，请耐心等待！", "提示", MessageBoxButtons.OKCancel);
            Process PreProcess = new Process();
            PreProcess.StartInfo.Arguments = Program.m_mainform.g_workPath;
            PreProcess.StartInfo.FileName = Application.StartupPath + "\\BackupConvert.exe";
            PreProcess.StartInfo.Verb = "runas";
            PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            PreProcess.Start();

            PreProcess.WaitForExit();


            Console.WriteLine("开始提取微信中转数据");
            PreProcess = null;
            Process reProcess = new Process();
            reProcess.StartInfo.Arguments = Program.m_mainform.g_workPath;
            reProcess.StartInfo.FileName = Application.StartupPath + "\\OnLineRes.exe";
            reProcess.StartInfo.Verb = "runas";
            reProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            reProcess.Start();

            reProcess.WaitForExit();

            Thread.Sleep(10000);

            MessageBox.Show("提取完成!", "提示");

            //timer.Stop();
        }

        private void TimerUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                m_Timer += 1;                
                m_Second = m_Timer;
                if (m_Second > 59)
                {
                    m_Second = m_Timer - m_Minute * 60;
                    if (m_Second == 60) m_Second = 0;
                }
                m_Minute = (int)(m_Timer / 60);
                if (m_Minute > 59)
                {
                    m_Minute = m_Minute - m_Hour * 60;
                    if (m_Minute == 60) m_Minute = 0;
                }
                m_Hour = (int)(m_Minute / 60);
                if (m_Hour >= 24)
                {
                    m_Timer = 0;
                }
                string str = string.Format("{0:d2}:{1:d2}:{2:d2}", m_Hour, m_Minute, m_Second);
               
                this.Invoke(new SetControlValue(SetTextBoxText), str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行定时到点事件失败:" + ex.Message);
            }
        }
        private void SetTextBoxText(string strValue)
        {
             this.textBox1.Text = string.Format("{0:d2}:{1:d2}:{2:d2}", m_Hour, m_Minute, m_Second);
            
        }
         private void Button3_Click(object sender, EventArgs e)
        {
            timer.Stop();
            FormGjglZzqkWc form = new FormGjglZzqkWc();
            form.ShowDialog();
        }

        private void OnLineScan_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                progressBar1.Value += progressBar1.Step;
                if (progressBar1.Value >= 100)
                    progressBar1.Value = 0;
                this.Refresh();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        //需要拷贝
        private void Send()
        {
            int i = 0;
            while (i <= 100)
            {
                //显示进度信息
                this.ShowPro(i);
                i++; //模拟发送多少
                Thread.Sleep(1000);
                string Dir_WXmd5 = "";
                DirectoryInfo root = new DirectoryInfo("D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin");//Program.m_mainform.g_workPath +
                if (root.Exists)
                {
                    foreach (DirectoryInfo d in root.GetDirectories())
                    {
                        if (d.Name != "")
                        {
                            Dir_WXmd5 = d.Name;
                            break;
                        }
                    }
                }
                if (System.IO.File.Exists("D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\" + Dir_WXmd5 + "\\midwxtrans.db")) //Program.m_mainform.g_workPath +
                {
                    if (System.IO.File.Exists("D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\" + Dir_WXmd5 + "\\midwxtrans.db-journal")) //Program.m_mainform.g_workPath + 
                    {
                        if (i > 100)
                            i = 0;
                    }
                    else
                    {
                        this.ShowPro(100);
                        MessageBox.Show("数据提取完成！", "提示");
                        break;
                    }
                }
                else
                {
                    if (i > 100)
                        i = 0;
                }
            }
            Thread.CurrentThread.Abort();
        }
        private delegate void ProgressBarShow(int i);
        private void ShowPro(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProgressBarShow(ShowPro), value);
            }
            else
            {
                this.progressBar1.Value = value;
                this.label4.Text = value + "% Processing...";
                try
                {
                    m_Timer += 1;
                    m_Second = m_Timer;
                    if (m_Second > 59)
                    {
                        m_Second = m_Timer - m_Minute * 60;
                        if (m_Second == 60) m_Second = 0;
                    }
                    m_Minute = (int)(m_Timer / 60);
                    if (m_Minute > 59)
                    {
                        m_Minute = m_Minute - m_Hour * 60;
                        if (m_Minute == 60) m_Minute = 0;
                    }
                    m_Hour = (int)(m_Minute / 60);
                    if (m_Hour >= 24)
                    {
                        m_Timer = 0;
                    }
                    string str = string.Format("{0:d2}:{1:d2}:{2:d2}", m_Hour, m_Minute, m_Second);

                    this.Invoke(new SetControlValue(SetTextBoxText), str);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("执行定时到点事件失败:" + ex.Message);
                }
            }
        }

        private void UcZjtq_SJ_QZ6_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100;//设置最大长度值
            progressBar1.Value = 0;    //设置当前值
            progressBar1.Step = 5;      //设置每次增长多少

            //判断是否新建备份
            if (Program.m_mainform.IsBackup == true)
            {
                Process PreProcess = new Process();
                PreProcess = null;
                PreProcess = new Process();
                PreProcess.StartInfo.Arguments = "sdcard/Huawei/Backup/backupFiles " + " " + Program.m_mainform.g_workPath + "\\newmm.db";
                Console.WriteLine(PreProcess.StartInfo.Arguments);
                PreProcess.StartInfo.FileName = Application.StartupPath + "\\getAllFilesName.exe";
                PreProcess.StartInfo.Verb = "runas";
                PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                PreProcess.Start();
                PreProcess.WaitForExit();
                Thread.Sleep(1000);

                Console.WriteLine("输出新的备份文件目录");
                string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\newmm.db";
                Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
                Program.m_mainform.g_conn.Open();
                SQLiteCommand com = Program.m_mainform.g_conn.CreateCommand();
                com.CommandText = "select name from file_name order by name asc";
                SQLiteDataReader sr = com.ExecuteReader();
                
                while (sr.Read())
                {
                    //Convert.ToDateTime(sr[0]);
                    Program.m_mainform.backupFileName = sr[0].ToString();
                    Console.WriteLine(sr[0]);
                }

                Console.WriteLine(Program.m_mainform.backupFileName);
                Program.m_mainform.g_conn.Close();

                Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.WriteLine("adb pull sdcard/Huawei/Backup/backupFiles/" + Program.m_mainform.backupFileName + " " + Program.m_mainform.g_workPath + "/AppBackup");
                process.StandardInput.WriteLine("exit");
                process.StandardInput.AutoFlush = true;
                string Conoutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();//等待程序执行完退出进程
                process.Close();
                Thread.Sleep(10000);
                Console.WriteLine(Conoutput);
               
            }
            startGetData();

            Thread thread = new Thread(new ThreadStart(Send)); //模拟进度条
            thread.IsBackground = true;
            thread.Start();

        }
    }
}
