using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Controls;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace WinAppDemo.Forms
{
    public partial class FormGjglBf : Form
    {

        public FormGjglBf()
        {
            InitializeComponent();
        }


        private void FormGjglBf_Load(object sender, EventArgs e)
        {
            Process PreProcess = new Process();
            PreProcess = null;
            PreProcess = new Process();
            PreProcess.StartInfo.Arguments = "sdcard/Huawei/Backup/backupFiles " + " " + Program.m_mainform.g_workPath + "\\mm.db";
            Console.WriteLine(PreProcess.StartInfo.Arguments);
            PreProcess.StartInfo.FileName = Application.StartupPath + "\\getAllFilesName.exe";
            PreProcess.StartInfo.Verb = "runas";
            PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            PreProcess.Start();
            PreProcess.WaitForExit();
            Console.WriteLine("开始输出备份文件目录");

          
        }
        private void FormGjglBf_Shown(object sender, EventArgs e)
        {
            //从安装路径去存放备份文件名的数据库
            dataGridView1.AutoGenerateColumns = false;
            string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\mm.db";
            Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
            Program.m_mainform.g_conn.Open();
            SQLiteCommand com = Program.m_mainform.g_conn.CreateCommand();
            com.CommandText = "select id ,name,size,choose from file_name";
            SQLiteDataReader sr1 = com.ExecuteReader();
            while (sr1.Read())
            {
                dataGridView1.Rows.Add(sr1[0], sr1[1], sr1[2], sr1[3]);
            }
            Program.m_mainform.g_conn.Close();

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            IList<string> imglist = new List<string>();
            Program.m_mainform.IsBackup = true;        //新建备份

            string exePath = System.Windows.Forms.Application.StartupPath;

            //imglist.Add(exePath + "/Images/test1.jpg");
            //imglist.Add(exePath + "/Images/test2.png");
            //imglist.Add(exePath + "/Images/test3.jpg");
            imglist.Add(exePath + "/Images/backup1.png");
            imglist.Add(exePath + "/Images/backup2.png");
            imglist.Add(exePath + "/Images/backup3.jpg");
            imglist.Add(exePath + "/Images/backup4.jpg");
            imglist.Add(exePath + "/Images/backup5.jpg");
            imglist.Add(exePath + "/Images/backup6.jpg");



            UcZjtq_SJ_QZ5 m_ucZjtq_sj_qz5 = new UcZjtq_SJ_QZ5(imglist);


            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            m_ucZjtq_sj_qz5.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(m_ucZjtq_sj_qz5);

            this.Close();
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.m_mainform.backupFileName = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dataGridView1.CurrentRow.Cells[3].Style.ForeColor = Color.Red;
            MessageBoxButtons messBtn = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("点击确认选择之前的备份文件" + Program.m_mainform.backupFileName + "\n\t或点击取消重新新建备份", "提示:确认", messBtn);
            if (dr == DialogResult.OK)
            {
                //adb提取备份文件

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
                Console.WriteLine(Conoutput);

                if (File.Exists(Program.m_mainform.g_workPath + "//AppBackup//com.tencnet.mm.db"))
                {
                    if (File.Exists(Program.m_mainform.g_workPath + "//AppBackup//com.tencent.mm.db"))
                    {
                        MessageBox.Show("获取备份完成!", "提示");
                        Console.WriteLine("存在微信备份文件");
                    }
                    else
                    {
                        MessageBox.Show("不存在微信备份文件", "提示");
                        return;
                    }
                }

                this.Close();
                //跳转到数据提取界面并执行微信备份解析程序
                UcZjtq_SJ_QZ6 m_ucZjtq_sj_qz6 = new UcZjtq_SJ_QZ6();


                AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

                m_ucZjtq_sj_qz6.Dock = DockStyle.Fill;
                AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
                AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(m_ucZjtq_sj_qz6);
               // m_ucZjtq_sj_qz6.startGetData();   

            }

        }
       
    }
}
