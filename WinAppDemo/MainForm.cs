using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Controls;
using WinAppDemo.Db.Base;
using WinAppDemo.Db.Model;
using System.Threading;
using System.Collections;
using System.Data.SQLite;



namespace WinAppDemo
{
    public partial class MainForm : Form
    {
        public SQLiteConnection g_conn = null;

        public string g_workPath="";
        public string g_ajName = "";   //案件名称
        public string g_zjName = "";   //证据名称

        public List<string> checkBaseList = new List<string>();   //选中基础信息类列表
        public List<string> checkFileList = new List<string>();   //选中文件类列表
        public List<string> checkAppList = new List<string>();    //选中APP类列表

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "手机数据取证系统SC-A200-努比亚备份";

            TitleButtionInit();
            btnAjgl.BackgroundImage = Properties.Resources.ajgl1;
            DisplayContent(new UcAjgl());

            int i = 0;
            i++;          

        }

        private void DisplayContent(UserControl uc)
        {
            uc.Dock = DockStyle.Fill;
            this.WinContent.Controls.Clear();
            this.WinContent.Controls.Add(uc);
        }

        private void TitleButtionInit()
        {
            btnAjgl.BackgroundImage = Properties.Resources.ajgl;
            btnZjtq.BackgroundImage = Properties.Resources.zjtq;
            btnZjzs.BackgroundImage = Properties.Resources.zjzs;
            btnTools.BackgroundImage = Properties.Resources.gj;
            setDirectory.BackgroundImage = Properties.Resources.set;
        }

        private void BtnAjgl_Click(object sender, EventArgs e)
        {
            TitleButtionInit();
            btnAjgl.BackgroundImage = Properties.Resources.ajgl1;
            DisplayContent(new UcAjgl());
        }

        private void BtnZjtq_Click(object sender, EventArgs e)
        {
            UcAjgl uc = new UcAjgl();
            uc.button13.PerformClick();
           
         
        }

        private void BtnZjzs_Click(object sender, EventArgs e)
        {
            AddNewGjalZs();
        }

        private void BtnTools_Click(object sender, EventArgs e)
        {
            TitleButtionInit();
            btnTools.BackgroundImage = Properties.Resources.gj1;
            DisplayContent(new UcTools());
        }

        public void AddNewGjalZj()
        {
            TitleButtionInit();
            btnZjtq.BackgroundImage = Properties.Resources.zjtq1;
            DisplayContent(new UcZjtq());
        }

        public void AddNewGjalZs()
        {
            TitleButtionInit();
            btnZjzs.BackgroundImage = Properties.Resources.zjzs1;
            DisplayContent(new UcZjzs());
        }

        public void AddNewGjalAj()
        {
            TitleButtionInit();
            btnAjgl.BackgroundImage = Properties.Resources.ajgl1;
            DisplayContent(new UcAjgl());
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            TitleButtionInit();
            setDirectory.BackgroundImage = Properties.Resources.set1;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择工作目录";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                //MessageBox.Show(dialog.SelectedPath);
                AppConfig.getAppConfig().working_directory = dialog.SelectedPath;//保存工作路径
                g_workPath = dialog.SelectedPath;
            }
            setDirectory.BackgroundImage = Properties.Resources.set;
        }

        private void setDirectory_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  g_conn.Close();
        }
    }
}
