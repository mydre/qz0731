using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WinAppDemo.Forms
{
    public partial class FormShowBackupFiles : Form
    {

        public FormShowBackupFiles()
        {
            InitializeComponent();
        }

        private void FormShowBackupFiles_Load(object sender, EventArgs e)
        {
            string dbPath = "Data Source ="+Application.StartupPath+"\\m.db";// + Program.m_mainform.g_workPath+"\\AppData\\Weixin\\ca9529dc14475dbcc7e8553e77ad7d0b" + "\\midwxtrans.db";

            //从安装路径去存放备份文件名的数据库
            Program.m_mainform.g_conn = new SQLiteConnection(dbPath);
            Program.m_mainform.g_conn.Open();
            dataGridView1.DataSource = null;
            SQLiteDataAdapter mAdapter = new SQLiteDataAdapter("select id as 序号,name as 文件名 from file_name;", Program.m_mainform.g_conn);
             DataTable dt = new DataTable();
            mAdapter.Fill(dt);
            dt.Columns.Add(new DataColumn("操作", typeof(string)));
            DataRow dr;
            for(int i=0;i<dt.Rows.Count;i++)
            {
                dr = dt.NewRow();
                dr["操作"] = i;
                dt.Rows.Add(dr);
            }


            dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.m_mainform.backupFileName = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

    }
}
