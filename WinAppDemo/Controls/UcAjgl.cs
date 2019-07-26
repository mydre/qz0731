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
using System.Threading;
using System.Collections;
namespace WinAppDemo.Controls
{
    public partial class UcAjgl : UserControl
    {
        public UcAjgl()
        {
            InitializeComponent();
        }

        private void UcAjgl_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            //新建案件
            FormGjglNewAj form = new FormGjglNewAj();
            string s = form.ShowDialog().ToString();
            if (s == "Cancel") return;
            Case @case = form.Case;
            using (var context = new CaseContext())
            {
                context.Cases.Add(@case);
                context.SaveChanges();
                AppContext.CaseID = @case.CaseId;
            }
            Program.m_mainform.AddNewGjalAj();
            MessageBox.Show("案件添加成功！");
        }

        private void UcAjgl_Load(object sender, EventArgs e)
        {
            using (var context = new CaseContext())
            {
                var cases = context.Cases.AsNoTracking().ToList();
                this.dataGridView1.DataSource = cases;
                //为了让案件的编号从1开始
                var dgw = this.dataGridView1;
                int len = dgw.Rows.Count;
                AppConfig ac = AppConfig.getAppConfig();
                ac.aList.Clear();
                ac.loadDone = 0;
                for (int i = 0; i < len; i++)
                {
                    var cell_caseId = dgw.Rows[i].Cells[1];
                    ac.aList.Add(cell_caseId.Value);
                    cell_caseId.Value = i + 1;
                    //Console.WriteLine(ac.aList[i]);
                }
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//为了完成复选框的选中和取消选中
        {
            try
            {
                //AppConfig.getAppConfig().caseId_selected_row = (int)this.dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                DataGridViewCheckBoxCell dgcc = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[0];
                Boolean flag = Convert.ToBoolean(dgcc.Value);
                dgcc.Value = flag == true ? false : true;
            }
            catch (Exception)
            {
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)//当下一次点击和行和上一次点击的行不一致时，会触发该事件(如果多次点击同一行，则不触发)
        {
            AppConfig ac = AppConfig.getAppConfig();
            if (dataGridView1.SelectedRows.Count == 0)
            {
                //Console.WriteLine("即将被返回");
                ac.loadDone = 1;
                return;
            }
            if(ac.loadDone == 1)
            {
                //Console.WriteLine("加载完成了！");
                var row = this.dataGridView1.SelectedRows[0];
                //Console.WriteLine("下标：" + row.Index);
                int indx = row.Index;
                Program.m_mainform.g_ajName = (string)dataGridView1.SelectedRows[0].Cells[2].Value;
                int caseId = (int)ac.aList[indx];
                ac.caseId_selected_row = caseId;
                using (var context = new CaseContext())
                {
                    var proofs = context.Proofs.AsNoTracking().Where(p => p.CaseID == caseId).ToList();
                    dataGridView2.DataSource = proofs;
                    var dgw = this.dataGridView2;
                    int len = dgw.Rows.Count;
                    ac.aEvidenceList.Clear();//清除状态
                    for (int i = 0; i < len; i++)
                    {
                        var cell_EvidenceId = dgw.Rows[i].Cells[1];
                        ac.aEvidenceList.Add(cell_EvidenceId.Value);//首先向aEvidenceList中进行添加
                        cell_EvidenceId.Value = i + 1;
                        //Console.WriteLine(ac.aEvidenceList[i]);
                    }
                }
                //为了完成案件的预览展示
                label5.Text = Convert.ToString(dataGridView1.Rows[row.Index].Cells[2].Value);
                label7.Text = Convert.ToString(dataGridView1.Rows[row.Index].Cells[4].Value);
                CheckTextLabel.Text = Convert.ToString(dataGridView1.Rows[row.Index].Cells[6].Value);
                EquipTextLabel.Text = Convert.ToString(dataGridView1.Rows[row.Index].Cells[9].Value);
                CheckTimeTextLabel.Text = Convert.ToString(dataGridView1.Rows[row.Index].Cells[11].Value);
            }
        }

        private void button13_Click(object sender, EventArgs e)//对案件添加证据
        {
            AppConfig ac = AppConfig.getAppConfig();
            int indx = this.dataGridView1.SelectedRows[0].Index;

            if (ac.caseId_selected_row == -1)
            {
                MessageBox.Show("请选择案件", "提示");
                return;
            }
            else if (ac.already_working == false)
            {
                //DialogResult dr = MessageBox.Show("是否要为编号为" + ac.caseId_selected_row + "的案件添加证据", "提示", MessageBoxButtons.OKCancel);
                DialogResult dr = MessageBox.Show("是否要为编号为" + (indx + 1) + "的案件添加证据", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    ac.caseId_selected_working = ac.caseId_selected_row;
                    ac.already_working = true;
                }
                else if (dr == DialogResult.Cancel)
                {
                    //ac.caseId_selected_row = -1;
                    return;
                }
            }
            else
            {
                if (ac.caseId_selected_row != ac.caseId_selected_working)
                {
                    DialogResult dr = MessageBox.Show("你之前对编号为" + ac.caseId_selected_working + "的案件添加证据(未保存)，是否确定要为编号为" + ac.caseId_selected_row + "的案件添加证据？", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        ac.caseId_selected_working = ac.caseId_selected_row;
                        AppContext.setInstanceNull();
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        //ac.caseId_selected_row = -1;
                        return;
                    }
                }
            }
            ac.caseId_selected_row = -1;

            Program.m_mainform.AddNewGjalZj();

            //UcZjtq uc = new UcZjtq();
            //uc.Dock = DockStyle.Fill;
            //var p = this.Parent.Parent.Controls["WinContent"].Controls;
            //this.Parent.Parent.Controls["WinContent"].Controls.Clear();
            //p.Add(uc);
        }

        private void allCheckedChanged(object sender, EventArgs e)//为了完成全选和取消全选
        {
            CheckBox c = sender as CheckBox;
            bool allCheck = false;
            if (c.Checked == true)
            {
                allCheck = true;
            }
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell dgcc = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                Boolean flag = Convert.ToBoolean(allCheck);
                dgcc.Value = flag;
            }
        }

        private void DataGrid2_CellClick(object sender, DataGridViewCellEventArgs e)//证据列表前面复选框的选中与取消选中
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
        }

        private void button3_Click(object sender, EventArgs e)//对案件进行编辑
        {
            AppConfig ac = AppConfig.getAppConfig();
            if (ac.caseId_selected_row == -1)
            {
                MessageBox.Show("请选择要编辑的案件", "提示");
                return;
            }
            else
            {
                var row = this.dataGridView1.SelectedRows[0];
                int indx = row.Index;
                DialogResult dr = MessageBox.Show("是否对编号为" + (indx + 1) + "的案件进行编辑", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    ac.caseId_Edited = ac.caseId_selected_row;
                    //ac.caseId_selected_row = -1;
                }
                else if (dr == DialogResult.Cancel)
                {
                    //ac.caseId_selected_row = -1;
                    return;
                }
            }
            FormGjglEditAj form = new FormGjglEditAj();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//删除案件(每个被删除案件的前面的复选框要打勾)
        {
            ArrayList selectedCaseIds = new ArrayList();
            int rc = dataGridView1.RowCount;

            string st = "";
            int once = 0;
            for (int i = 0; i < rc; i++)
            {
                bool selected = Convert.ToBoolean(this.dataGridView1.Rows[i].Cells[0].Value);
                if (selected)
                {
                    if(once == 0)
                    {
                        st = string.Format("是否要删除编号为：{0}", i + 1);
                        once = 1;
                    }else
                    {
                        st += string.Format("、{0}", i + 1);
                    }
                    selectedCaseIds.Add(AppConfig.getAppConfig().aList[i]);
                }
            }
            st += " 的案件?";
            int deleteAmount = selectedCaseIds.Count;
            if (deleteAmount == 0)
            {
                MessageBox.Show("请在你想要删除的案件前面打勾！");
            }
            else
            {   
                DialogResult RSS = MessageBox.Show(this, st, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (RSS == DialogResult.Yes)
                {//开始访问数据库
                    using (CaseContext caseContext = new CaseContext())
                    {
                        for (int i = 0; i < deleteAmount; i++)
                        {
                            int caseId = Convert.ToInt32(selectedCaseIds[i]);
                            var Caseinfo = caseContext.Cases.FirstOrDefault(c => c.CaseId == caseId);
                            if (Caseinfo != null)
                            {
                                caseContext.Cases.Remove(Caseinfo);
                                var proofs = caseContext.Proofs.Where(p => p.CaseID == caseId);
                                caseContext.Proofs.RemoveRange(proofs);//删除案件的同时，级联删除该案件对应的所有证据
                            }
                        }
                        caseContext.SaveChanges();
                    }
                    Program.m_mainform.AddNewGjalAj();
                    MessageBox.Show("删除案件成功！");
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)//对某一个案件的证据进行删除
        {
            ArrayList selectedProofIds = new ArrayList();
            int rc = dataGridView2.RowCount;
            AppConfig ac = AppConfig.getAppConfig();
            int once = 0;
            string st = "";
            for (int i = 0; i < rc; i++)
            {
                bool selected = Convert.ToBoolean(this.dataGridView2.Rows[i].Cells[0].Value);
                if (selected)
                {
                    if(once == 0)
                    {
                        st = string.Format("是否要删除编号为：{0}", i+1);
                        once = 1;
                    }else
                    {
                        st += string.Format("、{0}", i + 1);
                    }
                    //selectedProofIds.Add(this.dataGridView2.Rows[i].Cells[1].Value);//添加真正的证据的编号
                    selectedProofIds.Add(ac.aEvidenceList[i]);//添加真正的证据的编号
                }
            }
            st += " 的证据?";
            int deleteAmount = selectedProofIds.Count;
            if (deleteAmount == 0)
            {
                MessageBox.Show("请在你想要删除的证据前面打勾！");
            }
            else
            {
                DialogResult RSS = MessageBox.Show(this, st, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (RSS == DialogResult.Yes)
                {//开始访问数据库
                    using (CaseContext caseContext = new CaseContext())
                    {
                        for (int i = 0; i < deleteAmount; i++)
                        {
                            int proofId = Convert.ToInt32(selectedProofIds[i]);
                            //Console.WriteLine(proofId+"--------------------------");
                            var ProofInfo = caseContext.Proofs.FirstOrDefault(p => p.ProofId == proofId);
                            if (ProofInfo != null)
                            {
                                caseContext.Proofs.Remove(ProofInfo);
                            }
                        }
                        caseContext.SaveChanges();
                    }
                    Program.m_mainform.AddNewGjalAj();
                    MessageBox.Show("删除证据成功！");
                }
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)//证据列表
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                return;
            }
            var row = this.dataGridView2.SelectedRows[0];
            int proofId = (int)row.Cells[1].Value;
            AppConfig ac = AppConfig.getAppConfig();
            ac.proofId_selected = proofId;
        }

        private void button12_Click(object sender, EventArgs e)//对某一个案件的证据进行编辑
        {
            AppConfig ac = AppConfig.getAppConfig();
            if (ac.proofId_selected == -1)
            {
                MessageBox.Show("请选择要编辑的证据", "提示");
                return;
            }
            else
            {
                int indx = this.dataGridView2.SelectedRows[0].Index;
                DialogResult dr = MessageBox.Show("是否对编号为" + (indx + 1) + "的证据进行编辑", "提示", MessageBoxButtons.OKCancel);
                ac.proofId_selected = Convert.ToInt32(ac.aEvidenceList[indx]);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            FormGjglZzxxEdit form = new FormGjglZzxxEdit();
            form.ShowDialog();
        }
    }
}
