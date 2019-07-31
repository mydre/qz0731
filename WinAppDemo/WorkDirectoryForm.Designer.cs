namespace WinAppDemo
{
    partial class WorkDirectoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WorDIrectoryTexbox = new System.Windows.Forms.TextBox();
            this.WorkDirectoryLabel = new System.Windows.Forms.Label();
            this.SetBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WorDIrectoryTexbox
            // 
            this.WorDIrectoryTexbox.Location = new System.Drawing.Point(171, 82);
            this.WorDIrectoryTexbox.Name = "WorDIrectoryTexbox";
            this.WorDIrectoryTexbox.Size = new System.Drawing.Size(314, 21);
            this.WorDIrectoryTexbox.TabIndex = 5;
            this.WorDIrectoryTexbox.Text = "D:\\\\手机取证工作路径设置";
            // 
            // WorkDirectoryLabel
            // 
            this.WorkDirectoryLabel.AutoSize = true;
            this.WorkDirectoryLabel.Location = new System.Drawing.Point(100, 85);
            this.WorkDirectoryLabel.Name = "WorkDirectoryLabel";
            this.WorkDirectoryLabel.Size = new System.Drawing.Size(65, 12);
            this.WorkDirectoryLabel.TabIndex = 4;
            this.WorkDirectoryLabel.Text = "工作路径：";
            // 
            // SetBut
            // 
            this.SetBut.Location = new System.Drawing.Point(511, 81);
            this.SetBut.Name = "SetBut";
            this.SetBut.Size = new System.Drawing.Size(61, 23);
            this.SetBut.TabIndex = 3;
            this.SetBut.Text = "选择";
            this.SetBut.UseVisualStyleBackColor = true;
            this.SetBut.Click += new System.EventHandler(this.SetBut_Click);
            // 
            // WorkDirectoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 232);
            this.Controls.Add(this.WorDIrectoryTexbox);
            this.Controls.Add(this.WorkDirectoryLabel);
            this.Controls.Add(this.SetBut);
            this.Name = "WorkDirectoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WorkDirectoryForm";
            this.Load += new System.EventHandler(this.WorkDirectoryForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label WorkDirectoryLabel;
        private System.Windows.Forms.Button SetBut;
        public System.Windows.Forms.TextBox WorDIrectoryTexbox;
    }
}