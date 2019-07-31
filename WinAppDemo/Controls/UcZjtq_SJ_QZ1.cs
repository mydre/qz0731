using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ1 : UserControl
    {
       

        public UcZjtq_SJ_QZ1()
        {
            InitializeComponent();
            Program.m_mainform.checkBaseList.Clear();
            Program.m_mainform.checkFileList.Clear();
            Program.m_mainform.checkAppList.Clear();
            Program.m_mainform.checkBaseList.Add("手机基本信息");
        }
        
        private void Button1_Click(object sender, EventArgs e)   //开始提取
        {            
            foreach (Control c in this.groupBox1.Controls)
            {
                if(c is CheckBox &&((CheckBox)c).Checked==true)
                {
                    if(c.Text != "手机基本信息")
                        Program.m_mainform.checkBaseList.Add(c.Text);
                }
            }

            foreach (Control c in this.groupBox2.Controls)
            {
                if (c is CheckBox && ((CheckBox)c).Checked == true)
                {
                    Program.m_mainform.checkFileList.Add(c.Text);
                }
            }

            foreach (Control c in this.groupBox3.Controls)
            {
                if (c is CheckBox && ((CheckBox)c).Checked == true)
                {
                    Program.m_mainform.checkAppList.Add(c.Text);
                }
            }


            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_qz2.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_qz2);
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_ljcg.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_ljcg);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
