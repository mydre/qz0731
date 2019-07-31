namespace WinAppDemo
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WinContent = new System.Windows.Forms.Panel();
            this.pHeader = new System.Windows.Forms.Panel();
            this.btnTools = new System.Windows.Forms.Panel();
            this.btnZjzs = new System.Windows.Forms.Panel();
            this.btnZjtq = new System.Windows.Forms.Panel();
            this.btnAjgl = new System.Windows.Forms.Panel();
            this.setDirectory = new System.Windows.Forms.Panel();
            this.pHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // WinContent
            // 
            this.WinContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WinContent.Location = new System.Drawing.Point(-1, 60);
            this.WinContent.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.WinContent.Name = "WinContent";
            this.WinContent.Size = new System.Drawing.Size(1285, 702);
            this.WinContent.TabIndex = 1;
            // 
            // pHeader
            // 
            this.pHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pHeader.BackgroundImage")));
            this.pHeader.Controls.Add(this.btnTools);
            this.pHeader.Controls.Add(this.btnZjzs);
            this.pHeader.Controls.Add(this.btnZjtq);
            this.pHeader.Controls.Add(this.btnAjgl);
            this.pHeader.Controls.Add(this.setDirectory);
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(1284, 60);
            this.pHeader.TabIndex = 0;
            // 
            // btnTools
            // 
            this.btnTools.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTools.BackgroundImage")));
            this.btnTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTools.Location = new System.Drawing.Point(420, 0);
            this.btnTools.Margin = new System.Windows.Forms.Padding(0);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(140, 60);
            this.btnTools.TabIndex = 3;
            this.btnTools.Click += new System.EventHandler(this.BtnTools_Click);
            // 
            // btnZjzs
            // 
            this.btnZjzs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZjzs.BackgroundImage")));
            this.btnZjzs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZjzs.Location = new System.Drawing.Point(280, 0);
            this.btnZjzs.Margin = new System.Windows.Forms.Padding(0);
            this.btnZjzs.Name = "btnZjzs";
            this.btnZjzs.Size = new System.Drawing.Size(140, 60);
            this.btnZjzs.TabIndex = 2;
            this.btnZjzs.Click += new System.EventHandler(this.BtnZjzs_Click);
            // 
            // btnZjtq
            // 
            this.btnZjtq.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZjtq.BackgroundImage")));
            this.btnZjtq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZjtq.Location = new System.Drawing.Point(140, 0);
            this.btnZjtq.Margin = new System.Windows.Forms.Padding(0);
            this.btnZjtq.Name = "btnZjtq";
            this.btnZjtq.Size = new System.Drawing.Size(140, 60);
            this.btnZjtq.TabIndex = 1;
            this.btnZjtq.Click += new System.EventHandler(this.BtnZjtq_Click);
            // 
            // btnAjgl
            // 
            this.btnAjgl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAjgl.BackgroundImage")));
            this.btnAjgl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAjgl.Location = new System.Drawing.Point(0, 0);
            this.btnAjgl.Margin = new System.Windows.Forms.Padding(0);
            this.btnAjgl.Name = "btnAjgl";
            this.btnAjgl.Size = new System.Drawing.Size(140, 60);
            this.btnAjgl.TabIndex = 0;
            this.btnAjgl.Click += new System.EventHandler(this.BtnAjgl_Click);
            // 
            // setDirectory
            // 
            this.setDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setDirectory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("setDirectory.BackgroundImage")));
            this.setDirectory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.setDirectory.Location = new System.Drawing.Point(1236, 12);
            this.setDirectory.Name = "setDirectory";
            this.setDirectory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.setDirectory.Size = new System.Drawing.Size(36, 30);
            this.setDirectory.TabIndex = 0;
            this.setDirectory.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.WinContent);
            this.Controls.Add(this.pHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.pHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel WinContent;
        private System.Windows.Forms.Panel btnAjgl;
        private System.Windows.Forms.Panel btnZjzs;
        private System.Windows.Forms.Panel btnTools;
        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.Panel setDirectory;
        public System.Windows.Forms.Panel btnZjtq;
    }
}