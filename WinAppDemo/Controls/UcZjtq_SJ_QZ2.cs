using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using WinAppDemo.Db.Base;
using System.Data.SQLite;

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ2 : UserControl
    {

        DateTime dt;    //记时

        public UcZjtq_SJ_QZ2()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_qz1.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_qz1);
        }

        private void UcZjtq_SJ_QZ2_Load(object sender, EventArgs e)
        {



            // progressBar1.Value = 30;
            //获取手机基本信息包括设备信息、手机短信、通讯录、通话记录
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.Arguments = Program.m_mainform.g_workPath + "\\PhoneData";
            Console.WriteLine(Process.StartInfo.Arguments);
            Process.StartInfo.FileName = Application.StartupPath + "\\installGetInfo.exe";
            Process.StartInfo.Verb = "runas";
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start();
            Process.WaitForExit();

            //MessageBox.Show("手机基本信息获取成功!", "提示");

            Process.Close();
            //多线程展示进度条
            string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\PhoneData\\PhoneData.db";   //打开短信、联系人、通话记录等数据库
            Console.WriteLine(dbPath);
            Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
            Program.m_mainform.g_conn.Open();
            SqliteDbContext db = new SqliteDbContext();

            SQLiteCommand cmdSelect = new SQLiteCommand("select count(*) FROM Sms", Program.m_mainform.g_conn);
            cmdSelect.ExecuteNonQuery();
            SQLiteDataReader reader = cmdSelect.ExecuteReader();
            while (reader.Read())
            {
                int SmsNum = reader.GetInt32(0);
                if (SmsNum > 0)
                {
                    progressBar1.Value = 25;
                    Console.WriteLine("进度条为25%");
                    label4.Text = "25%";

                }
            }

            SQLiteCommand cmdSelect1 = new SQLiteCommand("select count(*) FROM Calls", Program.m_mainform.g_conn);
            cmdSelect1.ExecuteNonQuery();
            SQLiteDataReader reader1 = cmdSelect1.ExecuteReader();
            while (reader1.Read())
            {
                int CallsNum = reader1.GetInt32(0);
                if (CallsNum > 0)
                {
                    Thread.Sleep(1000);
                    progressBar1.Value = 50;
                    Console.WriteLine("进度条为50%");
                    label4.Text = "50%";
                }
           }

            SQLiteCommand cmdSelect2 = new SQLiteCommand("select count(*) FROM Contacts", Program.m_mainform.g_conn);
            cmdSelect2.ExecuteNonQuery();
            SQLiteDataReader reader2 = cmdSelect2.ExecuteReader();
            while (reader2.Read())
            {
                int CallsNum = reader2.GetInt32(0);
                if (CallsNum > 0)
                {
                    Thread.Sleep(1000);
                    progressBar1.Value = 75;
                    Console.WriteLine("进度条为75%");
                    label4.Text = "75%";
                }
            }

            Program.m_mainform.g_conn.Close();


            //提取手机的tencent文件夹
            if (!File.Exists(Program.m_mainform.g_workPath + "//PhoneData//tencent"))
            {
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\PhoneData\\tencent");
            }
            System.Diagnostics.Process reProcess = new System.Diagnostics.Process();
            reProcess.StartInfo.Arguments = "sdcard/tencent" + " " + Program.m_mainform.g_workPath + "/PhoneData/tencent";
            Console.WriteLine(reProcess.StartInfo.Arguments);
            reProcess.StartInfo.FileName = Application.StartupPath + "\\pullFileFromPhone.exe";
            reProcess.StartInfo.Verb = "runas";
            reProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            reProcess.Start();
            reProcess.WaitForExit();
             Console.WriteLine("提取tencent结束");
            //MessageBox.Show("手机tencent信息获取成功!", "提示");

            reProcess.Close();
            Thread.Sleep(50000);
            progressBar1.Value = 100;
            Console.WriteLine("进度条为100%");
            label4.Text = "100%";
        }


        private void Button1_Click_1(object sender, EventArgs e)
        {
            IList<string> imglist = new List<string>();

            string exePath = System.Windows.Forms.Application.StartupPath;

            //imglist.Add(exePath + "/Images/test1.jpg");
            //imglist.Add(exePath + "/Images/test2.png");
            //imglist.Add(exePath + "/Images/test3.jpg");
            imglist.Add(exePath + "/Images/StartUSBDebug1.jpg");
            imglist.Add(exePath + "/Images/StartUSBDebug2.png");
            imglist.Add(exePath + "/Images/StartUSBDebug3.png");
            


            FormGjglZzqx form = new FormGjglZzqx(imglist);
            form.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {

            progressBar1.Value = 100;
            label2.Text = "获取完成";
            FormGjglZzqk form = new FormGjglZzqk();
            // form.ShowDialog();
            form.Show();
           
        }

       
    }
}
