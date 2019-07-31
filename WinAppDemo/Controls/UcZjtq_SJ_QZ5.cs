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
using System.Diagnostics;

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ5 : UserControl
    {
        private IList<string> m_imgList = null;
        private int imgPos = 0;
        private int imgCount = 0;

        public UcZjtq_SJ_QZ5(IList<string> imglist)
        {
            InitializeComponent();

            if (imglist == null)
            {
                string exePath = System.Windows.Forms.Application.StartupPath;

                imglist.Add(exePath + "/Images/backup1.png");
                imglist.Add(exePath + "/Images/backup2.png");
                imglist.Add(exePath + "/Images/backup3.jpg");
                imglist.Add(exePath + "/Images/backup4.jpg");
                imglist.Add(exePath + "/Images/backup5.jpg");
                imglist.Add(exePath + "/Images/backup6.jpg");
            }

            m_imgList = imglist;
        }

        //private void UcZjtq_SJ_QZ4_Load(object sender, EventArgs e)
        //{

        //}

        private void init()
        {
            if (imgCount > 0)
            {
                if (imgPos < 1)
                {
                    btnLeft.BackgroundImage = Properties.Resources.left1;
                    btnLeft.Enabled = false;
                }
                else
                {
                    btnLeft.BackgroundImage = Properties.Resources.left2;
                    btnLeft.Enabled = true;
                }

                if (imgPos >= imgCount - 2)
                {
                    btnRight.BackgroundImage = Properties.Resources.right1;
                    btnRight.Enabled = false;
                }
                else
                {
                    btnRight.BackgroundImage = Properties.Resources.right2;
                    btnRight.Enabled = true;
                }
                if (imgPos < 1)
                {
                    pictureBox1.Image = Image.FromFile(m_imgList[0]);
                    pictureBox2.Image = Image.FromFile(m_imgList[1]);
                }
                else if (imgPos >= imgCount - 1)
                {
                    pictureBox1.Image = Image.FromFile(m_imgList[imgCount - 1]);
                    pictureBox2.Image = Image.FromFile(m_imgList[imgCount ]);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(m_imgList[imgPos]);
                    pictureBox2.Image = Image.FromFile(m_imgList[imgPos+1]);
                }
            }
            else
            {
                btnLeft.BackgroundImage = Properties.Resources.left1;
                btnLeft.Enabled = false;

                btnRight.BackgroundImage = Properties.Resources.right1;
                btnRight.Enabled = false;
            }
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            imgPos--;
            init();
        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            imgPos++;
            init();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            UcZjtq_SJ_QZ6 m_ucZjtq_sj_qz6 = new UcZjtq_SJ_QZ6();
            

            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            m_ucZjtq_sj_qz6.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(m_ucZjtq_sj_qz6);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void UcZjtq_SJ_QZ5_Load(object sender, EventArgs e)
        {
            imgPos = 0;
            imgCount = m_imgList.Count;

            init();
            //卸载手机上已安装的APK
            Console.WriteLine("开始卸载备份APK");
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("adb uninstall" + " " + "com.huawei.KoBackup");
            process.StandardInput.WriteLine("exit");
            process.StandardInput.AutoFlush = true;
            process.WaitForExit();//等待程序执行完退出进程
            process.Close();
            //安装华为手机自带备份APK
            Console.WriteLine("开始安装备份APK");
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
    }
}
