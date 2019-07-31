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
using WinAppDemo.tools;

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
            //SQLiteDataAdapter mAdapter = new SQLiteDataAdapter("select name as 名称,seq as 数量 from sqlite_sequence;", Program.m_mainform.g_conn);//select * from WXAccount

            //DataTable dt = new DataTable();
            //mAdapter.Fill(dt);

            ////绑定数据到DataGridView
            //dataGridView3.DataSource = dt;
            //dataGridView3.Show();
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
            dataGridView3.Hide();

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

            long totalNum = Program.m_mainform.g_Num_WXAccount;        //数据总条数      
            totalNum += Program.m_mainform.g_Num_WXChatroom;
            totalNum += Program.m_mainform.g_Num_WXChatroomList;
            totalNum += Program.m_mainform.g_Num_WXMessage;
            totalNum += Program.m_mainform.g_Num_WXNewFriend;
            totalNum += Program.m_mainform.g_Num_WXSns;
            totalNum += Program.m_mainform.g_Num_WXAddressBook;

            totalNum += Program.m_mainform.g_Num_phoneinfo;
            totalNum += Program.m_mainform.g_Num_Sms;
            totalNum += Program.m_mainform.g_Num_Calls;
            totalNum += Program.m_mainform.g_Num_Contacts;

            var topNode = new TreeNode();
            topNode.Name = "0";
            topNode.Text = Program.m_mainform.g_ajName + @"（总共"+Convert.ToString(totalNum)+"）"; // @"案件（已删除\未删除\总共）";
            treeView1.Nodes.Add(topNode);
            Bind(topNode, Types, 0);

            treeView1.ExpandAll();

            //添加根节点
            TreeNode nodeAJ = new TreeNode();
            nodeAJ.Text = Program.m_mainform.g_ajName + @"（总共" + Convert.ToString(totalNum) + "）";
            treeView2.Nodes.Add(nodeAJ);

            TreeNode nodeZJ = new TreeNode();
            nodeZJ.Text = Program.m_mainform.g_zjName + @"（总共" + Convert.ToString(totalNum) + "）";
            nodeAJ.Nodes.Add(nodeZJ);           //添加证据结点

            TreeNode nodeBase = new TreeNode("基础信息"+ @"（" + Convert.ToString(Program.m_mainform.g_Num_phoneinfo+ Program.m_mainform.g_Num_Sms+ Program.m_mainform.g_Num_Calls+ Program.m_mainform.g_Num_Contacts) +  "）");
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
                TreeNode nodeFile = new TreeNode("文件信息(0)");
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
                        TreeNode node1 = new TreeNode(WDID_Nickname+ "（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount + Program.m_mainform.g_Num_WXChatroom + Program.m_mainform.g_Num_WXChatroomList + Program.m_mainform.g_Num_WXMessage + Program.m_mainform.g_Num_WXAddressBook + Program.m_mainform.g_Num_WXSns) + "）");
                        node.Nodes.Add(node1);
                        TreeNode node11 = new TreeNode("账号信息" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAccount) + "）");
                        node1.Nodes.Add(node11);
                        TreeNode node12 = new TreeNode("通讯录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXAddressBook) + "）");
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


                        TreeNode node13 = new TreeNode("聊天记录" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXMessage) + "）");
                        node1.Nodes.Add(node13);
                        TreeNode node131 = new TreeNode("好友聊天");
                        node13.Nodes.Add(node131);
                        TreeNode node132 = new TreeNode("群聊");
                        node13.Nodes.Add(node132);
                        TreeNode node133 = new TreeNode("公众号讯息");
                        node13.Nodes.Add(node133);

                        TreeNode node14 = new TreeNode("朋友圈" + "（" + Convert.ToString(Program.m_mainform.g_Num_WXSns) + "）");
                        node1.Nodes.Add(node14);
                        //TreeNode node141 = new TreeNode("本人的朋友圈");
                        //node14.Nodes.Add(node141);
                        //TreeNode node142 = new TreeNode("好友的朋友圈");
                        //node14.Nodes.Add(node142);
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
                        this.richTextBoxEx1.Clear();
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
                        this.richTextBoxEx1.Clear();
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
                        this.richTextBoxEx1.Clear();
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
            int pos=nodeName.IndexOf("（");
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
                        dataGridView3.DataSource = null;
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
                        dataGridView3.DataSource = null;
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
                        dataGridView4.Size = dataGridView2.Size;
                        dataGridView4.Show();
                        dataGridView4.DataSource = null;
                        richTextBoxEx1.Clear();

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称, WXMessage.wxID as 微信账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'wxid_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 群聊账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like '%@chatroom' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXMessage.wxID as 公众号账号, count(WXMessage.wxID) as 消息数量 from WXMessage,WXAddressBook where WXMessage.wxID=WXAddressBook.wxID and WXAddressBook.wxID like 'gh_%' group by WXAddressBook.wxID;", Program.m_mainform.g_conn);
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

                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select nickname as 昵称,WXSns.wxID as 微信账号, count(WXSns.wxID) as 朋友圈条数 from WXSns,WXAddressBook where WXSns.wxID=WXAddressBook.wxID group by WXAddressBook.wxID;", Program.m_mainform.g_conn);   //and WXAddressBook.type=3
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView4.DataSource = dt1;
                        dataGridView4.Columns[1].Visible = false;

                        break;
                    }
                case "新朋友":
                    {                     
                        SQLiteDataAdapter mAdapter1 = new SQLiteDataAdapter("select wxID as 微信账号,nickname as 昵称,sex as 性别,sign as 个性签名, district as 地区,remark as 备注,description as 描述,contentVerify as 好友请求内容,lastModifiedTime as 最后更新时间  from WXNewFriend;", Program.m_mainform.g_conn);
                        DataTable dt1 = new DataTable();
                        mAdapter1.Fill(dt1);
                        dataGridView3.DataSource = dt1;

                        panel1.Hide();
                        panel3.Hide();
                        panel2.Show();
                        panel2.Dock = DockStyle.Fill;
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
                richTextBoxEx1.Clear();
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
                            richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Right;
                            richTextBoxEx1.SelectionColor = Color.DimGray;

                            richTextBoxEx1.AppendText("(");
                            richTextBoxEx1.AppendText($"{account.NickName}");
                            richTextBoxEx1.AppendText(")");
                            richTextBoxEx1.AppendText($"{account.WxId}");


                            if (System.IO.File.Exists(@"D:\UDisk\avator" + account.AvatarPath)) //判断文件是否存在
                            {

                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\UDisk\avator" + account.AvatarPath);
                                Bitmap bmp = new Bitmap(img, 25, 22);
                                Clipboard.SetDataObject(bmp);
                                DataFormats.Format dataFormat =
                                DataFormats.GetFormat(DataFormats.Bitmap);
                                if (richTextBoxEx1.CanPaste(dataFormat))
                                    richTextBoxEx1.Paste(dataFormat);
                             }
                            richTextBoxEx1.AppendText("\n");
                            richTextBoxEx1.SelectionColor = Color.Red;
                            //richTextBoxEx1.AppendText($"{m.Content}\n");
                            //richTextBoxEx1.AppendText($"{m.CreateTime}\n\n\n");

                        }
                        else
                        {
                            richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                            richTextBoxEx1.SelectionColor = Color.DimGray;
                            if (System.IO.File.Exists(@"D:\UDisk\avator" + account.AvatarPath)) //判断文件是否存在
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\UDisk\avator" + friend.AvatarPath);
                                Bitmap bmp = new Bitmap(img, 25, 22);
                                Clipboard.SetDataObject(bmp);
                                DataFormats.Format dataFormat =
                                DataFormats.GetFormat(DataFormats.Bitmap);
                                if (richTextBoxEx1.CanPaste(dataFormat))
                                    richTextBoxEx1.Paste(dataFormat);
                            }
                            richTextBoxEx1.AppendText("(");
                            richTextBoxEx1.AppendText($"{friend.NickName}");
                            richTextBoxEx1.AppendText(")");
                            richTextBoxEx1.AppendText($"{m.WxId}\n");

                            richTextBoxEx1.SelectionColor = Color.Blue;


                        }
                        //在sqlite的message表里面，有的对话的path字段含有空格，需要使用Replace进行去除
                        if (string.IsNullOrEmpty(m.Path.Replace(" ", "")))//message是单纯的对话消息 或者 是含有url链接的消息
                        {
                            SelectUrl.print_msg_or_url(m.Content, richTextBoxEx1);//该方法在打印message内容的同时能够识别url
                            //Console.WriteLine("是对话或者链接："+m.Content);
                        }
                        else//说明message是文件类型的消息，这时候应当能够点击链接并打开文件
                        {
                            //Console.WriteLine("文件类型：" + m.Content);
                            SelectUrl.print_file(m.Path, richTextBoxEx1);
                        }
                        richTextBoxEx1.AppendText($"{m.CreateTime}\n\n\n");
                        richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;


                    });
                }
                else if (Name == "朋友圈")
                {
                    var sns = context.WxSns
                          .Where(s => s.WxId == friend.WxId)
                          .OrderBy(s => s.CreateTime)
                          .ToList();

                    sns.ForEach(s =>
                    {
                        richTextBoxEx1.SelectionAlignment = HorizontalAlignment.Left;
                        richTextBoxEx1.SelectionColor = Color.DimGray;
                        System.Drawing.Image img = System.Drawing.Image.FromFile(@"D:\UDisk\avator" + friend.AvatarPath);
                        Bitmap bmp = new Bitmap(img, 25, 22);
                        Clipboard.SetDataObject(bmp);
                        DataFormats.Format dataFormat =
                        DataFormats.GetFormat(DataFormats.Bitmap);
                        if (richTextBoxEx1.CanPaste(dataFormat))
                            richTextBoxEx1.Paste(dataFormat);
                        richTextBoxEx1.AppendText("(");
                        richTextBoxEx1.AppendText($"{friend.NickName}");
                        richTextBoxEx1.AppendText(")");
                        richTextBoxEx1.AppendText($"{s.WxId}\n");

                        richTextBoxEx1.SelectionColor = Color.Blue;
                        richTextBoxEx1.AppendText($"{s.Content}\n");
                        richTextBoxEx1.AppendText($"{s.CreateTime}\n\n\n\n");


                        richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;

                    });
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
           
            richTextBoxEx1.Clear();
            string wxID = this.dataGridView4.Rows[e.RowIndex].Cells[1].Value as string;            

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
                if (count > 500)
                    MessageBox.Show("数据加载完成！");
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

                    if (type == 1 && type == 10000 && type == 50 && type == 47 && type == 10002 && type == 64 && type == 50 && type == 570425393)
                        richTextBoxEx1.AppendText(content + "\n");
                    else if (type == 436207665)   //红包
                    {
                        int status = Convert.ToInt32(dt1.Rows[i]["status"]);
                        if (status == 3)
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
                        SelectUrl.print_MsgOrUrl(content, type, path, richTextBoxEx1);

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }


                ////在sqlite的message表里面，有的对话的path字段含有空格，需要使用Replace进行去除
                //string ph = Convert.ToString(dt1.Rows[i]["path"]);

                //if (string.IsNullOrEmpty(ph.Replace(" ", "")))//message是单纯的对话消息 或者 是含有url链接的消息
                //{
                //    SelectUrl.print_msg_or_url(Convert.ToString(dt1.Rows[i]["content"]), richTextBoxEx1);//该方法在打印message内容的同时能够识别url
                //                                                                                         //Console.WriteLine("是对话或者链接："+m.Content);
                //}
                //else//说明message是文件类型的消息，这时候应当能够点击链接并打开文件
                //{
                //    SelectUrl.print_file(ph, richTextBoxEx1);
                //}

               
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

                    richTextBoxEx1.AppendText(Convert.ToString(dt1.Rows[i]["createTime"]) + "\n\n\n");
                    richTextBoxEx1.SelectionBackColor = Color.WhiteSmoke;
                }
                if (count > 500)
                    MessageBox.Show("数据加载完成！");

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

    public class TreeNodeTypes
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int ParentId { get; set; }
    }
}
