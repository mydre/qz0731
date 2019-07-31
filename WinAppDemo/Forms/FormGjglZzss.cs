using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Db.Base;
using WinAppDemo.Db.Model;
using System.Data.SQLite;
using WinAppDemo.Controls;

namespace WinAppDemo.Forms
{
    public partial class FormGjglZzss : Form
    {
        public SQLiteConnection conn = null;
        public FormGjglZzss()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SQLiteConnection conn = null;
            ////获取数据文件的路径
            //string dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b\\midwxtrans.db";// + Program.m_mainform.g_workPath+"\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";
            ////创建数据库实例，指定文件位置
            //conn = new SQLiteConnection(dbPath);              
            //conn.Open();


            //conn.Close();
            ////添加根节点
            //TreeNode nodeAJ = new TreeNode();
            //nodeAJ.Text = Program.m_mainform.g_ajName + @"（已删除\未删除\总共）";
            //UcZjzs uc = new UcZjzs();

            //uc.treeView2.Nodes.Add(nodeAJ);


            //TreeNode nodeZJ = new TreeNode();

            //nodeZJ.Text = Program.m_mainform.g_zjName + @"（ \ \ ）";
            //nodeAJ.Nodes.Add(nodeZJ);           //添加证据结点

            //TreeNode nodeBase = new TreeNode("基础信息");
            //nodeZJ.Nodes.Add(nodeBase);

            //int BaseNum = Program.m_mainform.checkBaseList.Count;
            //for (int i = 0; i < BaseNum; i++)
            //{
            //    TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i]);
            //    nodeBase.Nodes.Add(node);
            //}

            //int FileNum = Program.m_mainform.checkFileList.Count;
            //if (FileNum > 0)
            //{
            //    TreeNode nodeFile = new TreeNode("文件信息");
            //    nodeZJ.Nodes.Add(nodeFile);
            //    for (int i = 0; i < FileNum; i++)
            //    {
            //        TreeNode node = new TreeNode(Program.m_mainform.checkFileList[i]);
            //        nodeFile.Nodes.Add(node);
            //    }
            //}

            //int AppNum = Program.m_mainform.checkAppList.Count;
            //if (AppNum > 0)
            //{
            //    TreeNode nodeApp = new TreeNode("APP列表");
            //    nodeZJ.Nodes.Add(nodeApp);
            //    for (int i = 0; i < AppNum; i++)
            //    {
            //        TreeNode node = new TreeNode(Program.m_mainform.checkAppList[i]);
            //        nodeApp.Nodes.Add(node);
            //        if (node.Text == "微信")
            //        {
            //            TreeNode node1 = new TreeNode("账号1");
            //            node.Nodes.Add(node1);
            //            TreeNode node11 = new TreeNode("账号信息");
            //            node1.Nodes.Add(node11);
            //            TreeNode node12 = new TreeNode("通讯录");
            //            node1.Nodes.Add(node12);
            //            TreeNode node121 = new TreeNode("好友");
            //            node12.Nodes.Add(node121);
            //            TreeNode node122 = new TreeNode("公众号");
            //            node12.Nodes.Add(node122);
            //            TreeNode node123 = new TreeNode("群聊");
            //            node12.Nodes.Add(node123);
            //            TreeNode node124 = new TreeNode("应用程序");
            //            node12.Nodes.Add(node124);
            //            TreeNode node125 = new TreeNode("其他");
            //            node12.Nodes.Add(node125);


            //            TreeNode node13 = new TreeNode("聊天记录");
            //            node1.Nodes.Add(node13);
            //            TreeNode node131 = new TreeNode("好友聊天");
            //            node13.Nodes.Add(node131);
            //            TreeNode node132 = new TreeNode("群聊");
            //            node13.Nodes.Add(node132);
            //            TreeNode node133 = new TreeNode("公众号");
            //            node13.Nodes.Add(node133);

            //            TreeNode node14 = new TreeNode("朋友圈");
            //            node1.Nodes.Add(node14);
            ////            TreeNode node141 = new TreeNode("本人的朋友圈");
            ////            node14.Nodes.Add(node141);
            ////            TreeNode node142 = new TreeNode("好友的朋友圈");
            ////            node14.Nodes.Add(node142);
            //        }
            //    }
            //}

            //  treeView2.ExpandAll();

            long Num_WXAccount = 0;
            long Num_WXChatroom =0;
            long Num_WXChatroomList = 0;
            long Num_WXMessage = 0;
            long Num_WXNewFriend = 0;
            long Num_WXSns = 0;
            long Num_WXAddressBook = 0;


            Program.m_mainform.g_Num_WXAccount = 0;
            Program.m_mainform.g_Num_WXChatroom = 0;
            Program.m_mainform.g_Num_WXChatroomList = 0;
            Program.m_mainform.g_Num_WXMessage = 0;
            Program.m_mainform.g_Num_WXNewFriend = 0;
            Program.m_mainform.g_Num_WXSns = 0;
            Program.m_mainform.g_Num_WXAddressBook = 0;

            Program.m_mainform.g_Num_phoneinfo = 0;
            Program.m_mainform.g_Num_Sms = 0;
            Program.m_mainform.g_Num_Calls = 0;
            Program.m_mainform.g_Num_Contacts = 0;


            string keyword=textBox1.Text;
            int iKeyword = -9999;
            try
            {
                iKeyword = Convert.ToInt32(keyword);
            }
            catch
            { }


            string SQLCommand = string.Empty;
            SQLCommand = string.Format(" SELECT count(*) FROM WXAccount WHERE wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR email LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR QQid LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%';", keyword);


            SQLiteCommand cmdSelect00 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect00.ExecuteNonQuery();
            SQLiteDataReader reader00 = cmdSelect00.ExecuteReader();
            reader00.Read();
            try
            { Program.m_mainform.g_Num_WXAccount = reader00.GetInt32(0); }
            catch
            { }
            reader00.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR type ={0}", iKeyword);
            SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR sex LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR remark LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR description LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%';", keyword);

            SQLiteCommand cmdSelect01 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect01.ExecuteNonQuery();
            SQLiteDataReader reader01 = cmdSelect01.ExecuteReader();
            reader01.Read();
            try
            { Program.m_mainform.g_Num_WXAddressBook = reader01.GetInt32(0); }
            catch
            { }
            reader01.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXChatroom WHERE groupID LIKE '%{0}%'", keyword);            
            SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR adminWxID LIKE '%{0}%'", keyword);            
            SQLCommand += string.Format(" OR adminNickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR meNickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR groupNum ={0}", iKeyword);
            SQLCommand += string.Format(" OR checkinType ={0};", iKeyword);

            SQLiteCommand cmdSelect02 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect02.ExecuteNonQuery();
            SQLiteDataReader reader02 = cmdSelect02.ExecuteReader();
            reader02.Read();
            try
            {
                Program.m_mainform.g_Num_WXChatroom = reader02.GetInt32(0);
            }
            catch
            { }            
            reader02.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXChatroomList WHERE groupID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR groupNickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%';", keyword);

            SQLiteCommand cmdSelect03 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect03.ExecuteNonQuery();
            SQLiteDataReader reader03 = cmdSelect03.ExecuteReader();
            reader03.Read();
            try
            { Program.m_mainform.g_Num_WXChatroomList = reader03.GetInt32(0);       }
            catch
            {
            }
            reader03.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXMessage WHERE wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR type ={0}", iKeyword);
            SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
            SQLCommand += string.Format(" OR status ={0};", iKeyword);

            SQLiteCommand cmdSelect04 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect04.ExecuteNonQuery();
            SQLiteDataReader reader04 = cmdSelect04.ExecuteReader();
            reader04.Read();
            try
            {
                Program.m_mainform.g_Num_WXMessage = reader04.GetInt32(0);
            }
            catch
            {
            }
            reader04.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXSns WHERE wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR comment LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR type ={0};", iKeyword);            

            SQLiteCommand cmdSelect05 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect05.ExecuteNonQuery();
            SQLiteDataReader reader05 = cmdSelect05.ExecuteReader();
            reader05.Read();
            try
            {
                Program.m_mainform.g_Num_WXSns = reader05.GetInt32(0);
            }
            catch
            {
            }
            reader05.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM WXNewFriend WHERE wxID LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR sex LIKE '%{0}%'", keyword);            
            SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR remark LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR description LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR contentVerify LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR lastModifiedTime LIKE '%{0}%';", keyword); 

            SQLiteCommand cmdSelect06 = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
            cmdSelect06.ExecuteNonQuery();
            SQLiteDataReader reader06 = cmdSelect06.ExecuteReader();
            reader06.Read();
            try
            { Program.m_mainform.g_Num_WXNewFriend = reader06.GetInt32(0); }
            catch
            { }
            reader06.Close();

            string dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\PhoneData\\PhoneData.db";   //打开短信、联系人、通话记录等数据库
            conn = new SQLiteConnection(dbPath);
            conn.Open();

            SQLCommand = string.Format(" SELECT count(*) FROM phoneinfo WHERE phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR model LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR type LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR Android LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR IMEI LIKE '%{0}%';", keyword);

            SQLiteCommand cmdSelect10 = new SQLiteCommand(SQLCommand, conn);
            cmdSelect10.ExecuteNonQuery();
            SQLiteDataReader reader10 = cmdSelect10.ExecuteReader();
            reader10.Read();
            try
            {
                Program.m_mainform.g_Num_phoneinfo = reader10.GetInt32(0);
            }
            catch
            {
            }
            reader10.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM Sms WHERE phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR isSend LIKE '%{0}%';", keyword);
            
            SQLiteCommand cmdSelect11 = new SQLiteCommand(SQLCommand, conn);
            cmdSelect11.ExecuteNonQuery();
            SQLiteDataReader reader11 = cmdSelect11.ExecuteReader();
            reader11.Read();
            try
            {
                Program.m_mainform.g_Num_Sms = reader11.GetInt32(0);
            }
            catch
            {
            }
            reader11.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM Calls WHERE phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR duration LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR type LIKE '%{0}%';", keyword);

            SQLiteCommand cmdSelect12 = new SQLiteCommand(SQLCommand, conn);
            cmdSelect12.ExecuteNonQuery();
            SQLiteDataReader reader12 = cmdSelect12.ExecuteReader();
            reader12.Read();
            try
            {
                Program.m_mainform.g_Num_Calls = reader12.GetInt32(0);
            }
            catch
            {
            }
            reader12.Close();

            SQLCommand = string.Format(" SELECT count(*) FROM Contacts WHERE phoneNumber LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR company LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR Email LIKE '%{0}%'", keyword);
            SQLCommand += string.Format(" OR remark LIKE '%{0}%';", keyword);

            SQLiteCommand cmdSelect13 = new SQLiteCommand(SQLCommand, conn);
            cmdSelect13.ExecuteNonQuery();
            SQLiteDataReader reader13 = cmdSelect13.ExecuteReader();
            reader13.Read();
            try
            {
                Program.m_mainform.g_Num_Contacts = reader13.GetInt32(0);
            }
            catch
            {
            }
            reader13.Close();

            conn.Close();

            Program.m_mainform.AddNewGjalZs();
            this.Close();

        }

        private void FormGjglZzss_Load(object sender, EventArgs e)
        {

            //添加根节点
            TreeNode nodeAJ = new TreeNode();
            nodeAJ.Text = Program.m_mainform.g_ajName ;
            treeView1.Nodes.Add(nodeAJ);

            TreeNode nodeZJ = new TreeNode();
            nodeZJ.Text = Program.m_mainform.g_zjName ;
            nodeAJ.Nodes.Add(nodeZJ);           //添加证据结点

            TreeNode nodeBase = new TreeNode("基础信息");
            nodeZJ.Nodes.Add(nodeBase);

            int BaseNum = Program.m_mainform.checkBaseList.Count;
            for (int i = 0; i < BaseNum; i++)
            {
                TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i]);
                nodeBase.Nodes.Add(node);
            }

            int FileNum = Program.m_mainform.checkFileList.Count;
            if (FileNum > 0)
            {
                TreeNode nodeFile = new TreeNode("文件信息");
                nodeZJ.Nodes.Add(nodeFile);
                for (int i = 0; i < FileNum; i++)
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkFileList[i]);
                    nodeFile.Nodes.Add(node);
                }
            }

            int AppNum = Program.m_mainform.checkAppList.Count;
            if (AppNum > 0)
            {
                TreeNode nodeApp = new TreeNode("APP列表");
                nodeZJ.Nodes.Add(nodeApp);
                for (int i = 0; i < AppNum; i++)
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkAppList[i]);
                    nodeApp.Nodes.Add(node);
                    if (node.Text == "微信")
                    {
                        TreeNode node1 = new TreeNode("账号1");
                        node.Nodes.Add(node1);
                        TreeNode node11 = new TreeNode("账号信息");
                        node1.Nodes.Add(node11);
                        TreeNode node12 = new TreeNode("通讯录");
                        node1.Nodes.Add(node12);
                        TreeNode node121 = new TreeNode("好友");
                        node12.Nodes.Add(node121);
                        TreeNode node122 = new TreeNode("公众号");
                        node12.Nodes.Add(node122);
                        TreeNode node123 = new TreeNode("群");
                        node12.Nodes.Add(node123);
                        TreeNode node124 = new TreeNode("应用程序");
                        node12.Nodes.Add(node124);
                        TreeNode node125 = new TreeNode("其他");
                        node12.Nodes.Add(node125);


                        TreeNode node13 = new TreeNode("聊天记录");
                        node1.Nodes.Add(node13);
                        TreeNode node131 = new TreeNode("好友聊天");
                        node13.Nodes.Add(node131);
                        TreeNode node132 = new TreeNode("群聊");
                        node13.Nodes.Add(node132);
                        TreeNode node133 = new TreeNode("公众号讯息");
                        node13.Nodes.Add(node133);

                        TreeNode node14 = new TreeNode("朋友圈");
                        node1.Nodes.Add(node14);

                            //TreeNode node141 = new TreeNode("本人的朋友圈");
                            //node14.Nodes.Add(node141);
                            //TreeNode node142 = new TreeNode("好友的朋友圈");
                            //node14.Nodes.Add(node142);

                        //TreeNode node141 = new TreeNode("本人的朋友圈");
                        //node14.Nodes.Add(node141);
                        //TreeNode node142 = new TreeNode("好友的朋友圈");
                        //node14.Nodes.Add(node142);
                    }
                }
            }

            //treeView1.ExpandAll();
        }

        private void ufn_CheckChildren(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    n.Checked = node.Checked;
                    this.ufn_CheckChildren(n);
                }
            }
        }
        
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            this.ufn_CheckChildren(e.Node);
        }
    }

}
