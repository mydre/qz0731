using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using WinAppDemo.Forms;

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_QZ4 : UserControl
    {
        private IList<string> m_imgList = null;
        private int imgPos = 0;
        private int imgCount = 0;

        public UcZjtq_SJ_QZ4(IList<string> imglist)
        {
            InitializeComponent();

            if (imglist == null)
            {
                imglist = new List<string>();
            }

            m_imgList = imglist;
        }

        private void UcZjtq_SJ_QZ4_Load(object sender, EventArgs e)
        {
            imgPos = 0;
            imgCount = m_imgList.Count;

            init();
        }

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

                if (imgPos >= imgCount - 1)
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
                }
                else if (imgPos >= imgCount - 1)
                {
                    pictureBox1.Image = Image.FromFile(m_imgList[imgCount - 1]);
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(m_imgList[imgPos]);
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
            m_ucZjtq_sj_qz6.startGetData();   //7.8

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FormGjglBf form = new FormGjglBf();
            form.ShowDialog();
            
        }
    }
}
