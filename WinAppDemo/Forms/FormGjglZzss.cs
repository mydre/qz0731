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
        private void DiGui(TreeNode tn)
        {
            if (tn.Checked == true) Program.m_mainform.checkTreeList.Add(tn.Text);
            foreach (TreeNode tnSub in tn.Nodes)
            {
                DiGui(tnSub);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.m_mainform.g_Num_WXAccount_ss = 0;  //关键字搜索各项条数
            Program.m_mainform.g_Num_WXChatroom_ss = 0;
            //Program.m_mainform.g_Num_WXChatroomList_ss = 0;
            Program.m_mainform.g_Num_WXMessage_friend_ss = 0;
            Program.m_mainform.g_Num_WXMessage_chatroom_ss = 0;
            Program.m_mainform.g_Num_WXMessage_gh_ss = 0;
            Program.m_mainform.g_Num_WXMessage_ss = 0;
            Program.m_mainform.g_Num_WXNewFriend_ss = 0;
            Program.m_mainform.g_Num_WXSns_ss = 0;
            Program.m_mainform.g_Num_WXAddressBook_ss = 0;
            Program.m_mainform.g_Num_WXAddressBook_friend_ss = 0;
            //Program.m_mainform.g_Num_WXAddressBook_chatroom_ss = 0;
            Program.m_mainform.g_Num_WXAddressBook_gh_ss = 0;
            Program.m_mainform.g_Num_WXAddressBook_app_ss = 0;
            Program.m_mainform.g_Num_WXAddressBook_other_ss = 0;

            Program.m_mainform.g_Num_phoneinfo_ss = 0;
            Program.m_mainform.g_Num_Sms_ss = 0;
            Program.m_mainform.g_Num_Calls_ss = 0;
            Program.m_mainform.g_Num_Contacts_ss = 0;

            Program.m_mainform.g_keyword = textBox1.Text;
            string keyword = textBox1.Text; ;
            int iKeyword = -9999;
            try
            { iKeyword = Convert.ToInt32(keyword); }
            catch
            { }

            string dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\PhoneData\\PhoneData.db";   //打开短信、联系人、通话记录等数据库
            conn = new SQLiteConnection(dbPath);
            conn.Open();

            string SQLCommand = string.Empty;
            SQLiteCommand cmdSelect = null;
            SQLiteDataReader reader;

            foreach (TreeNode tn in treeView1.Nodes)
            {
                DiGui(tn);
            }


            for (int i = 0; i < Program.m_mainform.checkTreeList.Count; i++)
            {

                //   for (int i = 0; i < Program.m_mainform.checkAppList.Count; i++)
                //  {
                if ("微信" == Program.m_mainform.checkTreeList[i])
                {
                    //WXAccount 账户
                    SQLCommand = string.Format(" SELECT count(*) FROM WXAccount WHERE wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR email LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR QQid LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%';", keyword);

                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAccount_ss = reader.GetInt32(0); }
                    catch
                    { }

                    //SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE wxID LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    //SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR sex LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR remark LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR description LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%';", keyword);

                    //cmdSelect = null;
                    //cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    //cmdSelect.ExecuteNonQuery();
                    //reader = cmdSelect.ExecuteReader();
                    //reader.Read();
                    //try
                    //{ Program.m_mainform.g_Num_WXAddressBook_ss = reader.GetInt32(0); }
                    //catch
                    //{ }

                    //WXAddressBook 通讯录—好友
                    SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE type in(1,3) and wxID not like '%@chatroom' and wxID not like 'gh_%' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                    SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%');", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_friend_ss = reader.GetInt32(0); }
                    catch
                    { }

                    //SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE type in(1,3) and wxID like '%@chatroom' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    //SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR sex LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR remark LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR description LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%');", keyword);

                    //cmdSelect = null;
                    //cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    //cmdSelect.ExecuteNonQuery();
                    //reader = cmdSelect.ExecuteReader();
                    //reader.Read();
                    //try
                    //{ Program.m_mainform.g_Num_WXAddressBook_chatroom_ss = reader.GetInt32(0); }
                    //catch
                    //{ }

                    //WXAddressBook 通讯录—公众号
                    SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE type in(1,3) and wxID like 'gh_%' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                    SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%');", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_gh_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXAddressBook 通讯录—APP
                    SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE type=33 and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                    SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%');", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_app_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXAddressBook 通讯录—其它（如同群非好友等）
                    SQLCommand = string.Format(" SELECT count(*) FROM WXAddressBook WHERE type not in(1,3,33) and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                    SQLCommand += string.Format(" OR chatFrom LIKE '%{0}%');", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_other_ss = reader.GetInt32(0); }
                    catch
                    { }

                    //WXChatroom 群
                    SQLCommand = string.Format(" SELECT count(*) FROM WXChatroom WHERE groupID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR adminWxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR adminNickname LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR meNickname LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR groupNum ={0}", iKeyword);
                    SQLCommand += string.Format(" OR checkinType ={0};", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXChatroom_ss = reader.GetInt32(0); }
                    catch
                    { }

                    //SQLCommand = string.Format(" SELECT count(*) FROM WXChatroomList WHERE groupID LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR wxID LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR groupNickname LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                    //SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%';", keyword);

                    //cmdSelect = null;
                    //cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    //cmdSelect.ExecuteNonQuery();
                    //reader = cmdSelect.ExecuteReader();
                    //reader.Read();
                    //try
                    //{ Program.m_mainform.g_Num_WXChatroomList_ss = reader.GetInt32(0); }
                    //catch
                    //{ }

                    //WXMessage 聊天
                    SQLCommand = string.Format(" SELECT count(*) FROM WXMessage WHERE wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                    SQLCommand += string.Format(" OR status ={0};", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXMessage 聊天-好友
                    SQLCommand = string.Format(" SELECT count(*) FROM WXMessage WHERE  wxID not like '%@chatroom' and wxID not like 'gh_%' and id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                    SQLCommand += string.Format(" OR status ={0});", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_friend_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXMessage 聊天-群
                    SQLCommand = string.Format(" SELECT count(*) FROM WXMessage WHERE wxID like '%@chatroom' and id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                    SQLCommand += string.Format(" OR status ={0});", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_chatroom_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXMessage 聊天-公众号
                    SQLCommand = string.Format(" SELECT count(*) FROM WXMessage WHERE wxID like 'gh_%' and id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type ={0}", iKeyword);
                    SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                    SQLCommand += string.Format(" OR status ={0});", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_gh_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXSns 朋友圈
                    SQLCommand = string.Format(" SELECT count(*) FROM WXSns WHERE wxID LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR comment LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type ={0};", iKeyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXSns_ss = reader.GetInt32(0); }
                    catch
                    { }
                    //WXNewFriend 新朋友
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

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXNewFriend_ss = reader.GetInt32(0); }
                    catch
                    { }
                }
                // }

                // for (int i = 0; i < Program.m_mainform.checkBaseList.Count; i++)
                // {
                if ("手机基本信息" == Program.m_mainform.checkTreeList[i])
                {
                    SQLCommand = string.Format(" SELECT count(*) FROM phoneinfo WHERE phoneNumber LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR model LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR Android LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR IMEI LIKE '%{0}%';", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_phoneinfo_ss = reader.GetInt32(0); }
                    catch
                    { }
                }
                if ("短信" == Program.m_mainform.checkTreeList[i])
                {

                    SQLCommand = string.Format(" SELECT count(*) FROM Sms WHERE phoneNumber LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR isSend LIKE '%{0}%';", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Sms_ss = reader.GetInt32(0); }
                    catch
                    { }
                }
                if ("通话记录" == Program.m_mainform.checkTreeList[i])
                {
                    SQLCommand = string.Format(" SELECT count(*) FROM Calls WHERE phoneNumber LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR duration LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR type LIKE '%{0}%';", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Calls_ss = reader.GetInt32(0); }
                    catch
                    { }
                }
                if ("联系人" == Program.m_mainform.checkTreeList[i])
                {
                    SQLCommand = string.Format(" SELECT count(*) FROM Contacts WHERE phoneNumber LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR company LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR Email LIKE '%{0}%'", keyword);
                    SQLCommand += string.Format(" OR remark LIKE '%{0}%';", keyword);

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand(SQLCommand, conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Contacts_ss = reader.GetInt32(0); }
                    catch
                    { }
                }
            }
            conn.Close();

            Program.m_mainform.g_newSearch = true;

            //  Program.m_mainform.AddNewGjalZs();
            this.Close();

        }

        private void FormGjglZzss_Load(object sender, EventArgs e)
        {

            //添加根节点
            TreeNode nodeAJ = new TreeNode();
            nodeAJ.Text = Program.m_mainform.g_ajName;
            treeView1.Nodes.Add(nodeAJ);

            TreeNode nodeZJ = new TreeNode();
            nodeZJ.Text = Program.m_mainform.g_zjName;
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
                        TreeNode node1 = new TreeNode(Program.m_mainform.g_str_wxID + "(" + Program.m_mainform.g_str_nickname + ")");
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
                        //node14.Nodes.Add(node142)
                        TreeNode node15 = new TreeNode("新朋友");
                        node1.Nodes.Add(node15);
                    }
                }
            }

            //treeView1.ExpandAll();
            treeView1.Nodes[0].Checked = true;
        }

        private void ufn_CheckChildren(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    n.Checked = node.Checked;
                    if (n.Checked == true)
                        Program.m_mainform.checkTreeList.Add(n.Text);
                    this.ufn_CheckChildren(n);
                }
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Program.m_mainform.checkTreeList.Clear();
            this.ufn_CheckChildren(e.Node);

        }
    }

}
