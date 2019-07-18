﻿using System;
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

namespace WinAppDemo.Controls
{
    public partial class UcZjzs : UserControl
    {
        public SQLiteConnection conn = null;
        public string WDID_Nickname = "";   //用于记录微信账号（昵称）

        private List<TreeNodeTypes> Types;
        public UcZjzs()
        {
            InitializeComponent();
        }

        private void UcZjzs_Load(object sender, EventArgs e)
        {             
            //获取数据文件的路径
            // string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";
            string dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\83ed8b966b0f9a2df369b6e6fd832e71\\midwxtrans.db";// + Program.m_mainform.g_workPath+"\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";

            //创建数据库实例，指定文件位置
            Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
            Program.m_mainform.g_conn.Open();
            SQLiteDataAdapter mAdapter = new SQLiteDataAdapter("select name as 名称,seq as 数量 from sqlite_sequence;", Program.m_mainform.g_conn);//select * from WXAccount
            
            DataTable dt = new DataTable();
            mAdapter.Fill(dt);

            //绑定数据到DataGridView
            dataGridView3.DataSource = dt;
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            dataGridView3.Show();

            dbPath = "Data Source =D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\PhoneData\\PhoneData.db";   //打开短信、联系人、通话记录等数据库
            conn = new SQLiteConnection(dbPath);
            conn.Open();


            Types = new List<TreeNodeTypes>()
            {
                new TreeNodeTypes() {Id = 1, Name = @"证据（47\13433\13480）", Value = "1", ParentId = 0},
                new TreeNodeTypes() {Id = 2, Name = @"手机信息（47\13433\13480）", Value = "2", ParentId = 1},
                new TreeNodeTypes() {Id = 3, Name = @"即时通讯（47\13433\13480）", Value = "3", ParentId = 1},
                new TreeNodeTypes() {Id = 4, Name = @"基本信息（47\13433\13480）", Value = "4", ParentId = 2},
                new TreeNodeTypes() {Id = 5, Name = @"通讯录（47\13433\13480）", Value = "5", ParentId = 2},
                new TreeNodeTypes() {Id = 6, Name = @"微信（47\13433\13480）", Value = "6", ParentId = 3},
            };

            //string Name = "";
            using (SqliteDbContext context = new SqliteDbContext())
            {//ca9529dc14475dbcc7e8553e77ad7d0b
             //   context.Database.Connection.ConnectionString = "Data Source="+ "D:\\手机取证工作路径设置\\案件20190707093739\\HONORV2020190701094546\\AppData\\Weixin\\a12788cdac6ba28270d03cc2df9a0122" + "\\midwxtrans.db";
                context.WxAccounts.ToList().ForEach(acc =>
                {
                    int index = Types.Count + 1;
                    Types.Add(new TreeNodeTypes() { Id = index, Name = acc.Sign, ParentId = 6, Value = acc.WxId });
                    Types.Add(new TreeNodeTypes() { Id = index + 1, Name = "通讯录", ParentId = index, Value = "通讯录" });
                    Types.Add(new TreeNodeTypes() { Id = index + 2, Name = "公众号", ParentId = index, Value = "公众号" });
                    Types.Add(new TreeNodeTypes() { Id = index + 3, Name = "聊天记录", ParentId = index, Value = "聊天记录" });
                    Types.Add(new TreeNodeTypes() { Id = index + 4, Name = "群聊天记录", ParentId = index, Value = "群聊天记录" });
                    Types.Add(new TreeNodeTypes() { Id = index + 5, Name = "应用程序", ParentId = index, Value = "应用程序" });
                    Types.Add(new TreeNodeTypes() { Id = index + 6, Name = "朋友圈", ParentId = index, Value = "朋友圈" });
                    Types.Add(new TreeNodeTypes() { Id = index + 7, Name = "新朋友", ParentId = index, Value = "新朋友" });
                });
            }


            var topNode = new TreeNode();
            topNode.Name = "0";
            topNode.Text = Program.m_mainform.g_ajName + @"（已删除\未删除\总共）"; // @"案件（已删除\未删除\总共）";
            treeView1.Nodes.Add(topNode);
            Bind(topNode, Types, 0);

            treeView1.ExpandAll();

            //添加根节点
            TreeNode nodeAJ = new TreeNode();
            nodeAJ.Text = Program.m_mainform.g_ajName + @"（已删除\未删除\总共）";
            treeView2.Nodes.Add(nodeAJ);

            TreeNode nodeZJ = new TreeNode();
            nodeZJ.Text = Program.m_mainform.g_zjName + @"（ \ \ ）";
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


            string sql = "select wxID,nickname from WXAccount;";
            SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
            DataTable dt1 = new DataTable();
            mAdapter1.Fill(dt1);

           if( dt1.Rows.Count==1)    
                WDID_Nickname = Convert.ToString(dt1.Rows[0]["wxID"])+"(" + Convert.ToString(dt1.Rows[0]["nickname"]) + ")";

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
                        TreeNode node1 = new TreeNode(WDID_Nickname);
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
                        TreeNode node141 = new TreeNode("本人的朋友圈");
                        node14.Nodes.Add(node141);
                        TreeNode node142 = new TreeNode("好友的朋友圈");
                        node14.Nodes.Add(node142);
                    }
                }
            }
            treeView2.ExpandAll();

        }

        private void Bind(TreeNode parNode, List<TreeNodeTypes> list, int nodeId)
        {
            var childList = list.FindAll(t => t.ParentId == nodeId).OrderBy(t => t.Id);

            foreach (var urlTypese in childList)
            {
                var node = new TreeNode();
                node.Name = urlTypese.Id.ToString();
                node.Text = urlTypese.Name;
                node.Tag = urlTypese.Value;
                parNode.Nodes.Add(node);
                Bind(node, list, urlTypese.Id);
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string wxid = e.Node.Tag as string;
            string id = e.Node.Name;

            switch (wxid)
            {
                case "通讯录":
                    {
                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;

                        this.dataGridView1.DataSource = null;
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView1.DataSource = context.WxFriends
                                .Where(friend => friend.Type == 3)
                                .ToList();
                        }

                        break;
                    }
                case "公众号":
                    {
                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;

                        this.dataGridView1.DataSource = null;
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView1.DataSource = context.WxFriends
                                .Where(friend => friend.Type == 0)
                                .ToList();
                        }

                        break;
                    }
                case "应用程序":
                    {
                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;

                        this.dataGridView1.DataSource = null;
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView1.DataSource = context.WxFriends
                                .Where(friend => friend.Type == 33)
                                .ToList();
                        }

                        break;
                    }
                case "聊天记录":
                    {
                        Name = "聊天记录";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        dataGridView4.Hide();
                        dataGridView2.Show();
                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView2.Rows.AddRange(
                                context.WxFriends
                                .Where(friend => friend.Type == 3)
                                .Select(new Func<WxFriend, DataGridViewRow>((f) =>
                                {
                                    var row = new DataGridViewRow();
                                    row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxMessages.Count(m => m.WxId == f.WxId).ToString() });
                                    return row;
                                }))
                                .ToArray());
                        }

                        break;
                    }
                case "群聊天记录":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        dataGridView4.Hide();
                        dataGridView2.Show();
                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView2.Rows.AddRange(
                                context.WxFriends
                                .Where(friend => friend.Type == 4)
                                .Select(new Func<WxFriend, DataGridViewRow>((f) =>
                                {
                                    var row = new DataGridViewRow();
                                    row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxMessages.Count(m => m.WxId == f.WxId).ToString() });
                                    return row;
                                }))
                                .ToArray());
                        }

                        break;
                    }
                case "朋友圈":
                    {
                        Name = "朋友圈";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        dataGridView4.Hide();
                        dataGridView2.Show();
                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView2.Rows.AddRange(
                                context.WxFriends
                                .Where(friend => friend.Type == 3)
                                .Select(new Func<WxFriend, DataGridViewRow>((f) =>
                                {
                                    var row = new DataGridViewRow();
                                    row.CreateCells(this.dataGridView2, new[] { f.NickName, context.WxSns.Count(s => s.WxId == f.WxId).ToString() });
                                    return row;
                                }))
                                .ToArray());
                        }

                        break;
                    }
                case "新朋友":
                    {
                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;

                        this.dataGridView1.DataSource = null;
                        using (SqliteDbContext context = new SqliteDbContext())
                        {
                            this.dataGridView1.DataSource = context.WxNewFriend.ToList();
                        }

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
        private void TreeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string nodeName = e.Node.Text as string;
            //string id = e.Node.Name;

            switch (nodeName)
            {
                case "手机基本信息":                    
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();
                       
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where type =3 and wxID like 'wxid_%';", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        dataGridView3.Dock = DockStyle.Fill;
                        dataGridView3.Show();
                        break;
                    }
                case "群":
                    {
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Hide();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where wxID like '%@chatroom';", Program.m_mainform.g_conn);
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where wxID like 'gh_%';", Program.m_mainform.g_conn);
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,accountID as 微信号,phoneNumber as 手机号,sign as 个性签名,nickname as 昵称,sex as 性别, district as 地区,remark as 备注,description as 描述,chatFrom as 共同群聊来源 from WXAddressBook where type!=33 and type!=3 and wxID not like 'wxid_%'and wxID not like 'gh_%'and wxID not like '%@chatroom';", Program.m_mainform.g_conn);
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

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称, WXMessage.wxID as 微信账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'wxid_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;
                        dataGridView4.Show();
                        break;
                    }
                case "群聊":
                    {
                        Name = "群聊";                       

                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();

                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 群聊账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like '%@chatroom' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;
                        dataGridView4.Show();

                        break;
                    }
                case "公众号讯息":
                    {
                        Name = "公众号讯息";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();

                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 公众号账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'gh_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;
                        dataGridView4.Show();

                        break;
                    }
                case "朋友圈":
                    {
                        Name = "朋友圈";
                        panel1.Hide();
                        panel2.Hide();
                        panel3.Show();
                        
                        panel3.Dock = DockStyle.Fill;

                        this.dataGridView2.Rows.Clear();
                        this.richTextBox1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXSns.wxID as 微信账号, count(WXSns.wxID) as 朋友圈条数 from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXAddressBook.type=3 group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;
                        dataGridView4.Show();

                        break;
                    }
                case "新朋友":
                    {                     
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,nickname as 昵称,sex as 性别,sign as 个性签名, district as 地区,remark as 备注,description as 描述,contentVerify as 好友请求内容,lastModifiedTime as 最后更新时间  from WXNewFriend;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView1.DataSource = dt1;

                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;
                     //   dataGridView1.Dock = DockStyle.Fill;
                     //   dataGridView1.Show();
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

            try
            {
                DataGridViewCheckBoxCell dgcc = (DataGridViewCheckBoxCell)this.dataGridView2.Rows[e.RowIndex].Cells[0];
                Boolean flag = Convert.ToBoolean(dgcc.Value);
                dgcc.Value = flag == true ? false : true;
            }
            catch (Exception)
            {
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            using (SqliteDbContext context = new SqliteDbContext())
            {
                richTextBox1.Clear();
                string nickName = this.dataGridView2.Rows[e.RowIndex].Cells[0].Value as string;
                WxFriend friend = context.WxFriends.FirstOrDefault(f => f.NickName == nickName);//仅查找一条数据
                WxAccount account = context.WxAccounts.FirstOrDefault(a => a.Id == 1);

                if (friend == null)//如果没有找到
                {
                    return;
                }
                if (Name == "聊天记录")
                {
                    //（treeView1.selectedNode.Name == “name1”）
                    var messages = context.WxMessages //
                        .Where(m => m.WxId == friend.WxId)
                        .OrderBy(m => m.CreateTime)
                        .ToList();

                    messages.ForEach(m =>
                    {
                        if (m.IsSend == 1)
                        {
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                            richTextBox1.SelectionColor = Color.DimGray;

                            richTextBox1.AppendText("(");
                            richTextBox1.AppendText($"{account.NickName}");
                            richTextBox1.AppendText(")");
                            richTextBox1.AppendText($"{account.WxId}");


                            if (System.IO.File.Exists(@"D:\UDisk\avator" + account.AvatarPath)) //判断文件是否存在
                            {

                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\UDisk\avator" + account.AvatarPath);
                                Bitmap bmp = new Bitmap(img, 25, 22);
                                Clipboard.SetDataObject(bmp);
                                DataFormats.Format dataFormat =
                                DataFormats.GetFormat(DataFormats.Bitmap);
                                if (richTextBox1.CanPaste(dataFormat))
                                    richTextBox1.Paste(dataFormat);
                             }
                            richTextBox1.AppendText("\n");
                            richTextBox1.SelectionColor = Color.Red;
                            richTextBox1.AppendText($"{m.Content}\n");
                            richTextBox1.AppendText($"{m.CreateTime}\n\n\n");

                        }
                        else
                        {
                            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBox1.SelectionColor = Color.DimGray;
                            if (System.IO.File.Exists(@"D:\UDisk\avator" + account.AvatarPath)) //判断文件是否存在
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\UDisk\avator" + friend.AvatarPath);
                                Bitmap bmp = new Bitmap(img, 25, 22);
                                Clipboard.SetDataObject(bmp);
                                DataFormats.Format dataFormat =
                                DataFormats.GetFormat(DataFormats.Bitmap);
                                if (richTextBox1.CanPaste(dataFormat))
                                    richTextBox1.Paste(dataFormat);
                            }
                            richTextBox1.AppendText("(");
                            richTextBox1.AppendText($"{friend.NickName}");
                            richTextBox1.AppendText(")");
                            richTextBox1.AppendText($"{m.WxId}\n");

                            richTextBox1.SelectionColor = Color.Blue;
                            richTextBox1.AppendText($"{m.Content}\n");
                            richTextBox1.AppendText($"{m.CreateTime}\n\n\n");

                        }

                        richTextBox1.SelectionBackColor = Color.WhiteSmoke;
                        // richTextBox1.AppendText($"{m.Content}\n\n");
                    });
                }
                else if (Name == "朋友圈")
                {
                    SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID,nickname,type,content,createTime from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID order by createTime;", Program.m_mainform.g_conn);
                    DataTable dt1 = new DataTable();
                    mAdapter1.Fill(dt1);                

                    int count = dt1.Rows.Count;

                    for (int i = 0; i < count; i++)
                    { 
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.SelectionColor = Color.DimGray;
                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract\com.tencent.mm\MicroMsg\83ed8b966b0f9a2df369b6e6fd832e71\avatar" + friend.AvatarPath)) //判断文件是否存在
                        {
                            System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract\com.tencent.mm\MicroMsg\83ed8b966b0f9a2df369b6e6fd832e71\avatar" + friend.AvatarPath);
                            Bitmap bmp = new Bitmap(img, 25, 22);
                            Clipboard.SetDataObject(bmp);
                            DataFormats.Format dataFormat =
                            DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat))
                                richTextBox1.Paste(dataFormat);
                        }
                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToInt32(dt1.Rows[i]["wxID"])+"\n");

                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"])+"\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"])+"\n\n\n\n");


                        richTextBox1.SelectionBackColor = Color.WhiteSmoke;

                    }
                    
                }
            }

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
           
            richTextBox1.Clear();
            string wxID = this.dataGridView4.Rows[e.RowIndex].Cells[1].Value as string;            

            if (Name == "朋友圈")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXSns.type,content,createTime,comment,supportNum,commentNum,avatarPath from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID and WXSns.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox1.SelectionColor = Color.DimGray;
                    if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]))) //判断文件是否存在
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[0]["avatarPath"]));
                        Bitmap bmp = new Bitmap(img, 25, 22);
                        Clipboard.SetDataObject(bmp);
                        DataFormats.Format dataFormat =
                        DataFormats.GetFormat(DataFormats.Bitmap);
                        if (richTextBox1.CanPaste(dataFormat))
                            richTextBox1.Paste(dataFormat);
                    }
                    richTextBox1.AppendText("(");
                    richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                    richTextBox1.AppendText(")");
                    richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                    richTextBox1.SelectionColor = Color.Blue;
                    richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                    richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");
                    richTextBox1.SelectionBackColor = Color.WhiteSmoke;

                }
                if (count > 500)
                    MessageBox.Show("数据加载完成！");
            }
            else if (Name == "好友聊天")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID not like 'gh_%' and WXMessage.wxID not like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");
                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    else
                    {
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");


                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    richTextBox1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 500)
                    MessageBox.Show("数据加载完成！");

            }
            else if (Name == "群聊")
            {
                string sql = "select WXAddressBook.wxID as WXID,nickname,WXMessage.type,content,isSend,createTime,path,status,avatarPath from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXMessage.wxID like '%@chatroom' and WXMessage.wxID='" + wxID + "' order by createTime;";
                SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter(sql, Program.m_mainform.g_conn);
                DataTable dt1 = new DataTable();
                mAdapter1.Fill(dt1);

                int count = dt1.Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (Convert.ToInt16(dt1.Rows[i]["isSend"]) == 1)
                    {
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    else
                    {
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    richTextBox1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 500)
                    MessageBox.Show("数据加载完成！");
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
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Red;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    else
                    {
                        richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBox1.SelectionColor = Color.DimGray;

                        richTextBox1.AppendText("(");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["nickname"]));
                        richTextBox1.AppendText(")");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["WXID"]) + "\n");

                        if (System.IO.File.Exists(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]))) //判断文件是否存在
                        {
                            System.Drawing.Image img1 = System.Drawing.Image.FromFile(@"D:\手机取证工作路径设置\案件20190707093739\HONORV2020190701094546\AppExtract" + Convert.ToString(dt1.Rows[i]["avatarPath"]));
                            Bitmap bmp1 = new Bitmap(img1, 25, 22);
                            Clipboard.SetDataObject(bmp1);
                            DataFormats.Format dataFormat1 = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (richTextBox1.CanPaste(dataFormat1))
                                richTextBox1.Paste(dataFormat1);
                        }
                        richTextBox1.AppendText("\n");
                        richTextBox1.SelectionColor = Color.Blue;
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["content"]) + "\n");
                        richTextBox1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n\n");

                    }
                    richTextBox1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 500)
                    MessageBox.Show("数据加载完成！");

            }
        }
    }

    public class TreeNodeTypes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int ParentId { get; set; }
    }
}
