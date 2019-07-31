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
            //卸载手机上已安装的APK
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("adb uninstall" + " " + "\\com.huawei.KoBackup");
            process.StandardInput.WriteLine("exit");
            process.StandardInput.AutoFlush = true;
            process.WaitForExit();//等待程序执行完退出进程
            process.Close();
            //安装华为手机自带备份APK
            Process reprocess = new System.Diagnostics.Process();
            reprocess.StartInfo.FileName = "cmd.exe";
            reprocess.StartInfo.UseShellExecute = false;
            reprocess.StartInfo.RedirectStandardError = true;
            reprocess.StartInfo.RedirectStandardInput = true;
            reprocess.StartInfo.RedirectStandardOutput = true;
            reprocess.StartInfo.CreateNoWindow = true;
            reprocess.Start();
            reprocess.StandardInput.WriteLine("adb install" + " " + Application.StartupPath + "\\com.huawei.KoBackup.apk");
            reprocess.StandardInput.WriteLine("exit");
            reprocess.StandardInput.AutoFlush = true;
            reprocess.WaitForExit();//等待程序执行完退出进程
            reprocess.Close();

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
            //安装华为手机自带备份APK
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("adb install" + " " + Application.StartupPath + "\\com.huawei.KoBackup.apk");
            process.StandardInput.WriteLine("exit");
            process.StandardInput.AutoFlush = true;
            process.WaitForExit();//等待程序执行完退出进程
            process.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.m_mainform.backupFileName = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                process.StandardInput.WriteLine("adb pull sdcard/Huawei/Backup/backupFiles/" + Program.m_mainform.backupFileName+" "+ Program.m_mainform.g_workPath+ "/Appbackup");
                process.StandardInput.WriteLine("exit");
                process.StandardInput.AutoFlush = true;
                string Conoutput = process.StandardOutput.ReadToEnd();
                process.WaitForExit();//等待程序执行完退出进程
                process.Close();
                Console.WriteLine(Conoutput);
                if(File.Exists(Program.m_mainform.g_workPath + "//Appbackup//com.tencnet.mm.db"))
                {
                    Console.WriteLine("存在微信备份文件");
                }
            }
            else
            {
                MessageBox.Show("不存在微信备份文件", "提示");
                return;
            }
        }

       
    }
}
