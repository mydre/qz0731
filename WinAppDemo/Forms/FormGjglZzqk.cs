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

namespace WinAppDemo.Forms
{
    public partial class FormGjglZzqk : Form
    {

        public FormGjglZzqk()
        {
            InitializeComponent();
        }

        private void FormGjglZzqx_Load(object sender, EventArgs e)
        {
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            FormGjglBf form = new FormGjglBf();
            form.ShowDialog();

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

            //UcZjtq_SJ_QZ4 m_ucZjtq_sj_qz4 = new UcZjtq_SJ_QZ4(imglist);


            //AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            //m_ucZjtq_sj_qz4.Dock = DockStyle.Fill;
            //AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            //AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(m_ucZjtq_sj_qz4);

            this.Close();
        }

    }
}
