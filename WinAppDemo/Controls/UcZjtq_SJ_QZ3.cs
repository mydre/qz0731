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

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ3 : UserControl
    {
        public UcZjtq_SJ_QZ3()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_qz2.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_qz2);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_qz3.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_qz3);
        }

        private void UcZjtq_SJ_QZ2_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 30;
        }

        private void Button1_Click_1(object sender, EventArgs e)
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

            FormGjglZzqx form = new FormGjglZzqx(imglist);
            form.ShowDialog();
        }
    }
}
