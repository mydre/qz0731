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
using WinAppDemo.Db.Base;
using WinAppDemo.Db.Model;
using System.Data.SQLite;
using System.Threading;
using WinAppDemo.tools;

namespace WinAppDemo.Controls
{
    public partial class UcZjzs : UserControl
    {
        public SQLiteConnection conn = null; 
        public string str_avatarPath = "";        //本人微信昵称
        public bool is_avatarPath = false;  //本人微信头像文件是否存在

        //    private List<TreeNodeTypes> Types;
        public UcZjzs()
        {
            InitializeComponent();
        }

        private void UcZjzs_Load(object sender, EventArgs e)
        {
            Program.m_mainform.g_str_wxID = "";             //本人微信账号昵称
            Program.m_mainform.g_str_nickname = "";        //本人微信昵称

            //获取数据文件的路径
            // string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";
            string dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\de28621220a0ab0fc066062fbf6bcf41\\midwxtrans.db";// + Program.m_mainform.g_workPath+"\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";

            //创建数据库实例，指定文件位置
            Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
            Program.m_mainform.g_conn.Open();
           
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            dataGridView3.Hide();

            dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\PhoneData\\PhoneData.db";   //打开短信、联系人、通话记录等数据库
            conn = new SQLiteConnection(dbPath);
            conn.Open();

            SQLiteCommand cmdSelect = new SQLiteCommand("select count(*) FROM phoneinfo", conn);
            cmdSelect.ExecuteNonQuery();
            SQLiteDataReader reader = cmdSelect.ExecuteReader();
            reader.Read();
            try
            {
                Program.m_mainform.g_Num_phoneinfo = reader.GetInt32(0);
            }
            catch
            {
            }            

            int BaseNum = Program.m_mainform.checkBaseList.Count;
            int FileNum = Program.m_mainform.checkFileList.Count;
            int AppNum = Program.m_mainform.checkAppList.Count;

            for (int i = 0; i < BaseNum;i++)
            {
                if("短信" == Program.m_mainform.checkBaseList[i])
                {
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM Sms", conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Sms = reader.GetInt32(0); }
                    catch
                    { }
                }
                if ("通话记录" == Program.m_mainform.checkBaseList[i])
                {
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM Calls", conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Calls = reader.GetInt32(0); }
                    catch
                    { }
                }
                if ("联系人" == Program.m_mainform.checkBaseList[i])
                {
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM Contacts", conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_Contacts = reader.GetInt32(0); }
                    catch
                    { }
                }
            }

            for (int i = 0; i < AppNum; i++)
            {
                if ("微信" == Program.m_mainform.checkAppList[i])
                {
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAccount", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAccount = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook where type in(1,3) and wxID not like '%@chatroom' and wxID not like 'gh_%'", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_friend = reader.GetInt32(0); }
                    catch
                    { }
                    //cmdSelect = null;
                    //cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook where type in(1,3) and wxID like '%@chatroom'", Program.m_mainform.g_conn);
                    //cmdSelect.ExecuteNonQuery();
                    //reader = cmdSelect.ExecuteReader();
                    //reader.Read();
                    //try
                    //{ Program.m_mainform.g_Num_WXAddressBook_chatroom = reader.GetInt32(0); }
                    //catch
                    //{ }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook where type in(1,3) and wxID like 'gh_%'", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_gh = reader.GetInt32(0); }
                    catch
                    { }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook where type =33", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_app = reader.GetInt32(0); }
                    catch
                    { }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXAddressBook where type not in(1,3,33) ", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXAddressBook_other = reader.GetInt32(0); }
                    catch
                    { }


                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXChatroom", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXChatroom = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXChatroomList", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXChatroomList = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXMessage", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage = reader.GetInt32(0); }
                    catch
                    { }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXMessage where wxID not like '%@chatroom' and wxID not like 'gh_%';", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_friend = reader.GetInt32(0); }
                    catch
                    { }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXMessage where wxID like '%@chatroom';", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_chatroom = reader.GetInt32(0); }
                    catch
                    { }
                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXMessage where wxID like 'gh_%';", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXMessage_gh = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXSns", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXSns = reader.GetInt32(0); }
                    catch
                    { }

                    cmdSelect = null;
                    cmdSelect = new SQLiteCommand("select count(*) FROM WXNewFriend", Program.m_mainform.g_conn);
                    cmdSelect.ExecuteNonQuery();
                    reader = cmdSelect.ExecuteReader();
                    reader.Read();
                    try
                    { Program.m_mainform.g_Num_WXNewFriend = reader.GetInt32(0); }
                    catch
                    { }

                    reader.Close();
                }
            }

            long totalNum = Program.m_mainform.g_Num_WXAccount;        //数据总条数      
            totalNum += Program.m_mainform.g_Num_WXChatroom;
            //totalNum += Program.m_mainform.g_Num_WXChatroomList;
            totalNum += Program.m_mainform.g_Num_WXMessage;
            totalNum += Program.m_mainform.g_Num_WXNewFriend;
            totalNum += Program.m_mainform.g_Num_WXSns;
            totalNum += Program.m_mainform.g_Num_WXAddressBook_friend;
            totalNum += Program.m_mainform.g_Num_WXAddressBook_gh;
            totalNum += Program.m_mainform.g_Num_WXAddressBook_app;
            totalNum += Program.m_mainform.g_Num_WXAddressBook_other;

            totalNum += Program.m_mainform.g_Num_phoneinfo;
            totalNum += Program.m_mainform.g_Num_Sms;
            totalNum += Program.m_mainform.g_Num_Calls;
            totalNum += Program.m_mainform.g_Num_Contacts;

            //添加根节点
            TreeNode nodeAJ = new TreeNode();
            nodeAJ.Text = Program.m_mainform.g_ajName + @"（总共" + Convert.ToString(totalNum) + "）";
            treeView1.Nodes.Add(nodeAJ);

            TreeNode nodeZJ = new TreeNode();
            nodeZJ.Text = Program.m_mainform.g_zjName + @"（总共" + Convert.ToString(totalNum) + "）";
            nodeAJ.Nodes.Add(nodeZJ);           //添加证据结点

            TreeNode nodeBase = new TreeNode("基础信息" + @"（" + Convert.ToString(Program.m_mainform.g_Num_phoneinfo + Program.m_mainform.g_Num_Sms + Program.m_mainform.g_Num_Calls + Program.m_mainform.g_Num_Contacts) + "）");
            nodeZJ.Nodes.Add(nodeBase);


            for (int i = 0; i < BaseNum; i++)
            {
                if ("手机基本信息" == Program.m_mainform.checkBaseList[i])
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_phoneinfo + "）");
                    nodeBase.Nodes.Add(node);
                }
                if ("短信" == Program.m_mainform.checkBaseList[i])
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Sms+ "）");
                    nodeBase.Nodes.Add(node);
                }
                if ("通话记录" == Program.m_mainform.checkBaseList[i])
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Calls + "）");
                    nodeBase.Nodes.Add(node);
                }
                if ("联系人" == Program.m_mainform.checkBaseList[i])
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Contacts + "）");
                    nodeBase.Nodes.Add(node);
                }
            }

            
            if (FileNum > 0)
            {
                TreeNode nodeFile = new TreeNode("文件信息(0)");
                nodeZJ.Nodes.Add(nodeFile);
                for (int i = 0; i < FileNum; i++)
                {
                    TreeNode node = new TreeNode(Program.m_mainform.checkFileList[i]);
                    nodeFile.Nodes.Add(node);
                }
            }

            //获取本人微信ID、昵称、头像
            string sql = "select wxID,nickname,avatarPath from WXAccount;";
            SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
            DataTable dt1 = new DataTable();
            mAdapter1.Fill(dt1);

            if (dt1.Rows.Count == 1)
            {
                Program.m_mainform.g_str_wxID = Convert.ToString(dt1.Rows[0]["wxID"]);
                Program.m_mainform.g_str_nickname = Convert.ToString(dt1.Rows[0]["nickname"]);

                sql = "select avatarPath from WXAddressBook where wxID='"+Program.m_mainform.g_str_wxID+"';";

                SQLiteDataAdapter mAdapter2 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt2 = new DataTable();
                mAdapter2.Fill(dt2);

                if (dt2.Rows.Count == 1)
                {
                    str_avatarPath = Convert.ToString(dt2.Rows[0]["avatarPath"]);
                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + str_avatarPath)) //判断文件是否存在
                        is_avatarPath = true;
                    else
                        is_avatarPath = false;
                }
            }
                        
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
                        TreeNode node1 = new TreeNode(Program.m_mainform.g_str_wxID + "("+ Program.m_mainform.g_str_nickname + ")（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount + Program.m_mainform.g_Num_WXNewFriend + Program.m_mainform.g_Num_WXChatroom + Program.m_mainform.g_Num_WXMessage + Program.m_mainform.g_Num_WXAddressBook_friend + Program.m_mainform.g_Num_WXAddressBook_other + Program.m_mainform.g_Num_WXAddressBook_gh + Program.m_mainform.g_Num_WXAddressBook_app + Program.m_mainform.g_Num_WXSns) + "）");
                        node.Nodes.Add(node1);
                        TreeNode node11 = new TreeNode("账号信息" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount) + "）");
                        node1.Nodes.Add(node11);
                        TreeNode node12 = new TreeNode("通讯录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_friend+ Program.m_mainform.g_Num_WXAddressBook_gh + Program.m_mainform.g_Num_WXChatroom + Program.m_mainform.g_Num_WXAddressBook_app + Program.m_mainform.g_Num_WXAddressBook_other) + "）");
                        node1.Nodes.Add(node12);
                        TreeNode node121 = new TreeNode("好友" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_friend) + "）");
                        node12.Nodes.Add(node121);
                        TreeNode node122 = new TreeNode("公众号" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_gh) + "）");
                        node12.Nodes.Add(node122);
                        TreeNode node123 = new TreeNode("群" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXChatroom) + "）");
                        node12.Nodes.Add(node123);
                        TreeNode node124 = new TreeNode("应用程序" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_app) + "）");
                        node12.Nodes.Add(node124);
                        TreeNode node125 = new TreeNode("其他" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_other) + "）");
                        node12.Nodes.Add(node125);


                        TreeNode node13 = new TreeNode("聊天记录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage) + "）");
                        node1.Nodes.Add(node13);
                        TreeNode node131 = new TreeNode("好友聊天" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_friend) + "）");
                        node13.Nodes.Add(node131);
                        TreeNode node132 = new TreeNode("群聊" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_chatroom) + "）");
                        node13.Nodes.Add(node132);
                        TreeNode node133 = new TreeNode("公众号讯息" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_gh) + "）");
                        node13.Nodes.Add(node133);

                        TreeNode node14 = new TreeNode("朋友圈" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXSns) + "）");
                        node1.Nodes.Add(node14);
                        //TreeNode node141 = new TreeNode("本人的朋友圈");
                        //node14.Nodes.Add(node141);
                        //TreeNode node142 = new TreeNode("好友的朋友圈");
                        //node14.Nodes.Add(node142);

                        TreeNode node15 = new TreeNode("新朋友" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXNewFriend) + "）");
                        node1.Nodes.Add(node15);
                    }
                }
            }
            treeView1.ExpandAll();
        }

  //      private void Bind(TreeNode parNode, List<TreeNodeTypes> list, int nodeId)
   //     {
            //var childList = list.FindAll(t => t.ParentId == nodeId).OrderBy(t => t.Id);

            //foreach (var urlTypese in childList)
            //{
            //    var node = new TreeNode();
            //    node.Name = urlTypese.Id.ToString();
            //    node.Text = urlTypese.Name;
            //    node.Tag = urlTypese.Value;
            //    parNode.Nodes.Add(node);
            //    Bind(node, list, urlTypese.Id);
            //}
    //    }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeName = e.Node.Text as string;
            //string id = e.Node.Name;
            int pos = nodeName.IndexOf("（");
            if (pos > 0)
                nodeName = nodeName.Substring(0, pos);
            switch (nodeName)
            {
                case "手机基本信息":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select phoneNumber as 手机号,model as 品牌,type as 型号,Android as 安卓版本,IMEI from phoneinfo;", conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "联系人":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select name as 姓名,phoneNumber as 手机号, district as 地区,company as 单位,Email as 邮箱,remark as 备注 from Contacts;", conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "短信":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select phoneNumber as 手机号,content as 内容,datetime as 时间,isSend as 是否接收 from Sms;", conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "通话记录":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select name as 联系人,phoneNumber as 手机号,datetime as 时间,duration as 通话时长,type as 接收状态 from Calls;", conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "账号信息":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,email as 邮箱,QQid as 关联QQ号 from WXAccount;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "通讯录":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "好友":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where type in(1,3) and wxID not like '%@chatroom' and wxID not like 'gh_%';", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "群":
                    {
                        Name = "群";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select groupID as 群账号,name as 群名,adminWxID as 群主微信号,adminNickname as 群主昵称,groupNum as 成员数,meNickname as 本人群昵称 from WXChatroom;", Program.m_mainform.g_conn);//,checkinType as 入群方式
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        //dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "公众号":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where type in(1,3) and wxID like 'gh_%';", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "应用程序":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where type =33;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "其他":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where  type not in(1,3,33);", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "好友聊天":
                    {
                        Name = "好友聊天";

                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                       // dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Hide();
                        dataGridView2.Show();
                        dataGridView2.DataSource = null;
                        richTextBoxEx1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称, WXMessage.wxID as 微信账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID not like '%@chatroom' and WXAddressBook.wxID not like 'gh_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView2.DataSource = dt1;
                        dataGridView2.Columns[1].Visible = false;
                        break;
                    }
                case "群聊":
                    {
                        Name = "群聊";

                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        //dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Hide();
                        dataGridView2.Show();
                        dataGridView2.DataSource = null;
                        richTextBoxEx1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 群聊账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like '%@chatroom' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView2.DataSource = dt1;
                        dataGridView2.Columns[1].Visible = false;

                        break;
                    }
                case "公众号讯息":
                    {
                        Name = "公众号讯息";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        //dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Hide();
                        
                        dataGridView2.Show();
                        dataGridView2.DataSource = null;
                        richTextBoxEx1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 公众号账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'gh_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);

                        dataGridView2.DataSource = dt1;
                        dataGridView2.Columns[1].Visible = false;

                        break;
                    }
                case "朋友圈":
                    {
                        Name = "朋友圈";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        dataGridView4.Hide();
                        dataGridView2.Show();                        
                        
                        dataGridView2.DataSource = null;
                        richTextBoxEx1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXSns.wxID as 微信账号, count(WXSns.wxID) as 朋友圈条数 from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID group by WXAddressBook.wxID;", Program.m_mainform.g_conn);   //and WXAddressBook.type=3
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView2.DataSource = dt1;
                        dataGridView2.Columns[1].Visible = false;

                        break;
                    }
                case "新朋友":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,nickname as 昵称,sex as 性别,sign as 个性签名, district as 地区,remark as 备注,description as 描述,contentVerify as 好友请求内容,lastModifiedTime as 最后更新时间  from WXNewFriend;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                default:
                    {
                        panel2.Hide();
                        panel3.Hide();
                        panel1.Show();
                        panel1.Dock = DockStyle.Fill;

                        break;
                    }
            }

            //string wxid = e.Node.Tag as string;
            //string id = e.Node.Name;

            //switch (wxid)
            //{
            //    case "通讯录":
            //        {
            //            panel1.Hide();
            //            panel3.Hide();
            //            panel2.Show();
            //            panel2.Dock = DockStyle.Fill;

            //            this.dataGridView1.DataSource = null;
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView1.DataSource = context.WxFriends
            //                    .Where(friend => friend.Type == 3)
            //                    .ToList();
            //            }

            //            break;
            //        }
            //    case "公众号":
            //        {
            //            panel1.Hide();
            //            panel3.Hide();
            //            panel2.Show();
            //            panel2.Dock = DockStyle.Fill;

            //            this.dataGridView1.DataSource = null;
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView1.DataSource = context.WxFriends
            //                    .Where(friend => friend.Type == 0)
            //                    .ToList();
            //            }

            //            break;
            //        }
            //    case "应用程序":
            //        {
            //            panel1.Hide();
            //            panel3.Hide();
            //            panel2.Show();
            //            panel2.Dock = DockStyle.Fill;

            //            this.dataGridView1.DataSource = null;
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView1.DataSource = context.WxFriends
            //                    .Where(friend => friend.Type == 33)
            //                    .ToList();
            //            }

            //            break;
            //        }
            //    case "聊天记录":
            //        {
            //            Name = "聊天记录";
            //            panel1.Hide();
            //            panel2.Hide();
            //            panel3.Show();
            //            dataGridView4.Hide();
            //            dataGridView2.Show();
            //            panel3.Dock = DockStyle.Fill;

            //            this.dataGridView2.Rows.Clear();
            //            this.richTextBoxEx1.Clear();
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView2.Rows.AddRange(
            //                    context.WxFriends
            //                    .Where(friend => friend.Type == 3)
            //                    .Select(new Func<WxFriend, DataGridViewRow>((f) =>
            //                    {
            //                        var row = new DataGridViewRow();
            //                        row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxMessages.Count(m => m.WxId == f.WxId).ToString() });
            //                        return row;
            //                    }))
            //                    .ToArray());
            //            }

            //            break;
            //        }
            //    case "群聊天记录":
            //        {
            //            panel1.Hide();
            //            panel2.Hide();
            //            panel3.Show();
            //            dataGridView4.Hide();
            //            dataGridView2.Show();
            //            panel3.Dock = DockStyle.Fill;

            //            this.dataGridView2.Rows.Clear();
            //            this.richTextBoxEx1.Clear();
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView2.Rows.AddRange(
            //                    context.WxFriends
            //                    .Where(friend => friend.Type == 4)
            //                    .Select(new Func<WxFriend, DataGridViewRow>((f) =>
            //                    {
            //                        var row = new DataGridViewRow();
            //                        row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxMessages.Count(m => m.WxId == f.WxId).ToString() });
            //                        return row;
            //                    }))
            //                    .ToArray());
            //            }

            //            break;
            //        }
            //    case "朋友圈":
            //        {
            //            Name = "朋友圈";
            //            panel1.Hide();
            //            panel2.Hide();
            //            panel3.Show();
            //            dataGridView4.Hide();
            //            dataGridView2.Show();
            //            panel3.Dock = DockStyle.Fill;

            //            this.dataGridView2.Rows.Clear();
            //            this.richTextBoxEx1.Clear();
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView2.Rows.AddRange(
            //                    context.WxFriends
            //                    .Where(friend => friend.Type == 3)
            //                    .Select(new Func<WxFriend, DataGridViewRow>((f) =>
            //                    {
            //                        var row = new DataGridViewRow();
            //                        row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxSns.Count(s => s.WxId == f.WxId).ToString() });
            //                        return row;
            //                    }))
            //                    .ToArray());
            //            }

            //            break;
            //        }
            //    case "新朋友":
            //        {
            //            panel1.Hide();
            //            panel3.Hide();
            //            panel2.Show();
            //            panel2.Dock = DockStyle.Fill;

            //            this.dataGridView1.DataSource = null;
            //            using (SqliteDbContext context = new SqliteDbContext())
            //            {
            //                this.dataGridView1.DataSource = context.WxNewFriend.ToList();
            //            }

            //            break;
            //        }
            //    default:
            //        {
            //            panel2.Hide();
            //            panel3.Hide();
            //            panel1.Show();
            //            panel1.Dock = DockStyle.Fill;

            //            break;
            //        }
            //}
        }
        private void TreeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeName = e.Node.Text as string;
            string SQLCommand = string.Empty;
            string keyword = Program.m_mainform.g_keyword;
            int iKeyword = -9999;
            try
            { iKeyword = Convert.ToInt32(keyword); }
            catch
            { }


            //string id = e.Node.Name;
            int pos =nodeName.IndexOf("（");
            if(pos>0)
                nodeName = nodeName.Substring(0, pos);
            switch (nodeName)
            {
                case "手机基本信息":                    
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;

                        SQLCommand = string.Format(" SELECT phoneNumber as 手机号,model as 品牌,type as 型号,Android as 安卓版本,IMEI FROM phoneinfo WHERE phoneNumber LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR model LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR Android LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR IMEI LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "联系人":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select name as 姓名,phoneNumber as 手机号, district as 地区,company as 单位,Email as 邮箱,remark as 备注 from Contacts WHERE phoneNumber LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR company LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR Email LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR remark LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "短信":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" SELECT phoneNumber as 手机号,content as 内容,datetime as 时间,isSend as 是否接收 FROM Sms WHERE phoneNumber LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR isSend LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "通话记录":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select name as 联系人,phoneNumber as 手机号,datetime as 时间,duration as 通话时长,type as 接收状态 from Calls WHERE phoneNumber LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR datetime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR duration LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "账号信息":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,email as 邮箱,QQid as 关联QQ号 from WXAccount WHERE wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR accountID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR phoneNumber LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR email LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR QQid LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "通讯录":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}",iKeyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                case "好友":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE type in(1,3) and wxID not like '%@chatroom' and wxID not like 'gh_%' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}",iKeyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "群":
                    {
                        Name = "群搜索";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;

                        SQLCommand = string.Format(" select groupID as 群账号,name as 群名,adminWxID as 群主微信号,adminNickname as 群主昵称,groupNum as 成员数,meNickname as 本人群昵称 from WXChatroom WHERE groupID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR name LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR adminWxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR adminNickname LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR meNickname LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR groupNum ={0}", iKeyword);
                        SQLCommand += string.Format(" OR checkinType ={0};", iKeyword);

                        //SQLCommand = string.Format("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE type in(1,3) and wxID like '%@chatroom' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "公众号":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE type in(1,3) and wxID like 'gh_%' and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "应用程序":
                    {                        
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE type=33 and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "其他":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook WHERE type not in(1,3,33) and id in (select id from WXAddressBook where wxID like '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}",iKeyword);
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
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "好友聊天":
                    {
                        Name = "好友聊天";

                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Show();
                        dataGridView4.DataSource = null;
                        richTextBoxEx1.Clear();
                        SQLCommand = string.Format("select nickname as 昵称, WXMessage.wxID as 微信账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and  WXMessage.wxID not like '%@chatroom' and WXMessage.wxID not like 'gh_%' and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}", iKeyword);
                        SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                        SQLCommand += string.Format(" OR status ={0}) group by WXAddressBook.wxID;", iKeyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;
                        break;
                    }
                case "群聊":
                    {
                        Name = "群聊";

                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Show();
                        dataGridView4.DataSource = null;
                        richTextBoxEx1.Clear();
                        SQLCommand = string.Format(" select nickname as 昵称,WXMessage.wxID as 群聊账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like '%@chatroom' and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}", iKeyword);
                        SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                        SQLCommand += string.Format(" OR status ={0}) group by WXAddressBook.wxID;", iKeyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;

                        break;
                    }
                case "公众号讯息":
                    {
                        Name = "公众号讯息";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Show();
                        dataGridView4.DataSource = null;
                        richTextBoxEx1.Clear();
                        SQLCommand = string.Format(" select nickname as 昵称,WXMessage.wxID as 公众号账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'gh_%' and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}", iKeyword);
                        SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                        SQLCommand += string.Format(" OR status ={0}) group by WXAddressBook.wxID;", iKeyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);

                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;

                        break;
                    }
                case "朋友圈":
                    {
                        Name = "朋友圈";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        panel3.Dock = DockStyle.Fill;
                        dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Show();
                        dataGridView4.DataSource = null;
                        richTextBoxEx1.Clear();
                        SQLCommand = string.Format(" select nickname as 昵称,WXSns.wxID as 微信账号, count(WXSns.wxID) as 朋友圈条数 from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXSns.id in  (select id from WXSns where wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR comment LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR type ={0}) group by WXAddressBook.wxID;", iKeyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);   
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;

                        break;
                    }
                case "新朋友":
                    {                    
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                        dataGridView3.DataSource = null;
                        SQLCommand = string.Format(" select wxID as 微信账号,nickname as 昵称,sex as 性别,sign as 个性签名, district as 地区,remark as 备注,description as 描述,contentVerify as 好友请求内容,lastModifiedTime as 最后更新时间  from WXNewFriend where wxID LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR nickname LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR sex LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR sign LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR remark LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR district LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR avatarPath LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR description LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR contentVerify LIKE '%{0}%'", keyword);
                        SQLCommand += string.Format(" OR lastModifiedTime LIKE '%{0}%';", keyword);
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;

                    }
                default:
                    {
                        panel2.Hide();
                        panel3.Hide();
                        panel1.Show();
                        panel1.Dock = DockStyle.Fill;
                        
                        break;
                    }
            }
        }
        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            richTextBoxEx1.Clear();
            string wxID = this.dataGridView2.Rows[e.RowIndex].Cells[1].Value as string;

            if (Name == "朋友圈")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXSns.type as TYPE,content,createTime,comment,supportNum,commentNum,avatarPath from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXSns.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBoxEx1.SelectionColor = Color.DimGray;
                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]))) //判断文件是否存在
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]));
                        Bitmap bmp = new Bitmap(img, 25, 22);
                        Clipboard.SetDataObject(bmp);
                        DataFormats.Format dataFormat =
                        DataFormats.GetFormat(DataFormats.Bitmap);
                        if (richTextBoxEx1.CanPaste(dataFormat))
                            richTextBoxEx1.Paste(dataFormat);
                    }
                    richTextBoxEx1.AppendText("(");
                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                    richTextBoxEx1.AppendText(")");
                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                    richTextBoxEx1.SelectionColor = Color.Blue;
                    // richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");


                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);
                    SelectUrl.print_MsgOrUrl(content, type, richTextBoxEx1);

                    //if(type==1)   //图片加载
                    //{
                    //    int startindex, endindex;
                    //    string title = "";
                    //    string url = "";
                    //    startindex = content.IndexOf("【文字】");
                    //    if (startindex == -1)    //没有文字，只有图片
                    //    {
                    //        endindex = content.IndexOf("【图片文件】");

                    //        if (endindex >= 0)
                    //        {
                    //            for (int j = 0; j < 20; j++)
                    //            {
                    //                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                    //                if (startindex > 0)
                    //                {
                    //                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                    //                    //加载图片直接显示
                    //                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + url)) //判断文件是否存在
                    //                    {
                    //                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + url);
                    //                        Bitmap bmp = new Bitmap(img, 50, 35);
                    //                        Clipboard.SetDataObject(bmp);
                    //                        DataFormats.Format dataFormat =
                    //                        DataFormats.GetFormat(DataFormats.Bitmap);
                    //                        if (richTextBoxEx1.CanPaste(dataFormat))
                    //                            richTextBoxEx1.Paste(dataFormat);
                    //                    }

                    //                    endindex = startindex;
                    //                }
                    //                else
                    //                {
                    //                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                    //                    //加载图片直接显示
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        endindex = content.IndexOf("【图片文件】");

                    //        if (endindex >= 0)
                    //        {
                    //            title = content.Substring(startindex + 4, endindex - startindex - 4);
                    //            richTextBoxEx1.AppendText(title + "\n");
                    //            for (int j = 0; j < 20; j++)
                    //            {
                    //                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                    //                if (startindex > 0)
                    //                {
                    //                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                    //                    //加载图片直接显示
                    //                    endindex = startindex;
                    //                }
                    //                else
                    //                {
                    //                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                    //                    //加载图片直接显示
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }

                    //    richTextBoxEx1.AppendText("\n");
                    //}
                    //else
                    //    SelectUrl.print_MsgOrUrl(content, type, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;

                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
            else if (Name == "好友聊天")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type as TYPE,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID not like 'gh_%' and WXMessage.wxID not like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string path = Convert.ToString(dt1.Rows[i]["path"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);

                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Program.m_mainform.g_str_nickname);
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Program.m_mainform.g_str_wxID + "\n");

                        if (is_avatarPath) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + str_avatarPath);
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");                    

                    }
                    else
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        if (type != 10000 && type != 50 && type != 47 && type != 10002 && type != 64 && type != 50 && type != 570425393)
                        {
                            richTextBoxEx1.AppendText("(");
                            richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                            richTextBoxEx1.AppendText(")");
                            richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                            if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                            {
                                System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                                Bitmap bmp1 = new Bitmap(img1, 25, 22);
                                Clipboard.SetDataObject(bmp1);
                                DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                                if (richTextBoxEx1.CanPaste(dataFormat1))
                                    richTextBoxEx1.Paste(dataFormat1);
                            }
                        }
                        else
                            richTextBoxEx1.AppendText("系统消息");

                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");

                    }
                    
                    if (type == 1 || type == 10000 || type == 50 || type == 47 || type == 10002 || type == 64 || type == 50 || type == 570425393)
                        richTextBoxEx1.AppendText(content + "\n");
                    else if (type == 436207665)   //红包
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else if (type == 419430449)   //转账
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        
                        int startindex, endindex;
                        string title = "";
                        string des = "";
                        startindex = content.IndexOf("<title>");
                        endindex = content.IndexOf("<des>");
                        if (startindex >= 0 && endindex > startindex)
                        {
                            title = content.Substring(startindex + 7, endindex - startindex - 7);
                            richTextBoxEx1.AppendText(title + "\n");
                            des = content.Substring(endindex + 5, content.Length - endindex - 5);
                            richTextBoxEx1.AppendText(des + "\n");
                            if (status == 3)
                                richTextBoxEx1.AppendText("（已收）\n");
                        }
                        else
                        {
                            richTextBoxEx1.AppendText(content + "\n");
                            if (status == 3)
                                richTextBoxEx1.AppendText("（已收）\n");
                        }
                    }
                    else if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else
                        SelectUrl.print_MsgOrUrl(content, type, path, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
            else if (Name == "群聊")//type=3/34/43/48/49/436207665/419430449 区分不同wxID
            {
               // string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";

                string sql = "select type,content,isSend,createTime,path,status from WXMessage where wxID like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;


                for (int i = 0; i < count; i++)
                {                    
                    //if (i == 7000) MessageBox.Show("7000");
                    
                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string path = Convert.ToString(dt1.Rows[i]["path"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);

                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)  //本人发送,content里不含微信ID信息
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        int pos = content.IndexOf(":");                        
                        if (pos > 0)
                        {
                            if (type == 1)
                                content = content.Substring(pos + 2, content.Length - pos - 2);
                            else
                                content = content.Substring(pos + 1, content.Length - pos - 1);
                        }

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Program.m_mainform.g_str_nickname);
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Program.m_mainform.g_str_wxID + "\n");

                        if (is_avatarPath) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + str_avatarPath);
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");

                    }
                    else      //表示是收取的消息,从content里提取微信ID，再显示该微信ID发送的信息
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        int pos = content.IndexOf(":");
                        string WXID = "";
                        if (pos > 0)
                        {
                            WXID = content.Substring(0, pos);

                            if (type == 1)
                                content = content.Substring(pos + 2, content.Length - pos - 2);
                            else
                                content = content.Substring(pos + 1, content.Length - pos - 1);                       

                            //获取他人微信ID、昵称、头像
                            sql = "select nickname,avatarPath from WXAddressbook where wxID='"+ WXID + "';";
                            SQLiteDataAdapter mAdapter0 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                            DataTable dt0 = new DataTable();
                            mAdapter0.Fill(dt0);
                        
                            if (dt0.Rows.Count == 1)
                            {                           
                                richTextBoxEx1.AppendText("(");
                                richTextBoxEx1.AppendText(Convert.ToString(dt0.Rows[0]["nickname"]));
                                richTextBoxEx1.AppendText(")");
                                richTextBoxEx1.AppendText(WXID + "\n");

                                if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt0.Rows[0]["avatarPath"]))) //判断文件是否存在
                                {
                                    System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt0.Rows[0]["avatarPath"]));
                                    Bitmap bmp1 = new Bitmap(img1, 25, 22);
                                    Clipboard.SetDataObject(bmp1);
                                    DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                                    if (richTextBoxEx1.CanPaste(dataFormat1))
                                        richTextBoxEx1.Paste(dataFormat1);
                                }
                            }
                        }
                        else
                            richTextBoxEx1.AppendText("系统消息");
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");

                    }                    

                    if (type == 1 || type == 10000 || type == 50 || type == 47 || type == 10002 || type == 64 || type == 50 || type == 570425393)
                        richTextBoxEx1.AppendText(content + "\n");
                    else if (type == 436207665)   //红包
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else if (type == 419430449)   //转账
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        int startindex, endindex;
                        string title = "";
                        string des = "";
                        startindex = content.IndexOf("<title>");
                        endindex = content.IndexOf("<des>");
                        if (startindex >= 0 && endindex > startindex)
                        {
                            title = content.Substring(startindex + 7, endindex - startindex - 7);
                            richTextBoxEx1.AppendText(title + "\n");
                            des = content.Substring(endindex + 5, content.Length - endindex - 5);
                            richTextBoxEx1.AppendText(des + "\n");
                            if (status == 3)
                                richTextBoxEx1.AppendText("（已收）\n");
                        }
                        else
                        {
                            richTextBoxEx1.AppendText(content + "\n");
                            if (status == 3)
                                richTextBoxEx1.AppendText("（已收）\n");
                        }
                    }
                    else if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else
                        SelectUrl.print_MsgOrUrl(content, type, path, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
            else if (Name == "公众号讯息")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID like 'gh_%' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                    }
                    else
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                    }

                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    if (type == 285212721)
                        SelectUrl.print_MsgOrUrl(Convert.ToString(dt1.Rows[i]["content"]), type, richTextBoxEx1);
                    if (type == 49)
                        SelectUrl.print_MsgOrUrl(Convert.ToString(dt1.Rows[i]["content"]), type,"", richTextBoxEx1);

                    if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView1.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView2.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView2.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView2.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dataGridView3.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView3.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView3.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgV1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCheckBoxCell dgcc = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[0];
                Boolean flag = Convert.ToBoolean(dgcc.Value);
                dgcc.Value = flag == true ? false : true;
            }
            catch (Exception)
            {
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FormGjglZzss form = new FormGjglZzss();
            form.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {              

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if(Program.m_mainform.g_newSearch)
                {
                    long totalNum_ss = Program.m_mainform.g_Num_WXAccount_ss;        //数据总条数      
                    totalNum_ss += Program.m_mainform.g_Num_WXChatroom_ss;
                    //totalNum_ss += Program.m_mainform.g_Num_WXChatroomList_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXMessage_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXNewFriend_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXSns_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXAddressBook_friend_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXAddressBook_gh_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXAddressBook_app_ss;
                    totalNum_ss += Program.m_mainform.g_Num_WXAddressBook_other_ss;
                    totalNum_ss += Program.m_mainform.g_Num_phoneinfo_ss;
                    totalNum_ss += Program.m_mainform.g_Num_Sms_ss;
                    totalNum_ss += Program.m_mainform.g_Num_Calls_ss;
                    totalNum_ss += Program.m_mainform.g_Num_Contacts_ss;

                    //treeView2加载
                    //添加根节点
                    TreeNode nodeAJ = new TreeNode();
                    nodeAJ.Text = Program.m_mainform.g_ajName + @"（搜索词:"+ Program.m_mainform.g_keyword + "/数量:" + Convert.ToString(totalNum_ss) + "）";
                    treeView2.Nodes.Add(nodeAJ);
                    TreeNode nodeZJ = new TreeNode();
                    nodeZJ.Text = Program.m_mainform.g_zjName + @"（搜索词:" + Program.m_mainform.g_keyword + "/数量:" + Convert.ToString(totalNum_ss) + "）";
                    nodeAJ.Nodes.Add(nodeZJ);           //添加证据结点
                    TreeNode nodeBase = new TreeNode("基础信息" + @"（" + Convert.ToString(Program.m_mainform.g_Num_phoneinfo_ss + Program.m_mainform.g_Num_Sms_ss + Program.m_mainform.g_Num_Calls_ss + Program.m_mainform.g_Num_Contacts_ss) + "）");
                    nodeZJ.Nodes.Add(nodeBase);

                    int BaseNum = Program.m_mainform.checkBaseList.Count;
                    for (int i = 0; i < BaseNum; i++)
                    {
                        if ("手机基本信息" == Program.m_mainform.checkBaseList[i])
                        {
                            TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_phoneinfo_ss + "）");
                            nodeBase.Nodes.Add(node);
                        }
                        if ("短信" == Program.m_mainform.checkBaseList[i])
                        {
                            TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Sms_ss + "）");
                            nodeBase.Nodes.Add(node);
                        }
                        if ("通话记录" == Program.m_mainform.checkBaseList[i])
                        {
                            TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Calls_ss + "）");
                            nodeBase.Nodes.Add(node);
                        }
                        if ("联系人" == Program.m_mainform.checkBaseList[i])
                        {
                            TreeNode node = new TreeNode(Program.m_mainform.checkBaseList[i] + @"（" + Program.m_mainform.g_Num_Contacts_ss + "）");
                            nodeBase.Nodes.Add(node);
                        }                        
                    }

                    int FileNum = Program.m_mainform.checkFileList.Count;
                    if (FileNum > 0)
                    {
                        TreeNode nodeFile = new TreeNode("文件信息(0)");
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
                                TreeNode node1 = new TreeNode(Program.m_mainform.g_str_wxID + "(" + Program.m_mainform.g_str_nickname + ")（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount_ss + Program.m_mainform.g_Num_WXNewFriend_ss + Program.m_mainform.g_Num_WXMessage_ss + Program.m_mainform.g_Num_WXChatroom_ss + Program.m_mainform.g_Num_WXAddressBook_friend_ss+ Program.m_mainform.g_Num_WXAddressBook_gh_ss + Program.m_mainform.g_Num_WXAddressBook_app_ss + Program.m_mainform.g_Num_WXAddressBook_other_ss + Program.m_mainform.g_Num_WXSns_ss) + "）");
                                node.Nodes.Add(node1);
                                TreeNode node11 = new TreeNode("账号信息" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount_ss) + "）");
                                node1.Nodes.Add(node11);
                                TreeNode node12 = new TreeNode("通讯录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXChatroom_ss + Program.m_mainform.g_Num_WXAddressBook_friend_ss + Program.m_mainform.g_Num_WXAddressBook_gh_ss + Program.m_mainform.g_Num_WXAddressBook_app_ss+ Program.m_mainform.g_Num_WXAddressBook_other_ss) + "）");
                                node1.Nodes.Add(node12);
                                TreeNode node121 = new TreeNode("好友" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_friend_ss) + "）");
                                node12.Nodes.Add(node121);
                                TreeNode node122 = new TreeNode("公众号" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_gh_ss) + "）");
                                node12.Nodes.Add(node122);
                                TreeNode node123 = new TreeNode("群" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXChatroom_ss) + "）");
                                node12.Nodes.Add(node123);
                                TreeNode node124 = new TreeNode("应用程序" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_app_ss) + "）");
                                node12.Nodes.Add(node124);
                                TreeNode node125 = new TreeNode("其他" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook_other_ss) + "）");
                                node12.Nodes.Add(node125);


                                TreeNode node13 = new TreeNode("聊天记录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_ss) + "）");
                                node1.Nodes.Add(node13);
                                TreeNode node131 = new TreeNode("好友聊天" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_friend_ss) + "）");
                                node13.Nodes.Add(node131);
                                TreeNode node132 = new TreeNode("群聊" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_chatroom_ss) + "）");
                                node13.Nodes.Add(node132);
                                TreeNode node133 = new TreeNode("公众号讯息" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage_gh_ss) + "）");
                                node13.Nodes.Add(node133);

                                TreeNode node14 = new TreeNode("朋友圈" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXSns_ss) + "）");
                                node1.Nodes.Add(node14);
                                //TreeNode node141 = new TreeNode("本人的朋友圈");
                                //node14.Nodes.Add(node141);
                                //TreeNode node142 = new TreeNode("好友的朋友圈");
                                //node14.Nodes.Add(node142);
                                TreeNode node15 = new TreeNode("新朋友" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXNewFriend_ss) + "）");
                                node1.Nodes.Add(node15);
                            }
                        }
                    }
                    treeView2.ExpandAll();
                    Program.m_mainform.g_newSearch = false;
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            richTextBoxEx1.Clear();
            string groupID = this.dataGridView3.Rows[e.RowIndex].Cells[0].Value as string;

            if (Name == "群")
            {
                panel1.Hide();
                panel2.Show();
                panel3.Hide();
                dataGridView1.DataSource = null;
                string sql = "select wxID as 微信账号,nickname as 昵称,groupNickname as 群昵称,sign as 签名,district as 所在地 from WXChatroomList where groupID='"+ groupID +"';";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);//,checkinType as 入群方式
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.Show();
            }
            if (Name == "群搜索")
            {
                panel1.Hide();
                panel2.Show();
                panel3.Hide();
                dataGridView1.DataSource = null;
                string sql = "select wxID as 微信账号,nickname as 昵称,groupNickname as 群昵称,sign as 签名,district as 所在地 from WXChatroomList where groupID='" + groupID + "';";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);//,checkinType as 入群方式
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.Show();
            }
        }
        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    DataGridViewCheckBoxCell dgcc = (DataGridViewCheckBoxCell)this.dataGridView4.Rows[e.RowIndex].Cells[0];
            //    Boolean flag = Convert.ToBoolean(dgcc.Value);
            //    dgcc.Value = flag == true ? false : true;
            //}
            //catch (Exception)
            //{
            //}

            if (e.RowIndex < 0)
            {
                return;
            }

            richTextBoxEx1.Clear();
            string wxID = this.dataGridView4.Rows[e.RowIndex].Cells[1].Value as string;
            string keyword = Program.m_mainform.g_keyword;
            int iKeyword = -9999;
            try
            { iKeyword = Convert.ToInt32(keyword); }
            catch
            { }
            if (Name == "朋友圈")
            {
                string SQLCommand = " select WXAddressBook.wxID as WXID,nickname,WXSns.type as TYPE,content,createTime,comment,supportNum,commentNum,avatarPath from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXSns.wxID='" + wxID + "'";
                SQLCommand += string.Format(" and WXSns.id in  (select id from WXSns where wxID LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR comment LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR type ={0}) order by createTime;", iKeyword);
                //string sql = "select WXAddressBook.wxID as WXID,nickname,WXSns.type as TYPE,content,createTime,comment,supportNum,commentNum,avatarPath from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXSns.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBoxEx1.SelectionColor = Color.DimGray;
                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]))) //判断文件是否存在
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]));
                        Bitmap bmp = new Bitmap(img, 25, 22);
                        Clipboard.SetDataObject(bmp);
                        DataFormats.Format dataFormat =
                        DataFormats.GetFormat(DataFormats.Bitmap);
                        if (richTextBoxEx1.CanPaste(dataFormat))
                            richTextBoxEx1.Paste(dataFormat);
                    }
                    richTextBoxEx1.AppendText("(");
                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                    richTextBoxEx1.AppendText(")");
                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                    richTextBoxEx1.SelectionColor = Color.Blue;
                    // richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");


                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);
                    SelectUrl.print_MsgOrUrl(content, type, richTextBoxEx1);

                    //if(type==1)   //图片加载
                    //{
                    //    int startindex, endindex;
                    //    string title = "";
                    //    string url = "";
                    //    startindex = content.IndexOf("【文字】");
                    //    if (startindex == -1)    //没有文字，只有图片
                    //    {
                    //        endindex = content.IndexOf("【图片文件】");

                    //        if (endindex >= 0)
                    //        {
                    //            for (int j = 0; j < 20; j++)
                    //            {
                    //                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                    //                if (startindex > 0)
                    //                {
                    //                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                    //                    //加载图片直接显示
                    //                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + url)) //判断文件是否存在
                    //                    {
                    //                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + url);
                    //                        Bitmap bmp = new Bitmap(img, 50, 35);
                    //                        Clipboard.SetDataObject(bmp);
                    //                        DataFormats.Format dataFormat =
                    //                        DataFormats.GetFormat(DataFormats.Bitmap);
                    //                        if (richTextBoxEx1.CanPaste(dataFormat))
                    //                            richTextBoxEx1.Paste(dataFormat);
                    //                    }

                    //                    endindex = startindex;
                    //                }
                    //                else
                    //                {
                    //                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                    //                    //加载图片直接显示
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        endindex = content.IndexOf("【图片文件】");

                    //        if (endindex >= 0)
                    //        {
                    //            title = content.Substring(startindex + 4, endindex - startindex - 4);
                    //            richTextBoxEx1.AppendText(title + "\n");
                    //            for (int j = 0; j < 20; j++)
                    //            {
                    //                startindex = content.IndexOf("【图片文件】", endindex + 1, content.Length - endindex - 1);
                    //                if (startindex > 0)
                    //                {
                    //                    url = content.Substring(endindex + 6, startindex - endindex - 6);
                    //                    //加载图片直接显示
                    //                    endindex = startindex;
                    //                }
                    //                else
                    //                {
                    //                    url = content.Substring(endindex + 6, content.Length - endindex - 6);
                    //                    //加载图片直接显示
                    //                    break;
                    //                }
                    //            }
                    //        }
                    //    }

                    //    richTextBoxEx1.AppendText("\n");
                    //}
                    //else
                    //    SelectUrl.print_MsgOrUrl(content, type, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;

                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
            else if (Name == "好友聊天")
            {
                string SQLCommand = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type as TYPE,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and  WXMessage.wxID not like '%@chatroom' and WXMessage.wxID not like 'gh_%' and WXMessage.wxID='" + wxID + "'"; 
                SQLCommand += string.Format(" and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR type ={0}", iKeyword);
                SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                SQLCommand += string.Format(" OR status ={0}) order by createTime;", iKeyword);
                //string SQLCommand = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type as TYPE,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID not like 'gh_%' and WXMessage.wxID not like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");
                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");                    

                    }
                    else
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        
                    }
                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string path= Convert.ToString(dt1.Rows[i]["path"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);

                    if (type==1 && type == 10000 && type == 50 && type == 47 && type == 10002 && type == 64 && type == 50 && type == 570425393)
                        richTextBoxEx1.AppendText(content + "\n");
                    else if(type == 436207665)   //红包
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if(status==3)
                             richTextBoxEx1.AppendText(content + " （已收）\n");
                        else if (status == 2)
                             richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else if (type == 419430449)   //转账
                    {
                        int startindex, endindex;
                        string title = "";
                        string des = "";
                        startindex = content.IndexOf("<title>");
                        endindex = content.IndexOf("<des>");
                        if (startindex >= 0 && endindex > startindex)
                        {
                            title = content.Substring(startindex + 7, endindex - startindex - 7);
                            richTextBoxEx1.AppendText(title + "\n");
                            des = content.Substring(endindex + 5, content.Length - endindex - 5);
                            richTextBoxEx1.AppendText(des + "\n");
                        }
                        else
                            richTextBoxEx1.AppendText(content + "\n");
                        
                    }
                    else if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        else if (status == 2)
                            richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else
                        SelectUrl.print_MsgOrUrl(content, type, path,richTextBoxEx1); 
                                       
                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }

            }
            else if (Name == "群聊")
            {
                string SQLCommand = " select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like '%@chatroom' and WXMessage.wxID='" + wxID + "'";
                SQLCommand += string.Format("  and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR type ={0}", iKeyword);
                SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                SQLCommand += string.Format(" OR status ={0}) order by createTime;", iKeyword);
                //string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;


                for (int i = 0; i < count; i++)
                {
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        
                    }
                    else
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");

                    }

                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    string path = Convert.ToString(dt1.Rows[i]["path"]);
                    string content = Convert.ToString(dt1.Rows[i]["content"]);

                    if (type == 1 || type == 10000 || type == 50 || type == 47 || type == 10002 || type == 64 || type == 50 || type == 570425393)
                        richTextBoxEx1.AppendText(content + "\n");
                    else if (type == 436207665)   //红包
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else if (type == 419430449)   //转账
                    {
                        int startindex, endindex;
                        string title = "";
                        string des = "";
                        startindex = content.IndexOf("<title>");
                        endindex = content.IndexOf("<des>");
                        if (startindex >= 0 && endindex > startindex)
                        {
                            title = content.Substring(startindex + 7, endindex - startindex - 7);
                            richTextBoxEx1.AppendText(title + "\n");
                            des = content.Substring(endindex + 5, content.Length - endindex - 5);
                            richTextBoxEx1.AppendText(des + "\n");
                        }
                        else
                            richTextBoxEx1.AppendText(content + "\n");

                    }
                    else if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(content + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }
                    else
                        SelectUrl.print_MsgOrUrl(content, type, path, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }

                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }
            }
            else if (Name == "公众号讯息")
            {
                string SQLCommand = " select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'gh_%' and WXMessage.wxID='" + wxID + "'";
                SQLCommand += string.Format("  and WXMessage.id in  (select id from WXMessage where wxID LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR createTime LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR content LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR path LIKE '%{0}%'", keyword);
                SQLCommand += string.Format(" OR type ={0}", iKeyword);
                SQLCommand += string.Format(" OR isSend ={0}", iKeyword);
                SQLCommand += string.Format(" OR status ={0}) order by createTime;", iKeyword);
             //   string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID like 'gh_%' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(SQLCommand, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {   
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Red;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                    }
                    else
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;

                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBoxEx1.CanPaste(dataFormat1))
                                richTextBoxEx1.Paste(dataFormat1);
                        }
                        richTextBoxEx1.AppendText("\n");
                        richTextBoxEx1.SelectionColor = Color.Blue;
                        //richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                    }

                    int type = Convert.ToInt32(dt1.Rows[i]["TYPE"]);
                    if (type==285212721)
                        SelectUrl.print_MsgOrUrl(Convert.ToString(dt1.Rows[i]["content"]), type, richTextBoxEx1);

                    if (type == 318767153)   //服务通知
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
                            richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + " （已收）\n");
                        //else if (status == 2)
                        //    richTextBoxEx1.AppendText(content + " （未收）\n");
                    }                    

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 20)
                {
                    MessageBox.Show("数据加载完成！");
                    Thread.Sleep(500);
                }

            }
        }

        private void richTextBoxEx1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            /*
             设置根目录，从message表的path字段中取出的值诸如：
             \com.tencent.mm\MicroMsg\9a444f2bffa22236d9e4313dc93683c8\video\1652293005198de450297040.mp4
             \com.tencent.mm\MicroMsg\9a444f2bffa22236d9e4313dc93683c8\voice2\e1\7a\msg_2216490530198de4502faaa104.amr
             \tencent\MicroMsg\Download\rcontact.txt
             但是，这只是一个相对的路径(可以把它称为绝对路径的后一部分)，需要进行拼接形成完整的路径
             */
            string baseDir = "D:";//假设baseDir是绝对路径的前一部分
            string like = e.LinkText;
            int index = like.IndexOf('\\');//如果链接中含有反斜杠，说明这是一个文件
            if (index != -1)//如果是文件
            {//文件
                string file_dir = string.Format("{0}{1}", baseDir, like.Substring(index));//把前一部分和后一部分拼接成绝对路径
                //Console.WriteLine(file_dir);
                string result = WechatLinkFile.display_file(file_dir);//打开文件
                if (result != "")
                {
                    MessageBox.Show(string.Format("{0}{1}\n{2}", "打开文件异常：", file_dir, result));
                }
            }
            else
            {//如果是url链接
                try
                {
                    int id = like.IndexOf("http");
                    //Console.WriteLine(like.Substring(id));
                    WechatLinkFile.display_url(like.Substring(id));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                
            }
        }       
    }

    //public class TreeNodeTypes
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public string Value { get; set; }

    //    public int ParentId { get; set; }
    //}
}
