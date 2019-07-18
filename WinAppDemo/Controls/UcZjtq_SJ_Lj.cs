using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinAppDemo.Controls
{
    public partial class UcZjtq_SJ_Lj : UserControl
    {
        public UcZjtq_SJ_Lj()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if(AppConfig.getAppConfig().caseId_selected_working == -1)
            {
                MessageBox.Show("请首先选择案件,然后单击添加案件的按钮！");
                AppConfig.getAppConfig().caseId_selected_row = -1;
                Program.m_mainform.AddNewGjalAj();
                return;
            }

            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();

            AppContext.GetInstance().m_ucZjtq_sj_ljcg.Dock = DockStyle.Fill;
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Clear();
            AppContext.GetInstance().m_ucZjtq_sj.Controls.Add(AppContext.GetInstance().m_ucZjtq_sj_ljcg);

            ////获取手机型号由于创建证据目录
            //Process PreProcess = new Process();
            //PreProcess.StartInfo.Arguments = "D: 0";  
            //PreProcess.StartInfo.FileName = Application.StartupPath + "\\socketPbi.exe";
            //PreProcess.StartInfo.Verb = "runas";
            //PreProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //PreProcess.Start();
            //PreProcess.WaitForExit();


            string filename=Application.StartupPath + "\\phoneModel.txt";
            //判断目标文件是否存在
            bool flag = File.Exists(filename);
            if (flag)
            {
                string Str = File.ReadAllText(filename, Encoding.Default);
                string[] sArray = Str.Split('/');

                Program.m_mainform.g_zjName = sArray[1].Replace("-", "") + DateTime.Now.ToString("yyyyMMddHHmmss");
                Program.m_mainform.g_workPath += "\\" + Program.m_mainform.g_ajName + "\\" + Program.m_mainform.g_zjName;

                // File.Delete(filename);

                //创建案件目录结构
                Directory.CreateDirectory(Program.m_mainform.g_workPath);
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppBackup");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppData");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\AppExtract");
                Directory.CreateDirectory(Program.m_mainform.g_workPath + "\\PhoneData");
            } 
        }
    }
}
