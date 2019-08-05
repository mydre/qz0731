using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Db.Base;
using WinAppDemo.Db.Model;
using System.Data.SQLite;
using System.IO;
//using System.Xml;
//using System.Reflection;

namespace WinAppDemo.Forms
{
    public partial class FormGjglZzxx : Form
    {
        public SQLiteConnection g_conn = null;

        public FormGjglZzxx()
        {
            InitializeComponent();

            textBox2.Text = Guid.NewGuid().ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
                   
               Program.m_mainform.AddNewGjalZs();

            using (var context = new CaseContext())
            {
                Case @case = context.Cases.Find(AppConfig.getAppConfig().caseId_selected_working);
                Proof proof = new Proof()
                {
                    Case = @case,
                    ProofName = textBox1.Text,
                    ProofSerialNum = textBox2.Text,
                    PhoneNum = textBox6.Text,
                    phoneNum2 = textBox3.Text,
                    Holder = textBox4.Text,
                    ProofType = comboBox2.Text,
                    note = textBox8.Text,
                };
                @case.Proofs.Add(proof);

                context.SaveChanges();
            }

            //写CaseMsg.db的AppInfo表        
            string ProofId = "";
            string uin = "";
            string wxid = "";

             //获取数据文件的路径
            string Dir_WXmd5 = "";
            DirectoryInfo root = new DirectoryInfo(Program.m_mainform.g_workPath + "\\AppData\\Weixin");
            if (root.Exists)
            {
                foreach (DirectoryInfo d in root.GetDirectories())
                {
                    if (d.Name != "")
                    {
                        Dir_WXmd5 = d.Name;
                        break;
                    }
                }
            }
            string dbPath = "Data Source =" + Program.m_mainform.g_workPath + "\\AppData\\Weixin\\" + Dir_WXmd5 + "\\midwxtrans.db";

            g_conn = new SQLiteConnection(dbPath);
            g_conn.Open();

            
            SQLiteCommand cmdSelect = new SQLiteCommand("select wxID from WXAccount;", g_conn);
            cmdSelect.ExecuteNonQuery();
            SQLiteDataReader reader = cmdSelect.ExecuteReader();
            reader.Read();
            try
            {                
                wxid = "wxid:" + reader.GetString(0);
            }
            catch
            {
            }
            g_conn.Close();


            //提取各微信账户的UIN
            string WXPath = Program.m_mainform.g_workPath + "\\AppExtract\\com.tencent.mm\\shared_prefs\\app_brand_global_sp.xml";
            if (System.IO.File.Exists(WXPath))
            {
                string stemp = File.ReadAllText(WXPath);
                int start = stemp.IndexOf("<string>");
                int end = stemp.IndexOf("</string>");
                if (start >= 0 && end > start)
                    uin ="uin:"+ stemp.Substring(start + 8, end - start - 8);
            }
            else
                MessageBox.Show("文件不存在");
             


            //创建数据库实例，指定文件位置
            dbPath = "Data Source =" + Application.StartupPath + "\\Db\\CaseMsg.db";
            g_conn = new SQLiteConnection(dbPath);
            g_conn.Open();

            string sql = "select ProofId from ProofInfor where ProofName='" + textBox1.Text + "';";
            cmdSelect = null;
            cmdSelect = new SQLiteCommand(sql, g_conn);
            cmdSelect.ExecuteNonQuery();
            reader = cmdSelect.ExecuteReader();
            reader.Read();
            try
            { 
                    ProofId = Convert.ToString(reader.GetInt32(0));
                    cmdSelect = null;
                    sql = "insert into AppInfo values('" + ProofId +"','"+ textBox1.Text + "','Weinxin','"+ uin + "','"+ wxid + "');";
                    cmdSelect = new SQLiteCommand(sql, g_conn);
                    cmdSelect.ExecuteNonQuery();
                              
            }
            catch
            {
            }
            g_conn.Close();


            this.Close();
            AppConfig.AppConfigAddAdvinceClear();
            AppContext.setInstanceNull();

                  

        }

        private void FormGjglZzxx_Load(object sender, EventArgs e)
        {
            textBox1.Text = Program.m_mainform.g_zjName;
        }
    }

  


}
