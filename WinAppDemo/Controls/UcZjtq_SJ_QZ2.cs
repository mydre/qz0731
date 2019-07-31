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

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ2 : UserControl
    {
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
            progressBar1.Value = 30;
            //获取手机基本信息包括设备信息、手机短信、通讯录、通话记录
            System.Diagnostics.Process Process = new System.Diagnostics.Process();
            Process.StartInfo.Arguments = Program.m_mainform.g_workPath + "\\PhoneData";
            Console.WriteLine(Process.StartInfo.Arguments);
            Process.StartInfo.FileName = Application.StartupPath + "\\installGetInfo.exe";
            Process.StartInfo.Verb = "runas";
            Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start();
            Process.WaitForExit();

            MessageBox.Show("手机基本信息获取成功!", "提示");
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
            form.ShowDialog();
        }

       
    }
}
