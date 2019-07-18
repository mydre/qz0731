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


namespace WinAppDemo.Forms
{
    public partial class FormGjglZzxxEdit : Form
    {
        public Proof Proof { get; set; } = new Proof();

        public FormGjglZzxxEdit()
        {
            InitializeComponent();
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

                };
                @case.Proofs.Add(proof);

                context.SaveChanges();
            }

            this.Close();
            AppConfig.AppConfigAddAdvinceClear();
            AppContext.setInstanceNull();
        }

        private void FormGjglZzxxEdit_Load(object sender, EventArgs e)
        {
            AppConfig ac = AppConfig.getAppConfig();
            using (CaseContext caseContext = new CaseContext())
            {
                Proof proofInfo = caseContext.Proofs.FirstOrDefault(p => p.ProofId == ac.proofId_selected);
                if (proofInfo == null)
                {
                    MessageBox.Show("该证据不存在，无法继续进行编辑！");
                    this.Close();
                    return;
                }
                this.Proof = proofInfo;
                //对要编辑的页面进行初始化,这是页面上的数据是从数据库里面取出来的
                textBox1.Text = this.Proof.ProofName;
                textBox2.Text = this.Proof.ProofSerialNum;
                textBox6.Text = this.Proof.PhoneNum;
                textBox3.Text = this.Proof.phoneNum2;
                textBox4.Text = this.Proof.Holder;
                comboBox2.Text = this.Proof.ProofType;
                textBox8.Text = this.Proof.note;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Proof.ProofName = textBox1.Text;
            this.Proof.ProofSerialNum = textBox2.Text;
            this.Proof.PhoneNum = textBox6.Text;
            this.Proof.phoneNum2 = textBox3.Text;
            this.Proof.Holder = textBox4.Text;
            this.Proof.ProofType = comboBox2.Text;
            this.Proof.note = textBox8.Text;
            //向数据库中写回更改之后的数据
            AppConfig ac = AppConfig.getAppConfig();
            using (CaseContext caseContext = new CaseContext())
            {
                var proofInfo = this.Proof;
                var entry = caseContext.Entry(proofInfo);
                caseContext.Set<Proof>().Attach(entry.Entity);
                entry.State = System.Data.Entity.EntityState.Modified;
                caseContext.SaveChanges();
            }
            Program.m_mainform.AddNewGjalAj();
            MessageBox.Show("编辑证据成功！");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
