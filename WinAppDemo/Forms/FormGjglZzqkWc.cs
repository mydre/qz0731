using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDemo.Controls;

namespace WinAppDemo.Forms
{
    public partial class FormGjglZzqkWc : Form
    {

        public FormGjglZzqkWc()
        {
            InitializeComponent();
        }

        private void FormGjglZzqkWc_Load(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FormGjglZzxx form = new FormGjglZzxx();
            form.ShowDialog();

            this.Close();
        }

    }
}
