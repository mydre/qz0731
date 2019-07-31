using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Db.Model;
using WinAppDemo.Db.Base;
using WinAppDemo.Controls;
using System.Threading;
namespace WinAppDemo.Forms
{
    public partial class FormGjglEditAj : Form
    {
        public Case Case { get; set; } = new Case();

        public FormGjglEditAj()
        {
            InitializeComponent();
        }

        public void BtnOk_Click(object sender, EventArgs e)//当点击确定的时候，页面上的数据，要保存在caseInfo中，然后将caseInfo写回到数据库中
        {
            Console.WriteLine("编辑案件：");
            this.Case.Path = Program.m_mainform.g_workPath;
            this.Case.CaseName = textBox1.Text;
            this.Case.CaseSerialNum = textBox2.Text;
            this.Case.CaseType = comboBox1.Text;
            this.Case.Collecter = textBox3.Text;
            this.Case.CollecterNum = textBox4.Text;
            this.Case.CollecterDepartMent = comboBox2.Text;
            this.Case.InspectionPersonName = textBox5.Text;
            this.Case.InspectionPersonDepartMent = textBox6.Text;
            this.Case.OrganizationCode = textBox7.Text;
            this.Case.Note = textBox8.Text;
            //向数据库中写回更改之后的数据
            AppConfig ac = AppConfig.getAppConfig();
            using (CaseContext caseContext = new CaseContext())
            {
                var caseInfo = this.Case;
                var entry = caseContext.Entry(caseInfo);
                caseContext.Set<Case>().Attach(entry.Entity);
                entry.State = System.Data.Entity.EntityState.Modified;
                caseContext.SaveChanges();
            }
            Program.m_mainform.AddNewGjalAj();

            MessageBox.Show("编辑案件成功！");

            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Console.WriteLine("点击取消按键");
            //MessageBox.Show("取消");
        }

        private void FormGjglEdit_Load(object sender, EventArgs e)//加载编辑页面的时候执行这个函数
        {
            AppConfig ac = AppConfig.getAppConfig();
            using (CaseContext caseContext = new CaseContext())
            {
                Case caseInfo = caseContext.Cases.FirstOrDefault(c => c.CaseId == ac.caseId_Edited);
                if (caseInfo == null)
                {
                    MessageBox.Show("该案件不存在，无法继续进行编辑！");
                    this.Close();
                    return;
                }
                this.Case = caseInfo;
                //对要编辑的页面进行初始化,这是页面上的数据是从数据库里面取出来的
                textBox1.Text = this.Case.CaseName;
                textBox2.Text = this.Case.CaseSerialNum;
                comboBox1.Text = this.Case.CaseType;
                textBox3.Text = this.Case.Collecter;
                textBox4.Text = this.Case.CollecterNum;
                comboBox2.Text = this.Case.CollecterDepartMent;
                textBox5.Text = this.Case.InspectionPersonName;
                textBox6.Text = this.Case.InspectionPersonDepartMent;
                textBox7.Text = this.Case.OrganizationCode;
                textBox8.Text = this.Case.Note;
            }
        }
    }
}
