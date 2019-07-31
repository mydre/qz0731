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
            //获取 短信、联系人、通话记录
            //Process PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = Program.m_mainform.g_workPath + " 1";
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\socketPbi.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();

            //PreProcess = null;
            //PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = Program.m_mainform.g_workPath + " 2";
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\socketPbi.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();

            //PreProcess = null;
            //PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = Program.m_mainform.g_workPath + " 3";
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\socketPbi.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();


            ////提微信备份目录
            //PreProcess = null;
            //PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = "sdcard/Huawei/Backup/backupFiles "+Program.m_mainform.g_workPath + "\\mm.db";
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\getAllFilesName.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();

            ////提微信备份
            //PreProcess = null;
            //PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = "sdcard/Huawei/Backup/backupFiles/2019-06-12_15-01-06/com.tencent.mm.db " + Program.m_mainform.g_workPath + "\\AppBackup";
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\pullFileFromPhone.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();


            // MessageBox.Show("此处运行微信中转程序！", "提示", MessageBoxButtons.OKCancel);

            //Process PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = "D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546";  //全路径
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\BackupConvert.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();

            //PreProcess.WaitForExit();

            //MessageBox.Show("接着运行微信中转程序！", "提示", MessageBoxButtons.OKCancel);
            ////MessageBox.Show(Application.StartupPath, "提示", MessageBoxButtons.OKCancel);

            //PreProcess = null;
            //PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = "D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546";  //全路径
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\OnLineRes.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();

            //PreProcess.WaitForExit(); 

            // Thread.Sleep(10000);
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
            FormGjglZzqkWc from = new FormGjglZzqkWc();
            from.ShowDialog();
        }
    }
}
