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

        public string g_str_wxID = "";             //本人微信账号昵称
        public string g_str_nickname = "";        //本人微信昵称

        public long g_Num_WXAccount = 0;    //取证数据中各项条数
        public long g_Num_WXChatroom = 0;        
        public long g_Num_WXChatroomList = 0;
        public long g_Num_WXMessage = 0;
        public long g_Num_WXMessage_friend = 0;
        public long g_Num_WXMessage_chatroom = 0;
        public long g_Num_WXMessage_gh = 0;
        public long g_Num_WXNewFriend = 0;
        public long g_Num_WXSns = 0;
        public long g_Num_WXAddressBook = 0;
        public long g_Num_WXAddressBook_friend = 0;
        public long g_Num_WXAddressBook_gh = 0;
        public long g_Num_WXAddressBook_chatroom = 0;
        public long g_Num_WXAddressBook_app = 0;
        public long g_Num_WXAddressBook_other = 0;

        public long g_Num_phoneinfo = 0;
        public long g_Num_Sms = 0;
        public long g_Num_Calls = 0;
        public long g_Num_Contacts = 0;

        public string g_keyword = "";         //搜索的关键字
        public bool g_newSearch = false;      //新的搜索
        public long g_Num_WXAccount_ss = 0;  //关键字搜索各项条数
        public long g_Num_WXChatroom_ss = 0;        
        public long g_Num_WXChatroomList_ss = 0;
        public long g_Num_WXMessage_ss = 0;
        public long g_Num_WXMessage_friend_ss = 0;
        public long g_Num_WXMessage_chatroom_ss = 0;
        public long g_Num_WXMessage_gh_ss = 0;
        public long g_Num_WXNewFriend_ss = 0;
        public long g_Num_WXSns_ss = 0;
        public long g_Num_WXAddressBook_ss = 0;
        public long g_Num_WXAddressBook_friend_ss = 0;
        public long g_Num_WXAddressBook_gh_ss = 0;
        public long g_Num_WXAddressBook_chatroom_ss = 0;
        public long g_Num_WXAddressBook_app_ss = 0;
        public long g_Num_WXAddressBook_other_ss = 0;

        public long g_Num_phoneinfo_ss = 0;
        public long g_Num_Sms_ss = 0;
        public long g_Num_Calls_ss = 0;
        public long g_Num_Contacts_ss = 0;

        public List<string> checkBaseList = new List<string>();   //选中基础信息类列表
        public List<string> checkFileList = new List<string>();   //选中文件类列表
        public List<string> checkAppList = new List<string>();    //选中APP类列表
        public List<string> checkTreeList = new List<string>();    //搜索中选中的treeview节点列表
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
