using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDemo
{
    public partial class WorkDirectoryForm : Form
    {
        public WorkDirectoryForm()
        {
            InitializeComponent();
        }
        private void WorkDirectoryForm_Load(object sender, EventArgs e)
        {
          //  InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void SetBut_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择工作目录";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                //MessageBox.Show(dialog.SelectedPath);
                AppConfig.getAppConfig().working_directory = dialog.SelectedPath;//保存工作路径
                Program.m_mainform.g_workPath = dialog.SelectedPath;
            }
            //setDirectory.BackgroundImage = Properties.Resources.set;
            WorDIrectoryTexbox.Text = Program.m_mainform.g_workPath;
            MessageBox.Show(this, "工作路径设置完成", "提示");
            return;
        }

      
    }
}
