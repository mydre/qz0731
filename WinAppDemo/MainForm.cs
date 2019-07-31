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
using System.IO;
using PortableDeviceApiLib;
using PortableDeviceTypesLib;



namespace WinAppDemo
{
    public partial class MainForm : Form
    {
        private PortableDeviceManagerClass devMgr;
        private PortableDeviceApiLib.PortableDeviceClass ppDevice;

        public SQLiteConnection g_conn = null;
        
        public string g_workPath="";
        public string g_ajName = "";   //案件名称
        public string g_zjName = "";   //证据名称
        public string backupFileName = "";    //备份文件名
        public string DeviceBrand = "";       //设备厂商
        public string DeviceModel = "";     //设备型号
        public string Devicesystem = "";    //设备操作系统
        public string DeviceState = "";       //设备是否ROOT

        public long g_Num_WXAccount = 0;
        public long g_Num_WXChatroom = 0;
        public long g_Num_WXChatroomList = 0;
        public long g_Num_WXMessage = 0;
        public long g_Num_WXNewFriend = 0;
        public long g_Num_WXSns = 0;
        public long g_Num_WXAddressBook = 0;

        public long g_Num_phoneinfo = 0;
        public long g_Num_Sms = 0;
        public long g_Num_Calls = 0;
        public long g_Num_Contacts = 0;


        public List<string> checkBaseList = new List<string>();   //选中基础信息类列表
        public List<string> checkFileList = new List<string>();   //选中文件类列表
        public List<string> checkAppList = new List<string>();    //选中APP类列表
        public Case Case { get; set; } = new Case();                     //获取CaseInfo信息

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
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Setwork_directory();
        }
        #region
        /// <summary>
        /// 创建工作路径
        /// </summary>
        private void Setwork_directory()
        {
            int CaseNum = 0;
            string dbPath = "Data Source =" + Application.StartupPath + "\\Db\\CaseMsg.db";
            g_conn = new SQLiteConnection(dbPath);
            g_conn.Open();


            SQLiteCommand comm = g_conn.CreateCommand();
            comm.CommandText = "select  count (*) from CaseInfor";
            SQLiteDataReader sr = comm.ExecuteReader();
            while (sr.Read())
            {
                CaseNum = int.Parse(sr[0].ToString());
                //Console.WriteLine(iport);
                Console.WriteLine(sr[0]);
                Console.WriteLine("一共有多少条数据");
            }
            // sr.Close();
            if (CaseNum <= 0)
            {
                WorkDirectoryForm directoryForm = new WorkDirectoryForm();
                directoryForm.Show();
                Program.m_mainform.g_workPath = directoryForm.WorDIrectoryTexbox.ToString();
            }
            else
            {
                //g_conn.Open();
                Console.WriteLine("开始设置工作路径");
                SQLiteCommand com = g_conn.CreateCommand();
                com.CommandText = "select  Path, CaseName, Max(CaseId) from CaseInfor";
                SQLiteDataReader sr1 = com.ExecuteReader();
                while (sr1.Read())
                {
                    Program.m_mainform.g_workPath = sr1[0].ToString();
                    Console.WriteLine(sr1[0].ToString());
                    Program.m_mainform.g_workPath = Program.m_mainform.g_workPath.Substring(0, g_workPath.LastIndexOf(@"\"));
                    Console.WriteLine(Program.m_mainform.g_workPath);
                    Console.Read();
                }
            }
            if(!Directory.Exists(g_workPath))
            {
                MessageBoxButtons messBtn = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("\t'"+ Program.m_mainform.g_workPath + "'工作路径不存在，是否创建？\n\t或点击取消设置新的工作路径", "提示:确认", messBtn);
                if (dr == DialogResult.OK)
                {
                    Directory.CreateDirectory(Program.m_mainform.g_workPath);    //创建工作路径
                    return;
                }
                else
                {
                    return;
                }
              
            }

        }
        #endregion

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

        /// <summary>
        /// 弹窗设置工作路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSet_Click(object sender, EventArgs e)
        {
            WorkDirectoryForm directoryForm = new WorkDirectoryForm();
            directoryForm.Show();
            if (g_workPath!="")
            {
                directoryForm.WorDIrectoryTexbox.Text = g_workPath;
            }

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //  g_conn.Close();
            

        }


    }
    }
